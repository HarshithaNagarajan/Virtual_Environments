using System.Collections;
using System.Collections.Generic;
using SIPSorceryMedia.Abstractions;
using UnityEngine;

public class playChokingSound : MonoBehaviour
{

    AudioSource source;
    Collider soundTrigger;

    //void Start()
    //{
        // Find the game object with the tag "Epipen" and get its Animator component

    //}



    void Awake()
    {
        source = GetComponent<AudioSource>();
        soundTrigger = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "avatarHead")
        {
            print("Just hit by an avatarHead");
            source.Play();
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "avatarHead")
        {
            print("Being hit by avatarHead");
        }

    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "avatarHead")
        {
            print("Exit");
            source.Stop();
        }

    }
    
}