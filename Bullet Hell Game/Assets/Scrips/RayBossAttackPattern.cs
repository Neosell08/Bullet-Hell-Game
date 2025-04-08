using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Jobs;
using Random = UnityEngine.Random;

[System.Serializable]
public struct RayInfo
{
    public Vector2 MinMaxDelay;
    public float WarningDelay;
    public float WarningCoord;
    public float RayDuration;
    public float PreDefinedCoord;
    public bool UsePreDefindedCoord;
    public bool ConnectedToPrevious;
    public bool Sideways;
}


public class RayBossAttackPattern : MonoBehaviour
{
    public GameObject RayPrefab;
    public GameObject WarningSignPrefab;
    public float WarningCoord;
    public GameObject player;

    float timer;
    //all information about rays
    [SerializeField] List<RayInfo> Rays = new List<RayInfo>();

    //the calculated delay of each ray
    private List<float> rayDelays = new List<float>();


    


    // Start is called before the first frame update
    void Start()
    {
        GenerateNewDelays();
        GenerateNewCountDowns();
    }
    

    // Update is called once per frame
    private void Update()
    {
       

    }
    //stop firing new rays of old phase
    public void StopAllRays()
    {
        StopAllCoroutines();
    }
    //set new ray schedule
    public void SetRays(List<RayInfo> rays)
    {
        Rays = rays;
        GenerateNewDelays();
        StopAllRays();
        GenerateNewCountDowns();
    }

    //set the delays of every ray
    void GenerateNewDelays()
    {
        rayDelays.Clear();
        for (int i = 0; i < Rays.Count; i++)
        {
            rayDelays.Add(Random.Range(Rays[i].MinMaxDelay.x, Rays[i].MinMaxDelay.y));
        }
    }
    void GenerateNewCountDowns()
    {
        for (int i = 0; i < Rays.Count; i++)
        {
            if (!Rays[i].ConnectedToPrevious)
            {
                Debug.Log("Created New CountDown" + Convert.ToString(i));
                CancellationTokenSource cancellationToken = new CancellationTokenSource();
                StartCoroutine(CountDownRay(rayDelays[i], Rays[i]));
                
            }
            
        }
    }
    
    public IEnumerator CountDownRay(float delay, RayInfo info)
    { 
        Debug.Log("Aaa");
        //wait for delay
        yield return new WaitForSeconds(delay);

        //Check if operation has been cancelled
        Debug.Log("Aaa22222");
        //make ray
        
        StartCoroutine(MakeRay(info.Sideways, info.RayDuration, info.WarningDelay, info.PreDefinedCoord));
        Debug.Log("Aaa3333");
        
        if (info.ConnectedToPrevious)
        {
            if (Rays.Contains(info))
            {
                int index = Rays.IndexOf(info);
                
                if (index < Rays.Count - 1 && Rays[index + 1].ConnectedToPrevious)
                {
                    
                    StartCoroutine(CountDownRay(rayDelays[index + 1], Rays[index + 1]));
                }
                else
                {
                    int firstIndex = FindFirstInChain(Rays, index);
                    CountDownRay(rayDelays[firstIndex], Rays[firstIndex]);
                    
                }
            }
        }
        else
        {
            if (Rays.Contains(info))
            {
                int index = Rays.IndexOf(info);
                //if is not last
                if (index < Rays.Count - 1 && Rays[index+1].ConnectedToPrevious)
                {
                    
                    StartCoroutine(CountDownRay(rayDelays[index + 1], Rays[index + 1]));
                }

            }
        }
        
    }

    public IEnumerator MakeRay(bool sideways, float rayDuration, float warningDelay, float? preDefinedcoord = null)
    {
        //prepare vars
       

        Debug.Log("hello");
        Debug.Log("hello1");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float playerCoord = sideways ? player.transform.position.y : player.transform.position.x;
        Debug.Log("hello2");
        float tema = preDefinedcoord ?? playerCoord;
        float temp = 2f;
        Vector2 warningPos = sideways ? new Vector2(WarningCoord, temp) : new Vector2(temp, WarningCoord);
        
        //spawn warning sign 
        GameObject warningSign = Instantiate(WarningSignPrefab, warningPos, Quaternion.identity);

        yield return new WaitForSeconds(warningDelay);

        //spawn ray

        Quaternion rotation = sideways ? Quaternion.Euler(0, 0, 90) : Quaternion.identity;
        Vector2 rayPos = sideways ? new Vector2(0, warningSign.transform.position.y) : new Vector2(warningSign.transform.position.x, 0);
        Destroy(warningSign);
        GameObject ray = Instantiate(RayPrefab, rayPos, rotation);

        yield return new WaitForSeconds(rayDuration);

        Destroy(ray);
        Debug.Log("hello3");
    }

    float TotalDelay(List<RayInfo> rays, int index)
    {
        if (rays[index].ConnectedToPrevious && index > 0)
        {
            return TotalDelay(rays, index - 1) + rayDelays[index];
        }
        else
        {
            return rayDelays[index];
        }
    }
    int FindFirstInChain(List<RayInfo> rays, int index)
    {
        if (index <= 0 || !rays[index].ConnectedToPrevious)
        {
            return index;
        }
        else
        {
            return FindFirstInChain(rays, index - 1);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(new Vector2(WarningCoord, WarningCoord), 0.1f);
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void OnApplicationQuit()
    {
        StopAllCoroutines();
    }

}


