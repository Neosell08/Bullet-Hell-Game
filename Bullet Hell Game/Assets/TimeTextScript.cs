using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeTextScript : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = "Time: " + TimeToString(Time.time - Camera.main.GetComponent<GameTimeScript>().StartTime);
    }
    public static string TimeToString(float seconds)
    {
        if (seconds > 3599) { return "A lot"; }


        float mins = MathF.Floor(seconds / 60);
        seconds = MathF.Floor(seconds % 60);
        return (mins < 10 ? "0" : "") + mins + " : " + (seconds < 10 ? "0" : "") + seconds;
    }
}
