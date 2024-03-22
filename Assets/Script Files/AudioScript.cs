using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
public class ChokingAudio : MonoBehaviour

{
    public AudioSource chokingSound;
    public AudioSource breathingSound;
    Animator AvatarChoking;
    NetworkContext context;
    private bool isChokingAudioPlaying = false;
    private bool isBreathingAudioPlaying = false;
    // public TriggerScript instance; 
    // Start is called before the first frame update
    void Start()
    {
        context = NetworkScene.Register(this);
        AvatarChoking = GetComponent<Animator>();
        // epipenAnimator = GameObject.FindGameObjectWithTag("Epipen").GetComponent<Animator>();
        // chokingSound = GetComponent<AudioSource>();
        // breathingSound = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {

        if (!TriggerScript.instance.hasTriggered)
        {
            if (AvatarChoking.GetCurrentAnimatorStateInfo(0).IsTag("LayingDown"))
            {
                // Debug.Log("Playing Coughing Sound");
                isChokingAudioPlaying = true;
                chokingSound.Play();
            }
        }
        else
        {
            if (isChokingAudioPlaying)
            {
                chokingSound.Stop();
                isChokingAudioPlaying = false;
                if (!TriggerScript.instance.isEpipenAudioPlaying){
                    breathingSound.Play();
                    isBreathingAudioPlaying = true;
                }
                
            }
        }
        // if (!TriggerScript.instance.hasTriggered){
        //     Debug.Log("Epipen not triggered");
        //     // audioSource.Play();
        //     isChokingAudioPlaying = true;
        // } else {
        //     Debug.Log("Epipen was triggered");
        //     // audioSource.Stop();
        //     isChokingAudioPlaying = false;
        // }


    }

    void SendNetworkMessage()
    {
        var message = new Message();
        message.isChokingAudioPlaying = isChokingAudioPlaying;
        message.isBreathingAudioPlaying = isBreathingAudioPlaying;
        context.SendJson(message);
    }

    private struct Message
    {
        public bool isChokingAudioPlaying;
        public bool isBreathingAudioPlaying;
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var m = message.FromJson<Message>();
        // Update audio state based on the received message
        if (m.isChokingAudioPlaying)
        {
            chokingSound.Play();
        }
        else
        {
            chokingSound.Stop();
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
