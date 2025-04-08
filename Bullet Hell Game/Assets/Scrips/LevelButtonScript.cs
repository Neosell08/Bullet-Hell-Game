using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static StarDataSetter;

public class LevelButtonScript : MonoBehaviour
{
    public string Scene;
    public TextMeshProUGUI Text;
    public GameObject[] Stars;
    public Sprite FullStarTexture;
    [Header("Locking")]
    public string RequiredTimeScene;
    public float RequiredStars;
    public GameObject Lock;

    bool isLocked;
    public void ChangeScene()
    {
        SceneManager.LoadScene(Scene);
    }
    public void Start()
    {
        
        if (RequiredTimeScene != "" &&(!PlayerPrefs.HasKey(RequiredTimeScene + "Record") || PlayerPrefs.GetFloat(RequiredTimeScene + "Record") > PlayerPrefs.GetFloat(RequiredTimeScene + "StarTime" + Convert.ToString(RequiredStars))))
        {
            isLocked = true;
            GetComponent<Button>().interactable = false;
            Lock.SetActive(true);
        }

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
        for (int i = 0; i < Stars.Length; i++)

        {
            float starTime = PlayerPrefs.GetFloat(Scene + "StarTime" + Convert.ToString(i));

            Debug.Log(starTime);
            if (record <= starTime)
            {
                Stars[i].GetComponent<Image>().sprite = FullStarTexture;
            }
        }
    }
}
