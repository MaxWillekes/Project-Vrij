using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayInteract : MonoBehaviour
{
    SphereCollider ownCollider;
    GameObject hitObject;

    float Fade;
    // Update is called once per frame
    private void Start()
    {
        ownCollider = gameObject.AddComponent<SphereCollider>();
        //ownCollider.convex = true;
        ownCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "TagAbleHuman" && other.GetComponent<Renderer>().material.color != Color.red)
        {
            Debug.Log(other.name);
            other.GetComponent<Renderer>().material.color = Color.red;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().numberCorporateRemaining--;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().scoreNumber += Random.Range(15, 30);
        }
        /*
        if (other.tag == "TagAbleWall")
        {
            Fade = other.GetComponent<SpriteRenderer>().color.a;
            other.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Fade += +.1f);
            Debug.Log(Fade);
        }
        */
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "TagAbleWall" && other.name == "Grafitti")
        {
            Fade = other.GetComponent<SpriteRenderer>().color.a;
            other.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, Fade += +.01f);
            Debug.Log(Fade);
        }
    }
    
}
