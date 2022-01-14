using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapon weapon;

    // Start is called before the first frame update
    public void Pickup()
    {
        Debug.Log("Picking up" + weapon.name);
        Destroy(gameObject);
    }
}
