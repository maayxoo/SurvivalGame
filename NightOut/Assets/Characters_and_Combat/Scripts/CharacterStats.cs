using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//This class has all the stats for the characters
public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;//max health a character can have
    public int currentHealth;//current health

    //defines the base range of a players damage
    public int minBaseDamage = 3;
    public int maxBaseDamage = 10;
    //defines actual range after modifiers are applied
    public int minDamage;
    public int maxDamage;

    //how often the character can attack in seconds
    public float attackSpeed;

    //default attack speed
    public float baseAttackSpeed = 2.75f;

    public virtual void Die()
    {
        //gameOver();
    }

    //on awake set all the stats to their base stats
    public virtual void Awake()
    {
        currentHealth = maxHealth;
        minDamage = minBaseDamage;
        maxDamage = minBaseDamage;
        attackSpeed = baseAttackSpeed;
    }

    //applying damage to health stat
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
}
