using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonScript : MonoBehaviour
{
    public string Scene;
    public TextMeshProUGUI Text;
    public void ChangeScene()
    {
        SceneManager.LoadScene(Scene);
    }
    public void Start()
    {
        if (!PlayerPrefs.HasKey(Scene + "Record"))
        {
            PlayerPrefs.SetFloat(Scene + "Record", float.PositiveInfinity);
            Text.text = "Record:" + Globals.TimeToString(float.PositiveInfinity);
        }
        else
        {
            Text.text = "Record:" + Globals.TimeToString(PlayerPrefs.GetFloat(Scene + "Record"));
        }
    }
}
