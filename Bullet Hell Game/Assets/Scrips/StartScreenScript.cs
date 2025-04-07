using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenScript : MonoBehaviour
{
    public float Delay;
    public string Scene;

    float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > Delay) { SceneManager.LoadScene(Scene); }
    }
}
