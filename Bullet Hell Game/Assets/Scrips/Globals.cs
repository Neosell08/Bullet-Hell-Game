using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class Globals
{
    public static Vector2 RotationToVector(this float rotation)
    {
        rotation = rotation * Mathf.Deg2Rad;
        return new Vector2(MathF.Round(MathF.Cos(rotation), 6), MathF.Round(MathF.Sin(rotation), 6));
    }
    public static string TimeToString(float seconds)
    {
        if (seconds > 3599) { return "NaN"; }


        float mins = MathF.Floor(seconds / 60);
        seconds = MathF.Floor(seconds % 60);
        return (mins < 10 ? "0" : "") + mins + " : " + (seconds < 10 ? "0" : "") + seconds;
    }
    public static float RotationBetweenVectors(this Vector2 v1, Vector2 v2)
    {
        Vector2 direction = v2 - v1;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
    public static Vector2 RotateVector(Vector2 v, float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad; // Convert degrees to radians
        float cos = Mathf.Cos(radians);
        float sin = Mathf.Sin(radians);

        return new Vector2(
            v.x * cos - v.y * sin, // X component
            v.x * sin + v.y * cos  // Y component
        );
    }
}
