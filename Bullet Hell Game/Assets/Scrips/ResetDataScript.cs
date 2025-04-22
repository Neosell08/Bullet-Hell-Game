using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetDataScript : MonoBehaviour
{
    public string[] Scenes;
    public void ResetData()
    {
        foreach (string scene in Scenes)
        {
            PlayerPrefs.DeleteKey(scene + "Record");
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
