using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayInteract : MonoBehaviour
{
    SphereCollider ownCollider;
    GameObject hitObject;

    float Fade;

    private void Start()
    {
        ownCollider = gameObject.AddComponent<SphereCollider>();
        ownCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TagAbleHuman")
        {
            string otherMaterialName = other.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().material.mainTexture.name;

            if (other.tag == "TagAbleHuman" && otherMaterialName != otherMaterialName + "_Sprayed" && other.GetComponent<StateMachine>().sprayed == false )
            {
                other.GetComponent<StateMachine>().sprayed = true;

                Transform otherMaterial = other.transform.GetChild(0).transform.GetChild(0);

                //otherMaterial.GetComponent<Renderer>().material.EnableKeyword("_NORMALMAP");
                //otherMaterial.GetComponent<Renderer>().material.EnableKeyword("_METALLICGLOSSMAP");
                //otherMaterial.GetComponent<Renderer>().material.EnableKeyword("_PARALLAXMAP");

                otherMaterial.GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load<Texture>(otherMaterial.GetComponent<Renderer>().material.mainTexture.name + "_Sprayed"));
                //otherMaterial.GetComponent<Renderer>().material.SetTexture("_BumpMap", Resources.Load<Texture>(otherMaterial.GetComponent<Renderer>().material. + "_Sprayed"));
                //otherMaterial.GetComponent<Renderer>().material.SetTexture("_MetallicGlossMap", Resources.Load<Texture>(otherMaterial.GetComponent<Renderer>().material. + "_Sprayed"));
                //otherMaterial.GetComponent<Renderer>().material.SetTexture("_ParallaxMap", Resources.Load<Texture>(otherMaterial.GetComponent<Renderer>().material. + "_Sprayed"));

                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().numberCorporateSprayed++;
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().scoreNumber += Random.Range(15, 30);
            }
        }
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
