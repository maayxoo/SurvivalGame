using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    private float attackCooldown = 0;
    CharacterStats myStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }
    public void Attack(CharacterStats targetStats)
    {
        //Debug.Log("Attack in - CharacterCombat  ");
        if (attackCooldown <= 0f &&targetStats!=null)
        {
            int damageDealt = Random.Range(myStats.minDamage, myStats.maxDamage);
            Debug.Log(damageDealt + "damage dealt");
            targetStats.TakeDamage(damageDealt);
            attackCooldown = myStats.attackSpeed;
        }
    }
}
