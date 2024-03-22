using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerAnimations : MonoBehaviour
{
    public GameObject[] AnimationsToTrigger;
    private Button myButton;
    void Start()
    {
        myButton = GetComponent<Button>();
        if (myButton != null)
        {
            myButton.onClick.AddListener(() => onClick());
        }
    }
    public void onClick()
    {
        for (int i = 0; i < AnimationsToTrigger.Length; i++)
        {
            AnimationsToTrigger[i].GetComponent<Animator>().SetBool("Begin", true);
        }
    }

}
