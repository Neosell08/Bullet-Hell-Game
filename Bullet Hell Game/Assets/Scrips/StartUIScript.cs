using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUIScript : MonoBehaviour
{
    public AudioSource BackgroundMusic;
    public GameObject Countdown;
    private void Start()
    {
        Time.timeScale = 0f;
    }
    public void StartLevel()
    {
        Time.timeScale = 1f;
        BackgroundMusic.Play();
        Countdown.SetActive(true);
        gameObject.SetActive(false);
    }
}
