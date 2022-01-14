using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
            SceneManager.LoadScene(2); //loads the scene with index 2 upon collision with the trigger
    }
}