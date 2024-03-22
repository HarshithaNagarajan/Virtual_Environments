using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;

public class OxygenMaskTrigger : MonoBehaviour
{
    public AudioSource OxygenMaskSound;
    public AudioSource breathingSound;
    NetworkContext context;

    public static OxygenMaskTrigger instance; 
    public bool hasTriggered = false;
    public bool isOxygenMaskAudioPlaying = false; // Track whether audio is playing
    public bool isBreathingAudioPlaying = false; // Track whether audio is playing

    void Start()
    {
        context = NetworkScene.Register(this);
        OxygenMaskSound = GetComponent<AudioSource>();
        breathingSound = GetComponent<AudioSource>();
    }

    void Awake() {
        if (instance == null ){
            instance = this;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "OxygenMask")
        {
            // Debug.Log("Just hit by an Epipen");
            OxygenMaskSound.Play();
            // epipenAnimator.SetTrigger("Epipen");
            hasTriggered = true;

            // Update audio state and send network message
            isOxygenMaskAudioPlaying = true;
            SendNetworkMessage();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "OxygenMask")
        {
            // Debug.Log("Exit Epipen");
            OxygenMaskSound.Stop();
            breathingSound.Play();
            // Perform any actions needed when exiting the trigger zone

            // Update audio state and send network message
            isOxygenMaskAudioPlaying = false;
            isBreathingAudioPlaying = true;
            SendNetworkMessage();
        }
    }

    void SendNetworkMessage()
    {
        var message = new Message();
        message.isOxygenMaskAudioPlaying = isOxygenMaskAudioPlaying;
        message.isBreathingAudioPlaying = isBreathingAudioPlaying;
        context.SendJson(message);
    }

    private struct Message
    {
        public bool isOxygenMaskAudioPlaying;
        public bool isBreathingAudioPlaying;
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var m = message.FromJson<Message>();
        // Update audio state based on the received message
        if (m.isOxygenMaskAudioPlaying)
        {
            OxygenMaskSound.Play();
        }
        else
        {
            OxygenMaskSound.Stop();
        }

        if (m.isBreathingAudioPlaying)
        {
            breathingSound.Play();
        }
        else
        {
            breathingSound.Stop();
        }
    }
}
