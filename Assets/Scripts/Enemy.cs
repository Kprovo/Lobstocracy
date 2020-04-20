using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private float breakEnergy = 10f;

    void Start()
    {
        GameManager._singleton.enemies.Add(this);
        GameManager._singleton.enemiesLeft++;
    }

    void OnCollisionEnter(Collision collider)
    {
        if(collider.impulse.sqrMagnitude >= breakEnergy) {
            Debug.Log("Enemy Destroyed!");
            //Debug.Log("SQRMagnitude: ");
            //Debug.Log(collider.impulse.sqrMagnitude);
            GameManager._singleton.enemiesLeft--;
            GameManager._singleton.GameOverCheck();
            Destroy(gameObject);
        }        
    }
}
