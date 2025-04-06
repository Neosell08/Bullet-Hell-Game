using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelButton : MonoBehaviour
{
    public void StartLevel()
    {
        transform.parent.GetComponent<StartUIScript>().StartLevel(); 
    }
}
