using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndUIScript : MonoBehaviour
{
    public AudioSource BackgroundMusic;
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Activate()
    {
        BackgroundMusic.Stop();
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
