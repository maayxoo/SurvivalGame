using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    //How often the character can attack in seconds
    private float attackCooldown = 0;
    //The characters stats
    CharacterStats myStats;

    //when the script is loaded
    private void Start()
    {
        myStats = GetComponent<CharacterStats>();//getting the stats class from the character component
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;//this reduces the cool down as time goes on so that the character can attack again
    }
    public void Attack(CharacterStats targetStats)
    {
        //check if the attack cooldown is 0 or less and that the target exists
        if (attackCooldown <= 0f &&targetStats!=null)
        {
            int damageDealt = Random.Range(myStats.minDamage, myStats.maxDamage);//deals random damage based on the range of the stats
            targetStats.TakeDamage(damageDealt);//apply the damage to the targets stats
            attackCooldown = myStats.attackSpeed;//reset the cooldown
        }
    }
}
