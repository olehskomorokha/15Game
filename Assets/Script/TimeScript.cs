using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeScript : MonoBehaviour
{
    [SerializeField] Text timeText;
    public int seconds, minutes;
    void Start()
    {
        AddSeconds();
    }
    private void AddSeconds()
    {
        seconds++;
        if(seconds > 59) 
        {
            minutes++;
            seconds = 0;
        }
        timeText.text = (minutes < 10?"0":"") + minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
        Invoke(nameof(AddSeconds), 1);
    }
    public void StopTimer()
    {
        CancelInvoke(nameof(AddSeconds));
        timeText.gameObject.SetActive(false);
    }

    public void ContinueTimer()
    {
        timeText.gameObject.SetActive(true);
        Invoke(nameof(AddSeconds), 1);
    }
}
