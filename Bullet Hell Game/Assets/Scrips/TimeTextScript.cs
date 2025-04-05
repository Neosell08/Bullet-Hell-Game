using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeTextScript : MonoBehaviour
{
    private void OnEnable()
    {
        float time = Time.time - Camera.main.GetComponent<GameTimeScript>().StartTime;
        GetComponent<TextMeshProUGUI>().text = "Time: " + Globals.TimeToString(time);
        
    }
    
}
