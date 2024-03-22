using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class OximeterReadingAnimator : MonoBehaviour
{
    public TextMeshProUGUI topReading;
    public int countFPS = 30;
    public float duration = 1f;
    private int value;
    public string numberFormat = "N0";
    public static OximeterReadingAnimator instance;

    private Coroutine countingCoroutine;


void Awake() {
        if (instance == null ){
            instance = this;
        }
    }
    public int Value {
        get {
            return value;
        }
        set {
            UpdateText(value);
            this.value = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    //      if (topReading == null)
    // {
        topReading = GetComponent<TextMeshProUGUI>();
    // }
        // if (OximeterTrigger.instance != null)
        // {
        //     topReading = OximeterTrigger.instance.OximeterReading;
        // }
        // else
        // {
        //     topReading.text = "95"; // Default value if OximeterTrigger or its OximeterReading is not set
        // }
    }

    // Update is called once per frame

    private void UpdateText(int newValue)
    {
        if (countingCoroutine != null)
        {
            StopCoroutine(countingCoroutine);
        }
        countingCoroutine = StartCoroutine(CountText(newValue));
    }

    private IEnumerator CountText(int newValue)
    {
        // WaitForSeconds wait = new WaitForSeconds(1f / countFPS);
        // int oldValue = value;

        // // Calculate the step count between the old and new values
        // int stepCount = Mathf.Abs(newValue - oldValue);

        // // Determine the direction of counting
        // int increment = (int)Mathf.Sign(newValue - oldValue);

        // // Start counting from the old value
        // int currentValue = oldValue;

        // for (int i = 0; i <= stepCount; i++)
        // {
        //     // Update the text with the current value
        //     topReading.text = currentValue.ToString(numberFormat);

        //     // Increment or decrement the current value
        //     currentValue += increment;
        //     Debug.Log("Current Reading is "+ topReading.text);
        //     yield return wait;
        // }
        WaitForSeconds wait = new WaitForSeconds(1f / countFPS);
        int previousValue = value;

        // Calculate the step amount as the difference between the new and previous values
        int stepAmount = newValue - previousValue;

        // Determine the direction of counting
        int increment = (int)Mathf.Sign(stepAmount);

        while (previousValue != newValue)
        {
            // Increment the previous value by the step amount
            previousValue += increment;

            // Ensure that the previous value does not overshoot the target value
            if (increment > 0 && previousValue > newValue)
            {
                previousValue = newValue;
            }
            else if (increment < 0 && previousValue < newValue)
            {
                previousValue = newValue;
            }

            // Update the text
            // topReading.text = previousValue.ToString(numberFormat);
            topReading.SetText(previousValue.ToString(numberFormat));
           
            Debug.Log("Current Reading is "+ topReading.text);
            yield return wait;
        }
    }
}





