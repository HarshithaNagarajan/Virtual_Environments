using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Geometry;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkAnimation : MonoBehaviour
{
    NetworkContext context;
    private Animator animator; 

    public string startParam;

    public bool triggered = false;

    public bool loop = false;

    void Start()
    {
        context = NetworkScene.Register(this);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool trigger = animator.GetBool(startParam);

        if (loop && triggered != trigger){
            var message = new Message();
            message.triggerAnimation = trigger;
            context.SendJson(message); 
            triggered = trigger;
            print("triggered loop");
        }

        if (!loop && !triggered && trigger){
            var message = new Message();
            message.triggerAnimation = trigger;
            context.SendJson(message); 
            triggered = true;
            print("triggered non");
        }
    }

    private struct Message
    {
        public bool triggerAnimation;
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var m = message.FromJson<Message>();
        animator.SetBool(startParam, m.triggerAnimation);
    }
}
