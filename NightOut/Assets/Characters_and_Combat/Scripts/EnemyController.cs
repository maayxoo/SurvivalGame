using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{

    [SerializeField] private float lookRadius = 10f;

    public Vector3 move;

    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();

        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        move = agent.velocity;
        if (move == Vector3.zero)
        {
            anim.SetFloat("Speed", 0.1f, 0.1f, Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Speed",1f, 0.1f, Time.deltaTime);
        }

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            if (distance <= agent.stoppingDistance)
            {
                //attacking
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if (targetStats != null)
                {
                    //Debug.Log("Attack in - EnemyController");
                    anim.SetTrigger("Attack");
                    StartCoroutine(Attack(targetStats));
                }
                FaceTarget();
            }
        }
    }


    IEnumerator Attack(CharacterStats targetStats)
    {
        yield return new WaitForSeconds(1.0f);
        combat.Attack(targetStats);
    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
