using System.Collections;
using System.Collections.Generic;
using SIPSorceryMedia.Abstractions;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    Animator epipenAnimator;
    
    private bool hasTriggered = false;

    void Start()
    {
        epipenAnimator = GameObject.FindGameObjectWithTag("Epipen").GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
    }

    void Awake(){
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Epipen" && !hasTriggered)
        {
            Debug.Log("Just hit by an Epipen");
            audioSource.Play();
            epipenAnimator.SetTrigger("Epipen");
            hasTriggered = true;
        }
    }

    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Epipen")
        {
            Debug.Log("Hit by Epipen");
            // Perform any continuous actions here if needed
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Epipen")
        {
            Debug.Log("Exit Epipen");
            audioSource.Stop();
            // Perform any actions needed when exiting the trigger zone
        }
    }
}
