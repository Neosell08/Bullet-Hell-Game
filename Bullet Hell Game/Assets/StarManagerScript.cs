using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarManagerScript : MonoBehaviour
{
    public GameObject[] Stars;
    public float[] StarTimes;
    public Sprite FullStar;
    
    public void OnEnable()
    {
        
        float time = Mathf.Min(Time.time - Camera.main.GetComponent<GameTimeScript>().StartTime, PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "Record"));
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "Record", time);
        for (int i = 0; i < Stars.Length; i++)
        {
            
            if (StarTimes[i] >= time)
            {
                Stars[i].GetComponent<Image>().sprite = FullStar;
            }
        }
    }
}
