using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Geometry;
using UnityEngine.XR.Interaction.Toolkit;


public class MyNetworkedObject : MonoBehaviour
{
    NetworkContext context;
    PositionRotation lastSentPose;
    // public bool isOwner;




    // Start is called before the first frame update
    void Start()
    {
        context = NetworkScene.Register(this);
        var grab = GetComponent<XRGrabInteractable>();
        grab.activated.AddListener(XRGrabInteractable_Activated);
    }

    public void XRGrabInteractable_Activated(ActivateEventArgs eventArgs)
    {

        // Force the interactor(hand) to drop the firework
        var interactor = (XRBaseInteractor)eventArgs.interactorObject;
        interactor.allowSelect = false;
        var interactable = (XRGrabInteractable)eventArgs.interactableObject;
        interactable.enabled = false;
        interactor.allowSelect = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Only the owner should send updates
        var currentPose = Transforms.ToLocal(transform, context.Scene.transform);
        if (!currentPose.Equals(lastSentPose))
        {
            var message = new Message();
            message.pose = currentPose;
            context.SendJson(message);
            lastSentPose = currentPose;
        }
            
    }

    private struct Message
    {
        public PositionRotation pose;
        // public int token; // Token for ownership logic
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var m = message.FromJson<Message>();

        // Update the object only if the incoming token is higher
        var pose = Transforms.ToWorld(m.pose, context.Scene.transform);
        transform.position = pose.position;
        transform.rotation = pose.rotation;


    }
}