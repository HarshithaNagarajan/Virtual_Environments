using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Ubiq.Messaging;
using Ubiq.Geometry;

public class OpenBoxTrigger : MonoBehaviour
{
    Animator boxOpen;
    private bool isSelected = false;
    private XRBaseInteractable interactable;
    NetworkContext context;
    public bool isOwner;

     void Start()
    {
        context = NetworkScene.Register(this);
        boxOpen = GetComponent<Animator>();
        interactable = GetComponent<XRBaseInteractable>();
        // Add event listeners for selection events
        interactable.selectEntered.AddListener(OnSelectEnter);
        interactable.selectExited.AddListener(OnSelectExit);
        interactable.activated.AddListener(XRBaseController_Activated);
    }

        
    public void XRBaseController_Activated(ActivateEventArgs eventArgs)
    {

        // Force the interactor(hand) to drop the firework
        var interactor = (XRBaseInteractor)eventArgs.interactorObject;
        interactor.allowSelect = false;
        var interactable = (XRGrabInteractable)eventArgs.interactableObject;
        interactable.enabled = false;
        interactor.allowSelect = true;
    }
    void Update()
    {
        // Only the owner should send updates
        if(isOwner)
        {
           
            var message = new Message();
            message.pose = Transforms.ToLocal(transform, context.Scene.transform);
            context.SendJson(message);

        }
    }

    // Called when the object is selected by the controller
    public void OnSelectEnter(SelectEnterEventArgs interactor)
    {
        isSelected = !isSelected;
        boxOpen.SetBool("OpenKit", isSelected);
    }

    // Called when the object is deselected by the controller
    public void OnSelectExit(SelectExitEventArgs interactor)
    {
        // isSelected = false;
        // boxOpen.SetBool("OpenKit", isSelected);
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
