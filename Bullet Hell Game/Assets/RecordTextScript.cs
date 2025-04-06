using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecordTextScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "Record"))
        {
            text.text = "Record: " + Globals.TimeToString(PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "Record"));
        }
    }

    
}
