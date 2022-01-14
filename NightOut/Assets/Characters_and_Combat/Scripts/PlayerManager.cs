using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//singleton class for playing so it can be refenced in game
public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    void Awake()
    {
        instance = this;    
    }

    #endregion

    public GameObject player;
}
