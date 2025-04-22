using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndExplosionParticleScript : MonoBehaviour
{
    public Vector2 PitchRange;

    AudioSource sfx;
    // Start is called before the first frame update
    void Start()
    {
        sfx = GetComponent<AudioSource>();
        sfx.pitch = Random.Range(PitchRange.x, PitchRange.y);
    }

   
}
