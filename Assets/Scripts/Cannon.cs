using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour {

	public GameObject cannonballPrefab;
	public Transform launchPoint;
	public Transform barrelPivot;
	public int launchSpeed;

    public int ammo = 10;

    // Temporary
    public Text ammoText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && ammo >= 1) {
            ammo--;
            // Temporary
            ammoText.text = "Ammo: " + ammo.ToString();
			GameObject newCannonball = Instantiate (cannonballPrefab) as GameObject;
			newCannonball.transform.position = launchPoint.position;
            newCannonball.transform.rotation = launchPoint.rotation;
			newCannonball.GetComponent<Rigidbody> ().velocity = launchPoint.forward * launchSpeed;
		} else if(ammo < 1) {
            ammoText.text = "Out of ammo! Press 'R' to restart.";
        }

		Vector3 moveDirection = new Vector3 (0, Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		transform.Rotate (new Vector3(0, moveDirection.y, 0));
		barrelPivot.Rotate (new Vector3(0, 0, -moveDirection.z));
	}
}
