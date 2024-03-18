using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBox : MonoBehaviour
{
    // private bool mIsOpen = false;
    Animator FirstAidBox;

    void Start(){
        FirstAidBox = this.transform.parent.GetComponent<Animator>();
        FirstAidBox.SetBool("Open", true);
    }

    // private void OnTriggerEnter(Collider other) {
    //     FirstAidBox.SetBool("Open", true);
    // }

    // private void OnTriggerExit(Collider other) {
    //     FirstAidBox.SetBool("Open", false);
    // }

    

// Start is called before the first frame
    // protected virtual void OnSelectEnter(XRBaseInteractor interactor){
    //     mIsOpen = !mIsOpen;

    //     FirstAidBox = this.transform.parent.GetComponent<Animator>();
    //     FirstAidBox?.SetBool("Open", mIsOpen);
    // }
}
