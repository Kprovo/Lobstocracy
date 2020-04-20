using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapObject : MonoBehaviour
{
    public GameObject linkedObject;

    void Update()
    {
        if (linkedObject)
        {
            gameObject.transform.localPosition = linkedObject.transform.position;
            gameObject.transform.localRotation = linkedObject.transform.rotation;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
