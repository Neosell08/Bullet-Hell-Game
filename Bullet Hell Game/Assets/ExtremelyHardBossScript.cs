using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtremelyHardBossScript : MonoBehaviour
{
    public RayInfo Ray;
    public float outsideY;
    public float Speed;

    public float P3STDelay;
    public float P3BounceDelay;



    int Phase;
    BossHealth health;
    RayBossAttackPattern RayAttack;
    BounceBulletAttack BounceAttack;
    SphereProjectileBossAttack SphereAttack;
    OneShotAttack ShootTowardsAttack;
    float timer;
    Vector3 tempPos;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<BossHealth>();
        RayAttack = GetComponent<RayBossAttackPattern>();
        BounceAttack = GetComponent<BounceBulletAttack>();
        SphereAttack = GetComponent<SphereProjectileBossAttack>();
        ShootTowardsAttack = GetComponent<OneShotAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Phase == 0)
        {
            if (health.hp < 50)
            {
                StartCoroutine(FlyOutOfSceen());
                ChangePhase();
            }
        }
        else if (Phase == 1)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            if (timer > 15)
            {
                StartCoroutine(FlyInToScreen());
                ChangePhase();
            }
        }
    }
    public IEnumerator FlyOutOfSceen()
    {
        tempPos = transform.position;
        while (transform.position.y < outsideY)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * Speed);
            yield return null;
        }
    }
    public IEnumerator FlyInToScreen()
    {
        while (transform.position.y > tempPos.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * Speed);
            yield return null;
        }

    }
    public void ChangePhase(int p = -1)
    {
        if (p != -1)
        {
            Phase = p;
        }
        else
        {
            Phase++;
        }
        if (Phase == 1)
        {

            SphereAttack.enabled = false;
            BounceAttack.enabled = false;
            RayAttack.enabled = true;
        }
        else if (Phase == 2)
        {
            SphereAttack.enabled = true;
            BounceAttack.enabled = true;
            RayAttack.SetRays(new List<RayInfo> { Ray });
            ShootTowardsAttack.Delay = P3STDelay;
            BounceAttack.Delay = P3STDelay;
        }
        else if (Phase == 3)
        {
            
        }
    }
}
