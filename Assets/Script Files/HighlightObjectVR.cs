using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObjectVR : MonoBehaviour
{
    private Outline outline;
   
    public void OnHoverEnter()
    {
        if (outline != null) 
        {
            outline.enabled = true;
            
        }
        else
        {
            outline = gameObject.AddComponent<Outline>();
            outline.enabled = true;
            gameObject.GetComponent<Outline>().OutlineColor = Color.green;
            gameObject.GetComponent<Outline>().OutlineWidth = 4.0f;
        }
        
    }

    public void OnHoverExit()
    {
        if (outline != null)
        {
            outline.enabled = false;
            
        }
    }

    public void onSelect()
    {
        if (outline != null)
        {
            outline.enabled = false;

        }
    }


}
