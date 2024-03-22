using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;

public class TriggerScript : MonoBehaviour
{
    AudioSource audioSource;
    Animator epipenAnimator;
    NetworkContext context;

    public static TriggerScript instance; 
    public bool hasTriggered = false;
    public bool isAudioPlaying = false; // Track whether audio is playing

    void Start()
    {
        context = NetworkScene.Register(this);
        epipenAnimator = GameObject.FindGameObjectWithTag("Epipen").GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
            audioSource.Play();
            // epipenAnimator.SetTrigger("Epipen");
            hasTriggered = true;

            // Update audio state and send network message
            isAudioPlaying = true;
            SendNetworkMessage();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Epipen")
        {
            // Debug.Log("Exit Epipen");
            audioSource.Stop();
            // Perform any actions needed when exiting the trigger zone

            // Update audio state and send network message
            isAudioPlaying = false;
            SendNetworkMessage();
        }
    }

    void SendNetworkMessage()
    {
        var message = new Message();
        message.isAudioPlaying = isAudioPlaying;
        context.SendJson(message);
    }

    private struct Message
    {
        public bool isAudioPlaying;
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var m = message.FromJson<Message>();
        // Update audio state based on the received message
        if (m.isAudioPlaying)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}
