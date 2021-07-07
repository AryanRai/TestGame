using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermanager : MonoBehaviour
{
    #region Singleton

    public static playermanager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;
}
