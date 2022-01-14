using System.Collections;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public PlayerController cc; //player controller to get movement values
    public AudioSource audio; // audio source components


    //walk and run rates for playing setps are certain time
    public float walkRate = 0.7f;
    private float walkCooldown=0f;
    public float runRate = 0.35f;
    private float runCooldown = 0f;


    void Awake()
    {
        //getting components
        cc = GetComponent<PlayerController>();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //cooldowns for run and walk
        walkCooldown -= Time.deltaTime;
        runCooldown -= Time.deltaTime;
        //when playing sounds pitch and volume are randomised to make steps sound more realistic
        //if player is moving and grounded, and no cooldown and isnt running, play walk sounds 
        if (cc.groundedPlayer && cc.move.magnitude > 0f && walkCooldown <=0 && cc.isRunning == false)
        {
            audio.volume = Random.Range(0.1f, 0.2f);
            audio.pitch = Random.Range(0.8f, 1.1f);
            audio.Play();
            walkCooldown = walkRate;
        }
        //same as above except if player is running
        else if(cc.groundedPlayer && cc.move.magnitude > 0f && runCooldown <= 0 && cc.isRunning)
        {
            audio.volume = Random.Range(0.1f, 0.2f);
            audio.pitch = Random.Range(0.8f, 1.1f);
            audio.Play();
            runCooldown = runRate;
        }
    }


}
