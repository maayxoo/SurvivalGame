using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]

public class HungerBar : MonoBehaviour
{

    private InputAction moveAction;
    private InputAction jumpAction;

    private PlayerInput playerInput;


    public Slider HungerSlider;

    public static float Hunger;
    float maxHunger = 100f;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        Hunger = maxHunger;
        moveAction = playerInput.actions["Movement"];
        jumpAction = playerInput.actions["Jump"];
        Update();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        HungerSlider.value = Hunger;

        if (Hunger > 0)
        {

          Hunger -= 0.2f * Time.deltaTime;

          if(input != Vector2.zero)
          {
            Hunger -= 2f * Time.deltaTime;
            //this is how much value the player loses when they move
          }
          if (jumpAction.triggered)
          {
                Hunger -= 4f * Time.deltaTime;
                //if player jumps this is how much value they'll lose
            }
        }
      }
    }
