using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverForInstructions : MonoBehaviour
{
    public GameObject PatientInfo;

    public void OnHoverEnter()
    {
        PatientInfo.SetActive(true);
        print("Hovering");
    }

    public void OnHoverExit()
    {
        PatientInfo.SetActive(false);
    }
}
