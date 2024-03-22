using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using Unity.VisualScripting;

public class OximeterTrigger : MonoBehaviour
{
    NetworkContext context;

    public bool hasTriggered = false;
    public bool isAudioPlaying = false; // Track whether audio is playing

    public static OximeterTrigger instance;
    public OximeterReadingAnimator readingAnimator;
    public int OximeterReading = 95;
    public GameObject OximeterPanel;
    void Start()
    {
        context = NetworkScene.Register(this);
        OximeterPanel = GameObject.FindWithTag("OximeterReadingsPanel");
    }

    void OnTriggerEnter(Collider other)
    {
        if (OximeterPanel != null && other.gameObject.tag != "OtherMedical")
            {
                OximeterPanel.SetActive(true); // Activate the Canvas panel
            }

        if (other.gameObject.tag == "PatientIndexFinger")
        {
            
            if (!TriggerScript.instance.hasTriggered)
            {
                Debug.Log("Not hit by an Epipen yet ");
                OximeterReadingAnimator.instance.Value = 89;
                //set oximeterReading low
            }
            else if (TriggerScript.instance.hasTriggered && !OxygenMaskTrigger.instance.hasTriggered)
            {
                Debug.Log("Just hit by an Epipen yet ");
                //set oximeterReading slightly higher
                OximeterReadingAnimator.instance.Value = 92;

            }
            else if (TriggerScript.instance.hasTriggered && OxygenMaskTrigger.instance.hasTriggered)
            {
                Debug.Log("After hit by oxygen mask");
                //set oximeterReading high
                OximeterReadingAnimator.instance.Value = 98;
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
        if (OximeterReadingAnimator.instance != null)
        {
            OximeterReadingAnimator.instance.Value = 0;

        }
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
        OximeterReadingAnimator.instance.Value = m.OximeterReading;
    }
}
