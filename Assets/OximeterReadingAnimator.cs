using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OximeterReadingAnimator : MonoBehaviour
{
    public int OximeterReading;
    public TextMeshProUGUI TopReading;
    public int CountFPS = 30;
    public float duration = 1f;
    private int _value;
    public string NumberFormat = "N0";

    private Coroutine CountingCoroutine;

    public int Value {
        get {
            return  _value;
        }
        set{
            UpdateText(value);
            _value = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TopReading = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame

        private void UpdateText(int newValue){
        if (CountingCoroutine != null){
            StopCoroutine(CountText(newValue));
        } 
        CountingCoroutine = StartCoroutine(CountText(newValue));
        
    }

    private IEnumerator CountText(int newValue){
        WaitForSeconds Wait = new WaitForSeconds(1f / CountFPS);
        int previousValue = _value;
        int stepAmount;

        if(newValue - previousValue < 0){
            stepAmount = Mathf.FloorToInt(newValue - previousValue / (CountFPS * duration));
        } else {
            stepAmount = Mathf.CeilToInt(newValue - previousValue / (CountFPS * duration));

        } if (previousValue < newValue){
            while(previousValue < newValue){
                previousValue += stepAmount;
                if (previousValue > newValue){
                    previousValue = newValue;
                }

                TopReading.SetText(previousValue.ToString(NumberFormat));

                yield return Wait;
            }
        } else {
            while(previousValue > newValue){
                previousValue += stepAmount;
                if (previousValue < newValue){
                    previousValue = newValue;
                }

                TopReading.SetText(previousValue.ToString(NumberFormat));

                yield return Wait;
            }
        }
    }
}
