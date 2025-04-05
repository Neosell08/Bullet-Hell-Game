using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RayBossAttackPattern : MonoBehaviour
{
    public GameObject RayPrefab;
    public GameObject WarningSignPrefab;
    public Vector2 MinMaxDelay;
    public float WarningDelay;
    public float WarningY;
    public GameObject player;
    public float RayDuration;

    float Delay;
    float Timer;
    GameObject Warning;
    GameObject Ray;
    // Start is called before the first frame update
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
            if (Warning == null && Timer < WarningDelay + Delay)
            {
                Warning = Instantiate(WarningSignPrefab, new Vector3(player.transform.position.x, WarningY), Quaternion.Euler(0, 0, 0));
            }

            if (Timer > WarningDelay + Delay)
            {
                
                if (Ray == null)
                {
                    Ray = Instantiate(RayPrefab, new Vector2(Warning.transform.position.x, 0), Quaternion.Euler(0, 0, 0));
                    Destroy(Warning);
                    Warning = null;
                }

                if (Timer > WarningDelay + Delay + RayDuration)
                {
                    Timer = 0;
                    Delay = Random.Range(MinMaxDelay.x, MinMaxDelay.y);
                    Destroy(Ray);
                    Ray = null;
                }
                
            }

        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector2(0, WarningY), 0.1f);
    }
}
