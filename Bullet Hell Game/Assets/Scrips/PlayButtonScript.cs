using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonScript : MonoBehaviour
{
    public void Play()
    {
        transform.parent.GetComponent<EndUIScript>().Restart();
    }
}
