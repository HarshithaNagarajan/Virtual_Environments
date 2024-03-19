using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disappear_begin : MonoBehaviour
{
    public GameObject begin_button;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void whenBeginClicked()
    {
        if (begin_button.activeInHierarchy == true)
        {
            begin_button.SetActive(false);
        }
        else
        {
            begin_button.SetActive(true);
        }
    }

}
