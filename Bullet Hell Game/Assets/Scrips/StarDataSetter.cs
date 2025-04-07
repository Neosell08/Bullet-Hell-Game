using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StarDataSetter : MonoBehaviour
{
    [System.Serializable]
    public class StarTimes
    {
        public string scene;
        public float[] times;
    }
    public StarTimes[] SceneStarTimes;
    void Start()
    {
        for (int i = 0; i < SceneStarTimes.Count(); i++)
        {
            string scene = SceneStarTimes[i].scene;
            float[] times = SceneStarTimes[i].times;
            for (int j = 0; j < times.Length; j++)
            {
                PlayerPrefs.SetFloat(scene + "StarTime" + Convert.ToString(j), times[j]);
            }
        }
    }

}
