using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayInteract : MonoBehaviour
{
    SphereCollider ownCollider;
    GameObject hitObject;
    // Update is called once per frame
    private void Start()
    {
        ownCollider = gameObject.AddComponent<SphereCollider>();
        //ownCollider.convex = true;
        ownCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TagAbleHuman")
        {
            Debug.Log(other.name);
            other.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}
