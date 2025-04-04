using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereProjectileBossAttack : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Vector2 MinMaxDelay;
    public float BulletAmount;

    float Delay;
    float Timer;
    void Start()
    {
        Delay = Random.Range(MinMaxDelay.x, MinMaxDelay.y);
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > Delay)
        {
            Timer = 0;
            Delay = Random.Range(MinMaxDelay.x, MinMaxDelay.y);
            float randomRotation = UnityEngine.Random.Range(0f, 360f);
            for (int i = 0; i < BulletAmount; i++)
            {
                float rotation = (360 * i / BulletAmount) + randomRotation;
                Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, rotation));
            }
        }
    }
}
