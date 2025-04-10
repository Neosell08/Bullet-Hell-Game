using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBurstable
{
    [HideInInspector] public float Delay { get; set; }
    [HideInInspector] public bool DoBurst { get; set; }
}
