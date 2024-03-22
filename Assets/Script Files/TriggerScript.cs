using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;

public class TriggerScript : MonoBehaviour
{
    public AudioSource EpipenSound;
    public AudioSource ErrorSound;
    Animator epipenAnimator;
    NetworkContext context;

    public static TriggerScript instance; 
    public bool hasTriggered = false;
    public bool isEpipenAudioPlaying = false; // Track whether audio is playing
    public bool isErrorAudioPlaying = false; // Track whether audio is playing

    void Start()
    {
        context = NetworkScene.Register(this);
        epipenAnimator = GameObject.FindGameObjectWithTag("Epipen").GetComponent<Animator>();
        // EpipenSound = GetComponent<AudioSource>();
        // ErrorSound = GetComponent<AudioSource>();
    }

    void Awake() {
        if (instance == null ){
            instance = this;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Epipen" && !hasTriggered)
        {
            // Debug.Log("Just hit by an Epipen");
            EpipenSound.Play();
            // epipenAnimator.SetTrigger("Epipen");
            hasTriggered = true;

            // Update audio state and send network message
            isEpipenAudioPlaying = true;
            SendNetworkMessage();
        } else if (other.gameObject.tag == "OtherMedical"){
            ErrorSound.Play();
            isErrorAudioPlaying = true;
            SendNetworkMessage();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Epipen")
        {
            // Debug.Log("Exit Epipen");
            EpipenSound.Stop();
            // Perform any actions needed when exiting the trigger zone

            // Update audio state and send network message
            isEpipenAudioPlaying = false;
            SendNetworkMessage();
        } 
        if (other.gameObject.tag == "OtherMedical")
        {
            // Debug.Log("Exit Epipen");
            ErrorSound.Stop();
            // Perform any actions needed when exiting the trigger zone

            // Update audio state and send network message
            isErrorAudioPlaying = false;
            SendNetworkMessage();
        } 
    }

    void SendNetworkMessage()
    {
        var message = new Message();
        message.isEpipenAudioPlaying = isEpipenAudioPlaying;
        message.isErrorAudioPlaying = isErrorAudioPlaying;
        context.SendJson(message);
    }

    private struct Message
    {
        public bool isEpipenAudioPlaying;
        public bool isErrorAudioPlaying;
        
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var m = message.FromJson<Message>();
        // Update audio state based on the received message
        if (m.isEpipenAudioPlaying)
        {
            EpipenSound.Play();
        }
        else
        {
            EpipenSound.Stop();
        }
        if (m.isErrorAudioPlaying)
        {
            ErrorSound.Play();
        }
        else
        {
            ErrorSound.Stop();
        }
    }
}
