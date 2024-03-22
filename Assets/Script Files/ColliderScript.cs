using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Epipen"){
            print("Just hit by an Epipen");
        }
    }

    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Epipen"){
            print("Hit by Epipen");
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Epipen"){
            print("Exit Epipen");
        }
        
    }
}
