using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdatingTimerScript : MonoBehaviour
{
    TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        text.text = Globals.TimeToString(Time.time - Camera.main.GetComponent<GameTimeScript>().StartTime);
    }
}
