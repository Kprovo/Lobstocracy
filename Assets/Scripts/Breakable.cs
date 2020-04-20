using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {

    public float breakEnergy = 20000f;
    public int scoreValue = 2000;
    public float damageThreshold = 2f; // To be used for avoiding damage when level loads

    public GameObject crumbleObject;

    void OnCollisionEnter(Collision collider)
    {
        // Might need to check if object is already about to be destroyed
        if (collider.impulse.sqrMagnitude >= breakEnergy) {
            //Debug.Log(gameObject.name.ToString() + " destroyed!");
            //Debug.Log("SQRMagnitude: " + collider.impulse.sqrMagnitude.ToString());
            GameManager._singleton.score += scoreValue;
            Debug.Log(GameManager._singleton.score.ToString());

            // NOTE: The following particle system code will not work until Unity version 19.1

            /*
            // Create object to handle particles
            var crumble = Instantiate(crumbleObject, transform.position, Quaternion.identity);
            // Assign particle system and particle system shape to variables
            var ps = crumble.GetComponent<ParticleSystem>();
            var pss = ps.shape;
            // Copy mesh to the particle system renderer
            pss.meshRenderer = gameObject.GetComponent<MeshRenderer>();
            // Play particle animation
            ps.Play();
            */

            // Slow momentum of colliding object before destroying self
            //StartCoroutine(DestroyAfterPhysics());

            gameObject.AddComponent<TriangleExplosion>();

            StartCoroutine(gameObject.GetComponent <TriangleExplosion>().SplitMesh(true));
        } else {
            float damageDone = collider.impulse.sqrMagnitude;
            breakEnergy -= damageDone;
            //Debug.Log("breakEnergy: " + breakEnergy.ToString());
            // TODO:
            // Take points from total value of the object rounded to the nearest 10s place and add that to the score
        }
    }

    private IEnumerator DestroyAfterPhysics()
    {
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        Destroy(gameObject);
    }
}
