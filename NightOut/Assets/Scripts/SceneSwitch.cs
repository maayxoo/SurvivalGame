using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    void onTriggerEnter(Collider other)
    {
        //load end scene
        SceneManager.LoadScene(3); //loads the scene with index 3 upon collision with the trigger
    }
}
