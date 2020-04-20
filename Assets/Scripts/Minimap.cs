using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public static GameObject Singleton;

    void Awake()
    {
        Singleton = this.gameObject;
    }
}
