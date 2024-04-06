using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxHealth = 100f;
    public float curHealth = 0f;
    public GameObject healthBar;
    public GameObject healthBarMain;
    public ParticleSystem DeathEffect;
    public ParticleSystem HitEffect;

    public float knockbackTime = 0.1f;
    public float knockbackForce = 10f;
    private float knockbackCounter;
    private bool knockback;
    private Vector3 knockbackDirection;
    public CharacterController controller;
    public GameOverMenu gameOverMenu;

    void Start()
    {
        curHealth = maxHealth;
        DeathEffect.Stop();
        HitEffect.Stop();
    }

    void Update()
    {
        if (knockback)
        {
            knockbackCounter -= Time.deltaTime;
            Vector3 moveDirection = knockbackDirection * knockbackForce;
            controller.Move(moveDirection * Time.deltaTime);

            if (knockbackCounter <= 0)
            {
                knockback = false;
            }
        }
    }

    public void PlayerTakeDamage(float damage)
    {
        curHealth -= damage;
        float calcHealth = curHealth / maxHealth;
        SetHealthBar(calcHealth);
        if (curHealth <= 0)
        {
            Die();
        }
        else
        {
            PlayerKnockback(-transform.forward);
            Instantiate(HitEffect, transform.position, Quaternion.identity);
        }
    }

    public void PlayerKnockback(Vector3 direction)
    {
        knockbackCounter = knockbackTime;
        knockback = true;
        knockbackDirection = direction;
    }

    public void IncreaseHealth(float amount)
    {
        curHealth += amount;
        curHealth = Mathf.Clamp(curHealth, 0f, maxHealth);
        float calcHealth = curHealth / maxHealth;
        SetHealthBar(calcHealth);
    }

    void SetHealthBar(float myHealth)
    {
        healthBar.transform.localScale = new Vector3(myHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        healthBarMain.transform.localScale = new Vector3(myHealth, healthBarMain.transform.localScale.y, healthBarMain.transform.localScale.z); 
    }

    void Die()
    {
        gameOverMenu.ShowGameOverMenu();
        Destroy(gameObject);
        Instantiate(DeathEffect, transform.position, Quaternion.identity);
    }
}
