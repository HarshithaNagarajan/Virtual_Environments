using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using UnityEngine.XR.Interaction.Toolkit;

public class Epipen : MonoBehaviour
{
    XRGrabInteractable interactable;
    NetworkContext context;
    Transform parent;

    public int token;

    // Does this instance of the Component control the transforms for everyone?
    public bool isOwner;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        interactable = GetComponent<XRGrabInteractable>();
        interactable.firstSelectEntered.AddListener(OnPickedUp);
        interactable.lastSelectExited.AddListener(OnDropped);
        context = NetworkScene.Register(this);
        token = Random.Range(1, 10000);
        isOwner = true;
    }

    Vector3 lastPosition;

    void OnPickedUp(SelectEnterEventArgs ev)
    {
        Debug.Log("Picked up");
        TakeOwnership();
    }

    void OnDropped(SelectExitEventArgs ev)
    {
        Debug.Log("Dropped");
        transform.parent = parent;
        GetComponent<Rigidbody>().isKinematic = false;

    }

    private struct Message
    {
        public Vector3 position;
        public int token;
    }

    // When a Component Instance takes Ownership, that Peer decides the position
    // for everyone, either through the VR Controller or through its local Physics
    // Engine
    void TakeOwnership()
    {
        token++;
        isOwner = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isOwner)
        {
            Message m = new Message();
            m.position = this.transform.localPosition;
            m.token = token;
            context.SendJson(m);
        }
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage m)
    {
        var message = m.FromJson<Message>();
        transform.localPosition = message.position;
        if(message.token > token)
        {
            isOwner = false;
            token = message.token;
            GetComponent<Rigidbody>().isKinematic = true;
        }
        Debug.Log(gameObject.name + " Updated");
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if(lastPosition != transform.localPosition)
    //     {
    //         lastPosition = transform.localPosition;
    //         context.SendJson(new Message()
    //         {
    //             position = transform.localPosition
    //         });
    //     }
    // }

    // private struct Message
    // {
    //     public Vector3 position;
    // }

    // public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    // {
    //     // Parse the message
    //     var m = message.FromJson<Message>();

    //     // Use the message to update the Component
    //     transform.localPosition = m.position;

    //     // Make sure the logic in Update doesn't trigger as a result of this message
    //     lastPosition = transform.localPosition;
    // }
}
