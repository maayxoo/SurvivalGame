using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursorhide : MonoBehaviour
{

    //this hides the cursor and locks it to the middle of the screen
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
