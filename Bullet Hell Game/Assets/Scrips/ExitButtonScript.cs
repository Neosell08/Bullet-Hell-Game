using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButtonScript : MonoBehaviour
{
    public string ExitTo = "MainMenu";
    public void ExitGame()
    {
        if (ExitTo == "")
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(ExitTo);
        }
    }
}
