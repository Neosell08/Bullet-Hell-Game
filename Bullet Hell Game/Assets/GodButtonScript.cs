using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GodButtonScript : MonoBehaviour
{
    public void SetGodModeData()
    {
        PlayerPrefs.SetFloat("Boss1Record", 0);
        PlayerPrefs.SetFloat("Boss1HardRecord", 0);
        PlayerPrefs.SetFloat("Boss1ExtremelyHardRecord", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
