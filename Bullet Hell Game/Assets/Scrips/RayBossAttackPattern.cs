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
    public RayInfo(Vector2 minMaxDelay, float warningDelay, float warningCoord, float rayDuration, float preDefinedCoord, bool usePreDefindedCoord, bool connectedToPrevious, bool sideways)
    {
        MinMaxDelay = minMaxDelay;
        WarningDelay = warningDelay;
        WarningCoord = warningCoord;
        RayDuration = rayDuration;
        PreDefinedCoord = preDefinedCoord;
        UsePreDefindedCoord = usePreDefindedCoord;
        ConnectedToPrevious = connectedToPrevious;
        Sideways = sideways;
    }
}


public class RayBossAttackPattern : MonoBehaviour
{
    public GameObject RayPrefab;
    public GameObject WarningSignPrefab;
   

    float timer;
    //all information about rays
    [SerializeField] public List<RayInfo> Rays = new List<RayInfo>();

    //the calculated delay of each ray
    private List<float> rayDelays = new List<float>();

    List<GameObject> RayInstances = new List<GameObject>();
    List<GameObject> WarningInstances = new List<GameObject>();
    GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        RestartRays();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    

    // Update is called once per frame
    private void Update()
    {
       

    }
    //stop firing new rays of old phase
    public void StopAllRays()
    {
        StopAllCoroutines();
        foreach(var ray in RayInstances)
        {
            Destroy(ray);
        }
        foreach (var warning in WarningInstances)
        {
            Destroy(warning);
        }
        WarningInstances.Clear();
        RayInstances.Clear();
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
                
                CancellationTokenSource cancellationToken = new CancellationTokenSource();
                StartCoroutine(CountDownRay(rayDelays[i], Rays[i]));
                
            }
            
        }
    }
    public void RestartRays()
    {
        GenerateNewDelays();
        GenerateNewCountDowns();
    }
    
    public IEnumerator CountDownRay(float delay, RayInfo info)
    { 

        //wait for delay
        if (delay <= 0f)
        {
            while (Time.timeScale <= 0)
            {
                yield return new WaitForSeconds(delay);
            }
        }
        yield return new WaitForSeconds(delay);

        //make ray
        
        StartCoroutine(MakeRay(info.Sideways, info.RayDuration, info.WarningDelay, info.WarningCoord, info.UsePreDefindedCoord ? info.PreDefinedCoord : null));
        
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
                    StartCoroutine(CountDownRay(rayDelays[firstIndex], Rays[firstIndex]));
                    
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
                else
                {
                    StartCoroutine(CountDownRay(Random.Range(info.MinMaxDelay.x, info.MinMaxDelay.y), info));
                }

            }
        }
        
    }

    public IEnumerator MakeRay(bool sideways, float rayDuration, float warningDelay, float warningCoord, float? preDefinedcoord = null)
    {
        //prepare vars
      
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float playerCoord = sideways ? player.transform.position.y : player.transform.position.x;

        float temp = preDefinedcoord ?? playerCoord;
        
        Vector2 warningPos = sideways ? new Vector2(warningCoord, temp) : new Vector2(temp, warningCoord);
        
        //spawn warning sign 
        GameObject warningSign = Instantiate(WarningSignPrefab, warningPos, Quaternion.identity);
        if (sideways) warningSign.transform.GetChild(0).Rotate(0, 0, 90);
        WarningInstances.Add(warningSign);
        yield return new WaitForSeconds(warningDelay);

        //spawn ray

        Quaternion rotation = sideways ? Quaternion.Euler(0, 0, 90) : Quaternion.identity;
        Vector2 rayPos = sideways ? new Vector2(0, warningSign.transform.position.y) : new Vector2(warningSign.transform.position.x, 0);
        Destroy(warningSign);
        WarningInstances.Remove(warningSign);
        GameObject ray = Instantiate(RayPrefab, rayPos, rotation);
        RayInstances.Add(ray);
        yield return new WaitForSeconds(rayDuration);

        Destroy(ray);
        RayInstances.Remove(ray);

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
    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void OnApplicationQuit()
    {
        StopAllCoroutines();
    }

}


