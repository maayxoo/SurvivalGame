using System.Collections;
using UnityEngine;

public class EnemySoundFX : MonoBehaviour
{
    public EnemyController ec;//get the enemy controller to get values for if they are moving
    public AudioSource audio;//audio source component

    public float walkRate = 0.6f;//walk rate for how often we want to play the sounds
    private float walkCooldown = 0f;//cooldown

    // Start is called before the first frame update
    void Awake()
    {
        //getting components
        ec = GetComponent<EnemyController>();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //reset cooldown
        walkCooldown -= Time.deltaTime;
        //if no cooldown and the enemy is walking play the animation sounds
        if (ec.move.magnitude > 0f && walkCooldown <= 0 )
        {
            //randomizing pitch and volume to make steps sound more realistic
            audio.volume = Random.Range(0.1f, 0.2f);
            audio.pitch = Random.Range(0.8f, 1.1f);
            audio.Play();
            walkCooldown = walkRate;
        }
    }


}
