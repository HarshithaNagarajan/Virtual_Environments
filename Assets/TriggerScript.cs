using System.Collections;
using System.Collections.Generic;
using SIPSorceryMedia.Abstractions;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    private Animator epipenAnimator;

    void Start()
    {
        // Find the game object with the tag "Epipen" and get its Animator component
        
    }

    void Awake(){
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Epipen"){
            print("Just hit by an Epipen");
            GameObject epipenObject = GameObject.FindGameObjectWithTag("Epipen");
        // if (epipenObject != null)
        // {
        //     epipenAnimator = epipenObject.GetComponent<Animator>();
        //     epipenAnimator.SetTrigger("Epipen");
        // }
        // else
        // {
        //     Debug.LogError("Epipen object not found!");
        // }
            
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Epipen"){
            // transform.Rotate(90, 90, 90);
            print("Hit by Epipen");
            // epipenAnimator.SetBool("triggeredEpipen", true);

        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Epipen"){
            print("Exit Epipen");
            audioSource.Stop();
            // epipenAnimator.SetBool("triggeredEpipen", false);

        }
        
    }
}
