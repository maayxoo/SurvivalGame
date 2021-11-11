using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    public Slider HungerSlider;

    public static float Hunger;
    float maxHunger = 100f;
    // Start is called before the first frame update
    void Start()
    {
        Hunger = maxHunger;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Hunger);

        HungerSlider.value = Hunger;

        if (Hunger > 0)
        {

          Hunger -= 0.2f * Time.deltaTime;

          if(Input.GetKey(KeyCode.W))
          {
            Hunger -= 2f * Time.deltaTime;
          }

          if(Input.GetKey(KeyCode.UpArrow))
          {
            Hunger -= 2f * Time.deltaTime;
          }

          if(Input.GetKey(KeyCode.Space))
          {
            Hunger -= 4f * Time.deltaTime;
          }
        }
      }
    }
