using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//applied on weapons on the ground
public class WeaponPickup : MonoBehaviour
{
    public Weapon weapon; //what weapon is the pickup associated with

    //when picking up we want to destroy it.
    public void Pickup()
    {
        Destroy(gameObject);
    }
}
