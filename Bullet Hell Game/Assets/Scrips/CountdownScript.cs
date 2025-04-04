using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownScript : MonoBehaviour
{
    public int Duration;
    float Timer;
    TextMeshProUGUI Text;
    private void Start()
    {
        Time.timeScale = 0f;
        Text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        Timer = 0;
    }
    private void Update()
    {
        Timer += Time.unscaledDeltaTime;
        int remainingTime = Mathf.CeilToInt(Duration - Timer);
        Text.text = remainingTime.ToString();
       
        if (Timer > Duration)
        {
            Time.timeScale = 1f;
            Camera.main.GetComponent<GameTimeScript>().SetTime();
            gameObject.SetActive(false);
        }
    }
}
