using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIReset : MonoBehaviour
{
    public Transform player;
    private Player Player;
    [Header("Move")]
    public float moveSpeed = 20;
    public float rotationSpeed = 3.0f;
    public float detectionRadius = 20f;
    private bool isChasing = false;
    public float stopDistance = 2f;
    [Header("HP")]
    public GameObject healthBar;
    public float max_Health = 100f;
    public float cur_Health = 0f;
    public ParticleSystem DeathEffect;
    public ParticleSystem HitEffect;
    public float attackRange = 2f;
    public float attackCooldown = 0.25f;
    private float attackTimer = 0f;
    public float knockbackForce = 10f;
    public Light damageLight;
    private bool isTakingDamage = false;
    public Transform respawnPoint;

    private Rigidbody enemyRigidbody;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Player = player.GetComponent<Player>();
        cur_Health = max_Health;
        DeathEffect.Stop();
        HitEffect.Stop();

        enemyRigidbody = GetComponent<Rigidbody>();

        damageLight.enabled = false;
    }

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.transform == player)
            {
                // Kiểm tra xem nhân vật có đủ tầm nhìn trực tiếp hay không
                if (CheckLineOfSight())
                {
                    isChasing = true;
                    break;
                }
            }
            else
            {
                isChasing = false;
            }
        }

        if (isChasing)
        {
            transform.LookAt(player);
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    bool CheckLineOfSight()
    {
        RaycastHit hit;
        Vector3 direction = player.position - transform.position;

        if (Physics.Raycast(transform.position, direction, out hit, detectionRadius))
        {
            if (hit.transform == player)
            {
                return true;
            }
        }

        return false;
    }

    public void EnemyTakeDamage(float damage)
    {
        cur_Health -= damage;
        float calc_Health = cur_Health / max_Health;
        SetHealthBar(calc_Health);
        if (cur_Health <= 0)
        {
            Respawn();
        }
        else
        {
            Instantiate(HitEffect, transform.position, Quaternion.identity);
            ApplyKnockback();

            damageLight.enabled = true;
            isTakingDamage = true;
            StartCoroutine(DisableDamageLight());
        }
    }

    void ApplyKnockback()
    {
        Vector3 knockbackDirection = transform.position - player.position;
        knockbackDirection.y = 0f; // Ignore vertical component
        knockbackDirection.Normalize();
        enemyRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
    }

    void Attacks()
    {
        if (attackTimer <= 0)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.CompareTag("Player"))
                {
                    collider.gameObject.GetComponent<Player>().PlayerTakeDamage(10f);
                }
            }

            attackTimer = attackCooldown;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Attacks();
        }
    }

    void SetHealthBar(float myHealth)
    {
        healthBar.transform.localScale = new Vector3(myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

    public void ResetHealth()
    {
        cur_Health = max_Health;
        SetHealthBar(1f);
    }

    void Respawn()
    {
        Instantiate(DeathEffect, transform.position, Quaternion.identity);
        transform.position = respawnPoint.position;
        ResetHealth();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    IEnumerator DisableDamageLight()
    {
        yield return new WaitForSeconds(0.5f); // Thời gian ánh sáng point light được bật

        damageLight.enabled = false;
        isTakingDamage = false;
    }
}
