using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpipenCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void OnColliderEnter(Collider other)
    {
        if (other.gameObject.tag == "Epipen"){
            print("Collider hit by an Epipen");
        }
    }

    // Update is called once per frame
    void OnColliderStay(Collider other)
    {
        if (other.gameObject.tag == "Epipen"){
            
            print("Collider by Epipen");
        }
        
    }

    void OnColliderExit(Collider other)
    {
        if (other.gameObject.tag == "Epipen"){
            print("Collider Exit Epipen");
        }
        
    }
}
