using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{

    public Slider HealthSlider;

    private void Update()
    {
        HealthSlider.value = currentHealth;
    }
    public override void Die()
    {
        base.Die();
        //Destroy(gameObject);
    }
}
