using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    int currentCamera = 0;
    //public GameObject[] perspectives;
    public List<GameObject> perspectives = new List<GameObject>();
    GameObject targetObject;

    private Camera m_Camera;
    [SerializeField] private CustomMouseLook m_MouseLook;

    bool foundEnemies = false;


    // Use this for initialization
    void Start () {        
        foreach( Enemy enemy in GameManager._singleton.enemies) {
            perspectives.Add(enemy.gameObject);
        }
        if (perspectives.Count != 1) {
            foundEnemies = true;
        }
        m_Camera = Camera.main;
        m_MouseLook.Init(transform, m_Camera.transform);
        targetObject = perspectives[0];
    }
	
	// Update is called once per frame
	void Update () {
        RotateView();
        if ( foundEnemies == false) {
            foreach (Enemy enemy in GameManager._singleton.enemies) {
                perspectives.Add(enemy.gameObject);
            }
            if (perspectives.Count != 1) {
                foundEnemies = true;
            }
        }
        

        if (Input.GetKeyDown(KeyCode.Space)) {
            int nextCamera = NextCamera();
            // Set position
            Vector3 newPos = perspectives[nextCamera].transform.position;
            targetObject = perspectives[nextCamera];
            newPos.y += 1;
            gameObject.transform.position = newPos;
            // Set rotation
            Quaternion cameraFace = perspectives[nextCamera].transform.rotation;
            //Debug.Log(perspectives[nextCamera].transform.rotation.ToString());
            gameObject.transform.rotation = cameraFace;
            // Set mesh renderer
            if(perspectives[currentCamera] != null) {
                var renderers = perspectives[currentCamera].GetComponentsInChildren<MeshRenderer>();
                foreach (var r in renderers) {
                    r.enabled = true;
                }
                var skinnedRenderers = perspectives[currentCamera].GetComponentsInChildren<SkinnedMeshRenderer>();
                foreach (var sr in skinnedRenderers) {
                    sr.enabled = true;
                }
            }
            var renderersN = perspectives[nextCamera].GetComponentsInChildren<MeshRenderer>();
            foreach (var r in renderersN) {
                r.enabled = true;
            }
            var skinnedRenderersN = perspectives[nextCamera].GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var sr in skinnedRenderersN) {
                sr.enabled = true;
            }
        }

        if (true)/* Get Mouse Down */ {
            // Do raycast
        }

            // Update camera position
            if (targetObject) {
            Transform target = targetObject.transform;
            //transform.position = new Vector3(target.position.x - target.transform.forward.x * 5, target.position.y + 3, target.position.z - target.transform.forward.z * 5);
            transform.position = new Vector3(target.position.x, target.position.y + 4.5f, target.position.z);
        }
	}

    int NextCamera()
    {
        if((currentCamera + 1) >= perspectives.Count) {
            currentCamera = 0;
        } else {
            currentCamera++;
        }

        if(perspectives[currentCamera] != null) {
            return currentCamera;
        } else {
            return NextCamera();
        }
        
    }


    void FixedUpdate()
    {
        m_MouseLook.UpdateCursorLock();
    }

    private void RotateView()
    {
        m_MouseLook.LookRotation(transform, m_Camera.transform);
    }
}
