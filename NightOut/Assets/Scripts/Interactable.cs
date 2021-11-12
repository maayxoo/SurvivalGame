using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;


    bool isFocused = false;
    Transform player;
    bool hasInteracted = false;



    public virtual void Interact()
    {

    }

    private void Update()
    {
        if (isFocused && !hasInteracted)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }



    public void OnFocused(Transform playerTransform)
    {
        isFocused = true;
        player = playerTransform;

    }

    public void onDefocused()
    {
        isFocused = false;
        player = null;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
