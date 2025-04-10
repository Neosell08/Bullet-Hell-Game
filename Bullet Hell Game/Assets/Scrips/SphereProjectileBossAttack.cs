using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereProjectileBossAttack : MonoBehaviour, IBurstable
{
    public GameObject BulletPrefab;
    public Vector2 MinMaxDelay;
    public float BulletAmount;
    public bool RandomRotation;

    float Timer;

    [HideInInspector] public float Delay { get; set; }
    [HideInInspector] public bool DoBurst { get; set; }


    void Start()
    {
        if (!DoBurst)
        {
            Delay = Random.Range(MinMaxDelay.x, MinMaxDelay.y);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > Delay)
        {
            Timer = 0;
            if (!DoBurst)
            {
                Delay = Random.Range(MinMaxDelay.x, MinMaxDelay.y);
            }
            float randomRotation = !RandomRotation ? Random.Range(0f, 360f) : 0f;
            for (int i = 0; i < BulletAmount; i++)
            {
                float rotation = (360 * i / BulletAmount) + randomRotation;
                Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, rotation));
            }
        }
    }
}
