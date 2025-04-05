using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonScript : MonoBehaviour
{
    public string Scene;
    public TextMeshProUGUI Text;
    public float[] StarTimes;
    public GameObject[] Stars;
    public Sprite FullStarTexture;
    public void ChangeScene()
    {
        SceneManager.LoadScene(Scene);
    }
    public void Start()
    {
        
        float record;
        if (!PlayerPrefs.HasKey(Scene + "Record"))
        {
            PlayerPrefs.SetFloat(Scene + "Record", float.PositiveInfinity);
            record = float.PositiveInfinity;
            
        }
        else
        {
            record = PlayerPrefs.GetFloat(Scene + "Record");
        }
        Text.text = "Record:" + Globals.TimeToString(record);
        Debug.Log(record);
        for (int i = 0; i < Stars.Length; i++)
        {
            if (record <= StarTimes[i])
            {
                Stars[i].GetComponent<Image>().sprite = FullStarTexture;
            }
        }
    }
}
