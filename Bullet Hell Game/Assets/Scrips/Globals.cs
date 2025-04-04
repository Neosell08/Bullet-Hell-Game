using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static Vector2 RotationToVector(this float rotation)
    {
        rotation = rotation * Mathf.Deg2Rad;
        return new Vector2(MathF.Round(MathF.Cos(rotation), 6), MathF.Round(MathF.Sin(rotation), 6));
    }
}
