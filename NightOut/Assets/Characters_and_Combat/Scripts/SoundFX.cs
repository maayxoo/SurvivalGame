using System.Collections;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    public PlayerController cc;
    public AudioSource audio;

    public float walkRate = 0.7f;
    private float walkCooldown=0f;
    public float runRate = 0.35f;
    private float runCooldown = 0f;



    // Start is called before the first frame update
    void Awake()
    {
        cc = GetComponent<PlayerController>();
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        walkCooldown -= Time.deltaTime;
        runCooldown -= Time.deltaTime;
        if (cc.groundedPlayer && cc.move.magnitude > 0f && walkCooldown <=0 && cc.isRunning == false)
        {
            audio.volume = Random.Range(0.1f, 0.2f);
            audio.pitch = Random.Range(0.8f, 1.1f);
            audio.Play();
            walkCooldown = walkRate;
        }
        else if(cc.groundedPlayer && cc.move.magnitude > 0f && runCooldown <= 0 && cc.isRunning)
        {
            audio.volume = Random.Range(0.1f, 0.2f);
            audio.pitch = Random.Range(0.8f, 1.1f);
            audio.Play();
            runCooldown = runRate;
        }
    }


}
