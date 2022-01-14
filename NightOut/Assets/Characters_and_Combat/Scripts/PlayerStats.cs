using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    public Weapon currentWeapon = null;

    public int maxHunger = 100;
    public int currentHunger;

    public float starveDamageTick=1.0f; //how often (in seconds) should the user lose health when they are starving;
    public float starveDamangeCooldown = 0f;//how long its been since last damage
    public int starveDamge = 10;//amount of damage player takes when starving; 


    public Slider HealthSlider;
    public Slider HungerSlider;

    public override void Awake()
    {
        base.Awake();
        currentHunger = maxHunger;
    }

    private void Update()
    {
        HealthSlider.value = currentHealth;
        HungerSlider.value = currentHunger;

        starveDamangeCooldown -= Time.deltaTime;//takes away current time value, once its below 0 the cooldown is over
        if (currentHunger <= 0 && starveDamangeCooldown<=0f)
        {
            Starve();
        }

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

    public void Starve()
    {
        currentHealth -= starveDamge;//take away starve damage from users health
        starveDamangeCooldown = starveDamageTick;//reset cooldown
    }

    public override void Die()
    {
        base.Die();
        //Destroy(gameObject);
    }
}
