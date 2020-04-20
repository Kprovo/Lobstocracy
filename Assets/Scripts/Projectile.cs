using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private bool hasCollided = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasCollided) {
            // update the rotation of the projectile during trajectory motion
            transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);
        }
    }

    void OnCollisionEnter()
    {
        hasCollided = true;
    }

	private void OnBecameInvisible() {
		Destroy (gameObject);
	}
}
