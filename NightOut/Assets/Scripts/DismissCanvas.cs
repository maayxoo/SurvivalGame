using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismissCanvas : MonoBehaviour
{
    public GameObject canvas;
    public float secondsToDismiss = 7.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(dismiss(canvas));
    }

    IEnumerator dismiss(GameObject canvas)
    {
        yield return new WaitForSeconds(secondsToDismiss);
        canvas.SetActive(false);
    }

}
