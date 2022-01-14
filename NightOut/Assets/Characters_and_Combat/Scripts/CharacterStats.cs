using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public int minBaseDamage = 3;
    public int maxBaseDamage = 10;

    public int minDamage;
    public int maxDamage;

    public float attackSpeed;
    public float baseAttackSpeed = 2.75f;
    public virtual void Die()
    {
        //Die
    }

    public virtual void Awake()
    {
        currentHealth = maxHealth;
        minDamage = minBaseDamage;
        maxDamage = minBaseDamage;
        attackSpeed = baseAttackSpeed;
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
