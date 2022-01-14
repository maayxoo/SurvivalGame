using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController),typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    //variables for various movement functions 
    [SerializeField] private float playerWalkSpeed = 2.0f;
    [SerializeField] private float playerRunSpeed = 6.0f;

    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 4.0f;

    public bool isRunning = false;

    public Transform attackPoint; //point where an enemy can be in
    public float attackRange = 1f;//range of attack
    public GameObject axeObject;//the axe in the players hand

    //layers for collision checking
    public LayerMask enemyLayers;
    public LayerMask foodLayer;
    public LayerMask weaponLayer;

    //players combat class
    CharacterCombat combat;

    //refences to other components
    private CharacterController controller;
    private Animator anim;
    private PlayerStats playerStats;

    //current move speed
    private float moveSpeed;

    //player input system
    private PlayerInput playerInput;
    private Vector3 playerVelocity;

    //players current move vectory
    public Vector3 move;

    //if the player is grounded true
    public bool groundedPlayer;

    //camera positoning
    private Transform cameraMainTransform;
    private Camera cam;


    //all the inputs from the player
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;
    private InputAction actionAction;

    private InputAction RunStartAction;
    private InputAction RunFinishAction;






    private void Awake()
    {
        //getting components
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        anim = GetComponentInChildren<Animator>();
        playerStats = GetComponent<PlayerStats>();
        cam = Camera.main;
        cameraMainTransform = cam.transform;

        combat = GetComponent<CharacterCombat>();
        //setting player inputs
        moveAction = playerInput.actions["Movement"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];
        actionAction = playerInput.actions["Action"];
        
        RunStartAction = playerInput.actions["RunStart"];
        RunFinishAction = playerInput.actions["RunFinish"];
        axeObject.SetActive(false);//disabling axe as they dont start
    }


    void Update()    
    {

        groundedPlayer = controller.isGrounded;//check if grounded
        Vector2 input = moveAction.ReadValue<Vector2>();//read player input
        move = new Vector3(input.x, 0, input.y);
        move = cameraMainTransform.forward * move.z + cameraMainTransform.right * move.x;
        move.y = 0;


        //Checking Movement of the player and setting speeds and animation
        if (move == Vector3.zero)
        {
            moveSpeed = 0f;
            anim.SetFloat("speed", 0.1f,0.1f,Time.deltaTime);
        }
        else if (move != Vector3.zero && !isRunning)
        {
            moveSpeed = playerWalkSpeed;
            anim.SetFloat("speed", 0.5f, 0.1f, Time.deltaTime);
        }

        else if (move != Vector3.zero && isRunning)
        {
            moveSpeed = playerRunSpeed;
            anim.SetFloat("speed", 1f, 0.1f, Time.deltaTime);
        }
        controller.Move(move * Time.deltaTime * moveSpeed);

        //stopping the player once grounded
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Changes the height position of the player along with jumping animations
        if (groundedPlayer)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isGrounded", true);

            if (jumpAction.triggered)
            {
                anim.SetBool("isJumping", true);
                anim.SetBool("isGrounded", false);
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);//player jump
            }
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if(input != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }

        //toggling sprint and checking if shoot and action is triggered
        if (RunStartAction.triggered)
        {
            enableSprint();
        }
        if (RunFinishAction.triggered)
        {
            disableSprint();
        }
        if (shootAction.triggered)
        {
            Attack();
        }
        if (actionAction.triggered)
        {
            Action();
        }



    }
    //player attacking enemy, checks for enemy layers in the attack point, if so it performs a combat attack
    private void Attack()
    {
        float attackDelay = 1.0f;
        if (playerStats.currentWeapon)
        {
            anim.SetTrigger("WeaponAttack");
            attackDelay = 0.48f;
        }
        else
        {
         anim.SetTrigger("Attack");
        }
        Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider enemy in hitColliders)
        {
            StartCoroutine(AttackEnemy(enemy.GetComponent<CharacterStats>(), attackDelay));
        }
        
        Collider[] hitAidColliders = Physics.OverlapSphere(transform.position, 2f, aidLayer);
        foreach (Collider aid in hitAidColliders)
        {
            if (playerStats.currentHealth > 50)
            {
                playerStats.currentHealth = 100;
            }
            else
            {
                playerStats.currentHealth += 50;
            }
            HealthPickup aidScript = aid.GetComponent<HealthPickup>();
            aidScript.Die();
        }
    }

    //delaying attack inline with animation using Ienumerator
    IEnumerator AttackEnemy(CharacterStats enemyStats, float attackDelay)
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Player Attacked");
        combat.Attack(enemyStats);
    }
    //action, checks all interactable layers for items and performs correct tasks
    private void Action()
    {
        Debug.Log("Action");
        Collider[] hitFoodColliders = Physics.OverlapSphere(transform.position, 2f, foodLayer);
        foreach (Collider food in hitFoodColliders)
        {
            if (HungerBar.Hunger > 50)
            {
                HungerBar.Hunger = 100;
            }
            else
            {
                HungerBar.Hunger += 50f;
            }
            HungerPickup foodScript = food.GetComponent<HungerPickup>();
            foodScript.Die();
        }
        Collider[] hitWeaponColliders = Physics.OverlapSphere(transform.position, 2f, weaponLayer);
        foreach (Collider weapon in hitWeaponColliders)
        {
            WeaponPickup weaponScript = weapon.GetComponent<WeaponPickup>();
            pickupItem(weaponScript);
        }
    }

    //picking up weapon
    private void pickupItem(WeaponPickup weaponScript)
    {
        weaponScript.Pickup();
        playerStats.currentWeapon = weaponScript.weapon;
        axeObject.SetActive(true);
    }
    //gizmos for reference
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    //enable and disable sprint
    private void enableSprint()
    {
        isRunning = true;
    }
    private void disableSprint()
    {
        isRunning = false;
    }
}
