using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RayBossAttackPattern : MonoBehaviour
{
    public GameObject RayPrefab;
    public GameObject WarningSignPrefab;
    public Vector2 MinMaxDelay;
    public float WarningDelay;
    public float WarningCoordinate;
    public GameObject player;
    public float RayDuration;
    public ConnectedRay[] Connecteds;

    float Delay;
    float ElapsedTime;
    GameObject WarningSign;
    GameObject Ray;
    // Start is called before the first frame update
    void Start()
    {
        Delay = Random.Range(MinMaxDelay.x, MinMaxDelay.y);
    }

    // Update is called once per frame
    private void Update()
    {
        ElapsedTime += Time.deltaTime;

        if (ElapsedTime > Delay)
        {
            HandleWarningSign();
            HandleRay();
        }
    }

    private void HandleWarningSign()
    {
        // Instantiate warning sign if none exists and within warning delay window
        if (WarningSign == null && ElapsedTime < Delay + WarningDelay && Ray == null)
        {
            Vector3 warningPosition = new Vector3(player.transform.position.x, WarningY, 0);
            WarningSign = Instantiate(WarningSignPrefab, warningPosition, Quaternion.identity);
        }
    }

    private void HandleRay()
    {
        // Instantiate ray after warning delay
        if (ElapsedTime > Delay + WarningDelay )
        {
            if (Ray == null && ElapsedTime < Delay + WarningDelay + RayDuration)
            {
                Vector2 rayPosition = new Vector2(WarningSign.transform.position.x, 0);
                Ray = Instantiate(RayPrefab, rayPosition, Quaternion.identity);

                Destroy(WarningSign); // Remove warning sign after ray is instantiated
                WarningSign = null;
            }
            // Reset timer and destroy ray after its duration
            if (Ray != null && ElapsedTime > Delay + WarningDelay + RayDuration)
            {
                Destroy(Ray);
                Ray = null;
                TryResetCycle();
            }
        }
    }

    private void TryResetCycle()
    {
        if (Connecteds.All(x => x.CycleDone))
        {
            ElapsedTime = 0f;
            Delay = Random.Range(MinMaxDelay.x, MinMaxDelay.y);

            foreach (ConnectedRay connected in Connecteds)
            {
                connected.Reset();
            }
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector2(0, WarningY), 0.1f);
        Gizmos.DrawSphere(new Vector2(WarningX, 0), 0.1f);
    }

}


