using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
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
        if (attackCooldown <= 0f)
        {
            int damageDealt = Random.Range(myStats.minDamage, myStats.maxDamage);
            targetStats.TakeDamage(damageDealt);
            attackCooldown = 1 / attackSpeed;
            Debug.Log(damageDealt + "damage dealt");
        }
    }
}
