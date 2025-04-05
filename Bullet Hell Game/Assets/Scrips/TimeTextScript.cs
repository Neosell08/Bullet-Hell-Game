using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeTextScript : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = "Time: " + Globals.TimeToString(Time.time - Camera.main.GetComponent<GameTimeScript>().StartTime);
    }
    
}
