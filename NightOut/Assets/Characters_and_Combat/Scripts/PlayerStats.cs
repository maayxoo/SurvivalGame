using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerStats : CharacterStats
{
    public Weapon currentWeapon = null; //current equipped weapon

    public int maxHunger = 100;
    public int currentHunger;

    public float starveDamageTick=1.0f; //how often (in seconds) should the user lose health when they are starving;
    public float starveDamangeCooldown = 0f;//how long its been since last damage
    public int starveDamge = 10;//amount of damage player takes when starving; 


    public Slider HealthSlider;//health bar ui
    public Slider HungerSlider;//hunger bar ui

    public override void Awake()
    {
        base.Awake();
        currentHunger = maxHunger;//set base values
    }


    private void Update()
    {
        //set bars to current values for heatlh and hunger
        HealthSlider.value = currentHealth;
        HungerSlider.value = currentHunger;


        starveDamangeCooldown -= Time.deltaTime;//takes away current time value, once its below 0 the cooldown is over
        //if hunger is less then zero and no cooldown cause starve damage to player
        if (currentHunger <= 0 && starveDamangeCooldown<=0f)
        {
            Starve();
        }

        //if they have a weapon apply modifiers to damage otherwise use base stats
        if (currentWeapon)
        {
            minDamage = minBaseDamage*currentWeapon.damageModifier;
            maxDamage = maxBaseDamage*currentWeapon.damageModifier;
        }
        else
        {
            minDamage = minBaseDamage;
            maxDamage = maxBaseDamage;
        }
    }

    //starve damage
    public void Starve()
    {
        currentHealth -= starveDamge;//take away starve damage from users health
        starveDamangeCooldown = starveDamageTick;//reset cooldown
    }

    //switch to game over scene
    public void gameOver()
    {
        SceneManager.LoadScene(4);
    }

    //when a player dies
    public override void Die()
    {
        base.Die();
        //Destroy(gameObject);
        gameOver(); //call game over function
    }
}
