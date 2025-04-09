using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneShotAttack : MonoBehaviour
{
    public bool Enabled;
    public float Delay;
    public GameObject BulletPrefab;

    private float timer;
    public void Shoot()
    {
        
        Instantiate(BulletPrefab, transform.position, Quaternion.identity);
    }
    private void Update()
    {
        if (!Enabled) return;
       timer += Time.deltaTime;
        if (timer > Delay)
        {
            timer = 0;
            Shoot();
        }
    }
}
