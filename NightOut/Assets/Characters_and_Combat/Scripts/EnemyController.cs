using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{

    [SerializeField] private float lookRadius = 10f; //radius for detecting the enemy detecting a player

    public Vector3 move;//the current move value of enemy

    Transform target; // the target of the enemy
    NavMeshAgent agent; //the navmeshagent in charge of enemy AI
    CharacterCombat combat;//enemys combat class

    private Animator anim;//animation component

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;//set the target to the only player
        //getting components for enemy obect
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);//distance from enemy to player
        move = agent.velocity;//current velocity of the enemy
        //checking if the enemy is moving and if so changing its animation in the blend tree
        if (move == Vector3.zero)
        {
            anim.SetFloat("Speed", 0.1f, 0.1f, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Speed",1f, 0.1f, Time.deltaTime);
        }

        //if the distance is in the look radius, start moving towards the player
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            //if its in attacking distance attack the player and perform the animation
            if (distance <= agent.stoppingDistance)
            {
                //attacking
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    anim.SetTrigger("Attack");
                    StartCoroutine(Attack(targetStats));
                }
                FaceTarget();
            }
        }
    }


    //attack function, adds a delay so that the attack function happens timed with the animation
    IEnumerator Attack(CharacterStats targetStats)
    {
        yield return new WaitForSeconds(1.0f);
        combat.Attack(targetStats);
    }
    //if the enemy is close it will turn to face the target
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


    //gizmos for reference
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
