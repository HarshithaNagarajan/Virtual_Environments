using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbulanceController : MonoBehaviour
{
   public float speed = 30.0f;
    private bool STATE1 = false; // move forward 1
    private bool STATE2 = false; // turn right
    private bool STATE3 = false; // move forward after turn

    public GameObject[] charactersToReveal;

    public GameObject canvas;

    private CameraFollow cameraFollowScript;

    public Transform followTarget;

    public float timer = 0;

    public float TIMER1 = 5.35f;
    public float TIMER2 = 2.0f;

    void Start()
    {
        cameraFollowScript = Camera.main.GetComponent<CameraFollow>();
    }

    public void StartMoving()
    {
        STATE1 = true;
    }

    void Update()
    {
        if (STATE1)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            timer = timer + Time.deltaTime;
            canvas.SetActive(false);

            cameraFollowScript.target = followTarget;

            if (timer > TIMER1)
            {
                STATE1 = false;
                STATE2 = true;
                timer = 0;
                speed = 5.0f;
                canvas.SetActive(false);
            }
        }

        if (STATE2)
        {
            transform.Rotate(Vector3.up * 90);
            STATE2 = false;
            STATE3 = true;
        }

        if (STATE3)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            timer = timer + Time.deltaTime;

            if (timer > TIMER2)
            {
                STATE3 = false;
                foreach (GameObject character in charactersToReveal)
                {
                    if (character != null)
                    {
                        character.SetActive(true);
                        Animator characterAnimator = character.GetComponent<Animator>();
                        characterAnimator.SetBool("start", true);
                    }
                }
                cameraFollowScript.target = null;
            }
        }
    }
}

