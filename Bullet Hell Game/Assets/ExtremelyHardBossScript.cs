using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtremelyHardBossScript : MonoBehaviour
{
    int Phase;
    BossHealth health;
    RayBossAttackPattern RayAttack;
    BounceBulletAttack BounceAttack;
    SphereProjectileBossAttack SphereAttack;
    public RayInfo Ray;

    float timer;
    Vector3 tempPos;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<BossHealth>();
        RayAttack = GetComponent<RayBossAttackPattern>();
        BounceAttack = GetComponent<BounceBulletAttack>();
        SphereAttack = GetComponent<SphereProjectileBossAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Phase == 0)
        {
            if (health.hp < 20)
            {
                FlyOutOfSceen();
                Phase++;
            }
        }
        else if (Phase == 1)
        {
            timer += Time.deltaTime;
            if (timer > 15)
            {
                FlyInToScreen();
                Phase++;
            }
        }
    }
    public void FlyOutOfSceen()
    {
        tempPos = transform.position;
        transform.position = new Vector3(transform.position.x, 999999);
        SphereAttack.enabled = false;
        BounceAttack.enabled = false;
        RayAttack.enabled = true;
    }
    public void FlyInToScreen()
    {
        transform.position = tempPos;
        SphereAttack.enabled = true;
        BounceAttack.enabled = true;
        RayAttack.SetRays(new List<RayInfo> { Ray });
        
    }
}
