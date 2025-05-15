using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class HardBossScript : MonoBehaviour
{
    /// <summary>
    /// Points where the boss should move
    /// </summary>
    public Vector2[] MovePoints;
    public RayBossAttackPattern RayAttack1;
    public RayBossAttackPattern RayAttack2;
    public GameObject P3BulletPrefab;
    public Vector2 P3MinMaxRange;
    public int P3BulletAmount;
    public float OutsideY;
    public float Phase2Timer;
    public float Phase2Health;
    public Vector2 Phase3MinMaxRays;


    /// <summary>
    /// At what index the current target point is 
    /// </summary>
    int CurPointIndex;
    Vector2 tempPos;
    Vector2 MoveDir;
    SphereProjectileBossAttack SphereAttack;
    int Phase;
    BossHealth health;
    float timer;

    public float Speed;
    public float LerpSpeed;
    public float NewPointDistanceThreshold;

    bool IsMovingDown = false;
    void Start()
    {
        SphereAttack = GetComponent<SphereProjectileBossAttack>();
        health = GetComponent<BossHealth>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Phase == 0)
        {
            DoMovePattern();
            if (health.hp < Phase2Health)
            {
                Debug.Log("hello");
                StartCoroutine(FlyOutOfSceen());
                ChangePhase();
            }
        }
        else if (Phase == 1)
        {
            timer += Time.deltaTime;
            if (timer > Phase2Timer)
            {
                StartCoroutine(FlyInToScreen());
                ChangePhase();
            }
        }
        else
        {
            DoMovePattern();
        }
    }

    public void DoMovePattern()
    {
        if (IsMovingDown) return;
        Vector2 targetDir = MovePoints[CurPointIndex] - (Vector2)transform.position;
        MoveDir = Vector2.Lerp(MoveDir, targetDir, LerpSpeed);
        MoveDir.Normalize();

        transform.position += (Vector3)MoveDir * Speed * Time.deltaTime;


        if (Vector2.Distance(transform.position, MovePoints[CurPointIndex]) < NewPointDistanceThreshold)
        {
            CurPointIndex = BossScript.CircularClamp(CurPointIndex + 1, 0, MovePoints.Length - 1);
        }
    }
    public IEnumerator FlyOutOfSceen()
    {
        tempPos = transform.position;
        Vector2 target = new Vector2(0, OutsideY);
        Vector3 dir = (target - tempPos).normalized;
        Debug.Log(dir);
        while (transform.position.y < OutsideY)
        {
            transform.position += dir * Speed * Time.deltaTime;
            yield return null;
        }
    }
    public IEnumerator FlyInToScreen()
    {
        IsMovingDown = true;
        Vector3 dir = (new Vector2(0, tempPos.y) - (Vector2)transform.position).normalized;
        while (transform.position.y > tempPos.y)
        {
            transform.position += dir * Speed * Time.deltaTime;
            yield return null;
        }
        IsMovingDown = false;
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
            RayAttack1.StopAllRays();
            RayAttack1.enabled = false;
            RayAttack2.enabled = true;
            SphereAttack.enabled = false;
            
        }
        else if (Phase == 2)
        {
            RayAttack2.StopAllRays();
            SphereAttack.enabled = true;
            RayAttack1.enabled = true;
            RayAttack2.enabled = false;
            RayAttack1.RestartRays();

            SphereAttack.BulletAmount = P3BulletAmount;
            SphereAttack.BulletPrefab = P3BulletPrefab;
            SphereAttack.MinMaxDelay = P3MinMaxRange;
            List<RayInfo> rays = RayAttack1.Rays;
            rays[0] = new RayInfo(Phase3MinMaxRays, rays[0].WarningDelay, rays[0].WarningCoord, rays[0].RayDuration, rays[0].PreDefinedCoord, rays[0].UsePreDefindedCoord, rays[0].ConnectedToPrevious, rays[0].Sideways);
            RayAttack1.SetRays(rays);
        }
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2? lastPoint = null;
        for (int i = 0; i < MovePoints.Length; i++)
        {
            if (i > 0)
            {
                Gizmos.color = Color.yellow;
            }
            Gizmos.DrawSphere(MovePoints[i], 0.1f);
            if (lastPoint != null)
            {
                Gizmos.DrawLine(MovePoints[i], lastPoint.Value);
            }
            lastPoint = MovePoints[i];
        }
    }
}


