using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    public float attackRange = 1f;
    public float attackDamage = 20f;
    public float attackCooldown = 0.5f;
    private float cooldownTimer = 0f;
    private Collider collider;
    private float cubeDamageBoost = 20f;

    void Start()
    {
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && cooldownTimer <= 0)
        {
            collider.isTrigger = false;
            Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.CompareTag("Enemy"))
                {
                    EnemyAI enemy = collider.gameObject.GetComponent<EnemyAI>();
                    EnemyAIReset enemyAIReset= collider.gameObject.GetComponent<EnemyAIReset>();
                    if (enemy != null)
                    {
                        enemy.EnemyTakeDamage(attackDamage); 
                    }
                    if(enemyAIReset !=null)
                    {
                        enemyAIReset.EnemyTakeDamage(attackDamage);
                    }
                }
            }
            cooldownTimer = attackCooldown;
        }

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (cooldownTimer <= 0 && !collider.isTrigger)
        {
            collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Material"))
        {
            IncreaseDamage();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Material_2"))
        {
            IncreaseDamage();
            Destroy(other.gameObject);
        }
    }

    private void IncreaseDamage()
    {
        // Tính toán sát thương tăng thêm khi chạm vào cube
        float extraDamage = cubeDamageBoost;
        attackDamage += extraDamage;
        Debug.Log("Extra damage when touching the cube: " + extraDamage);
    }

    private void OnDrawGizmosSelected()
    {
        // Vẽ hình cầu tấn công
        Gizmos.color = Color.red;

        Matrix4x4 oldMatrix = Gizmos.matrix;
        Gizmos.matrix = transform.localToWorldMatrix;

        float radiusX = attackRange;
        float radiusY = attackRange;
        int numSegments = 32;

        Vector3 prevPoint = Vector3.zero;
        for (int i = 0; i <= numSegments; i++)
        {
            float angle = i / (float)numSegments * 360f;
            Vector3 curPoint = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) * radiusX, 0f, Mathf.Cos(angle * Mathf.Deg2Rad) * radiusY);
            if (i > 0)
            {
                Gizmos.DrawLine(prevPoint, curPoint);
            }
            prevPoint = curPoint;
        }
        Gizmos.matrix = oldMatrix;
    }
}
