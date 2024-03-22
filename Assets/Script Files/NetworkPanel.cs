using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Geometry;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;


public class NetworkPanel : MonoBehaviour
{
    NetworkContext context;
    private Button myButton;
    public GameObject[] objectsToToggle;    

    void Start()
    {
        context = NetworkScene.Register(this);
        myButton = GetComponent<Button>();
        if (myButton != null)
        {
            myButton.onClick.AddListener(() => onClick());
        }
    }

    public void onClick()
    {
        var message = new Message();
        message.activeSelves = new bool[objectsToToggle.Length];
        for (int i = 0; i < objectsToToggle.Length; i++)
        {
            message.activeSelves[i] = !objectsToToggle[i].activeSelf;
            objectsToToggle[i].SetActive(message.activeSelves[i]);
        }
        context.SendJson(message);
    }


    private struct Message
    {
        public bool[] activeSelves;
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var m = message.FromJson<Message>();
        for (int i = 0; i < objectsToToggle.Length; i++)
        {
            objectsToToggle[i].SetActive(m.activeSelves[i]);
        }
    }
}