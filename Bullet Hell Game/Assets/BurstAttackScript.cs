using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstAttackScript : MonoBehaviour
{
    public MonoBehaviour attack;
    public float BurstSpeed;
    public float BurstDelay;
    public float BurstDuration;

    float Timer = 0;
    bool IsBursting;
    // Start is called before the first frame update
    void Start()
    {
        if (attack is IBurstable burstable)
        {
            burstable.DoBurst = true;
            
            burstable.Delay = float.PositiveInfinity;   
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attack is IBurstable burstable)
        {
            Timer += Time.deltaTime;

            if (IsBursting)
            {
                if (Timer > BurstDuration)
                {
                    Timer = 0;
                    IsBursting = false;
                    burstable.Delay = float.PositiveInfinity;
                }
            }
            else
            {
                if (Timer > BurstDelay)
                {
                    Timer = 0;
                    IsBursting = true;
                    burstable.Delay = 1 / BurstSpeed;
                }
            }
        }
        
    }
}
