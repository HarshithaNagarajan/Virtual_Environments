using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;

public class OximeterTrigger : MonoBehaviour
{
    NetworkContext context;
    public int OximeterReading;
    public bool hasTriggered = false;
    public bool isAudioPlaying = false; // Track whether audio is playing

    void Start()
    {
        context = NetworkScene.Register(this);
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Oximeter")
        {
            if (!TriggerScript.instance.hasTriggered)
            {
                Debug.Log("Not hit by an Epipen yet ");
                //set oximeterReading low
            }
            else  if (TriggerScript.instance.hasTriggered && !OxygenMaskTrigger.instance.hasTriggered)
            {
                Debug.Log("Just hit by an Epipen yet ");
                //set oximeterReading slightly higher

            }
            else if (TriggerScript.instance.hasTriggered && OxygenMaskTrigger.instance.hasTriggered)
            {
                Debug.Log("After hit by oxygen mask");
                //set oximeterReading high
            }
            

            // audioSource.Play();
            // // epipenAnimator.SetTrigger("Epipen");
            // hasTriggered = true;

            // // Update audio state and send network message
            // isAudioPlaying = true;
            SendNetworkMessage();
        }
    }

    void OnTriggerExit(Collider other)
    {
        //set OximeteReading to 0
        OximeterReading = 0;
    }

    void SendNetworkMessage()
    {
        var message = new Message();
        message.OximeterReading = OximeterReading;
        context.SendJson(message);
    }

    private struct Message
    {
        public int OximeterReading;
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var m = message.FromJson<Message>();
        // Update audio state based on the received message
        
    }
}
