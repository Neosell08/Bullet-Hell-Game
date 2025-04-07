using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class RayBossAttackPattern : MonoBehaviour
{
    public GameObject RayPrefab;
    public GameObject WarningSignPrefab;
    public Vector2 MinMaxDelay;
    public float WarningDelay;
    public float WarningCoord;
    public GameObject player;
    public float RayDuration;
    public bool Enabled;

    float Delay;
    float timer;
    List<Task> rayOperations;

    // Start is called before the first frame update
    void Start()
    {
        Delay = Random.Range(MinMaxDelay.x, MinMaxDelay.y);
    }

    // Update is called once per frame
    private void Update()
    {
        if (enabled)
        {
            timer += Time.deltaTime;

            if (timer > Delay)
            {
                MakeRay(false);
                timer = 0;
            }

        }
        
    }

    public async Task MakeRay(bool sideways, float? rayDuration = null, float? warningDelay = null, float? preDefinedcoord = null)
    {
        //prepare vars
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float playerCoord = sideways ? player.transform.position.y : player.transform.position.x;
        
        warningDelay = warningDelay ?? WarningDelay;
        float temp = preDefinedcoord ?? playerCoord;
        rayDuration = rayDuration ?? RayDuration;

        Vector2 warningPos = sideways ? new Vector2(WarningCoord, temp) : new Vector2(temp, WarningCoord);

        //spawn warning sign 
        GameObject warningSign = Instantiate(WarningSignPrefab, warningPos, Quaternion.identity);

        await Task.Delay((int)(warningDelay.Value * 1000));

        //spawn ray

        Quaternion rotation = sideways ? Quaternion.Euler(0, 0, 90) : Quaternion.identity;
        Vector2 rayPos = sideways ? new Vector2(0, warningSign.transform.position.y) : new Vector2(warningSign.transform.position.x, 0);
        Destroy(warningSign);
        GameObject ray = Instantiate(RayPrefab, rayPos, rotation);

        await Task.Delay((int)(rayDuration.Value * 1000));

        Destroy(ray);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector2(WarningCoord, WarningCoord), 0.1f);
    }

}


