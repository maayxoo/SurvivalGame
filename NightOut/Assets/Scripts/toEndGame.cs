using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toEndGame : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(3);
    }
}
