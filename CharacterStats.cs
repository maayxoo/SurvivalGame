using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public int minDamage = 3;
    public int maxDamage = 10;
    public virtual void Die()
    {
        gameOver();
    }

    public virtual void Awake()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void gameOver()
    {
        SceneManager.LoadScene(4);
    }

}
