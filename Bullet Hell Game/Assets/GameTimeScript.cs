using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeScript : MonoBehaviour
{
    [HideInInspector] public float StartTime;
    public void Start()
    {
        SetTime();
    }
    public void SetTime()
    {
        StartTime = Time.time;
    }
}
