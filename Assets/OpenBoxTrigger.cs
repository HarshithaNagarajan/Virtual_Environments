using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OpenBoxTrigger : MonoBehaviour
{
    Animator boxOpen;
    private bool isSelected = false;
    private XRBaseInteractable interactable;

     void Start()
    {
        boxOpen = GetComponent<Animator>();
        interactable = GetComponent<XRBaseInteractable>();
        //  interactable.activated.AddListener(XRBaseInteractor);

        // Add event listeners for selection events
        interactable.selectEntered.AddListener(OnSelectEnter);
        interactable.selectExited.AddListener(OnSelectExit);
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
}
