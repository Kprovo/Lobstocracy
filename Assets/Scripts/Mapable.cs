using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Mapable : MonoBehaviour
{
    GameObject linkedObject;
    MinimapObject mapComponent;

    void Start()
    {
        if (!gameObject.GetComponentInParent<Minimap>())
        {
            linkedObject = Instantiate(this.gameObject, Minimap.Singleton.transform);

            Transform[] allChildren = GetComponentsInChildren<Transform>();
            Transform[] allLinkedChildren = linkedObject.GetComponentsInChildren<Transform>();

            for (var i = 0; i < allLinkedChildren.Length; i++)
            {
                var components = allLinkedChildren[i].GetComponents<Component>();
                foreach (var t in components.Reverse())
                {
                    if (t is Transform || t is MeshFilter || t is MeshRenderer)
                        continue;
                    Destroy(t);
                }

                allLinkedChildren[i].gameObject.transform.SetParent(Minimap.Singleton.transform);
                mapComponent = allLinkedChildren[i].gameObject.AddComponent<MinimapObject>();
                mapComponent.linkedObject = allChildren[i].gameObject;
            }
        }
    }
}
