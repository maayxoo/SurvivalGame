using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController),typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;
    [SerializeField] private float rotationSpeed = 4.0f;

    public Transform attackPoint;
    public float attackRange = 0.3f;
    public LayerMask enemyLayers;
    public LayerMask foodLayer;

    CharacterCombat combat;


    private CharacterController controller;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private Transform cameraMainTransform;
    private Camera cam;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction shootAction;
    private InputAction actionAction;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        cam = Camera.main;
        cameraMainTransform = cam.transform;

        combat = GetComponent<CharacterCombat>();

        moveAction = playerInput.actions["Movement"];
        lookAction = playerInput.actions["MouseLook"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];
        actionAction = playerInput.actions["Action"];

    }

    void Update()    
    {
        if (shootAction.triggered)
        {
            Attack();
        }
        if (actionAction.triggered)
        {
            Action();
        }

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);
        move = cameraMainTransform.forward * move.z + cameraMainTransform.right * move.x;
        move.y = 0;


        controller.Move(move * Time.deltaTime * playerSpeed);

        

        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if(input != Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void Attack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        Debug.Log("Player Attacked");
        foreach(Collider enemy in hitColliders)
        {
            combat.Attack(enemy.GetComponent<CharacterStats>());
        }
    }

    private void Action()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f, foodLayer);
        foreach (Collider food in hitColliders)
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

    }


}