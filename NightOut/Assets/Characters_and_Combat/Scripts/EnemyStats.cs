using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//subclass of characterstats
public class EnemyStats : CharacterStats
{
    //when an enemy dies destroy the game object
    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
