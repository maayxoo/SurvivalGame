using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerPickup : MonoBehaviour
{
  private void OnTriggerStay(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      if (Input.GetKeyDown(KeyCode.E))
      {
        HungerBar.Hunger += 50f;
        Destroy(gameObject);
      }
    }
  }
}
