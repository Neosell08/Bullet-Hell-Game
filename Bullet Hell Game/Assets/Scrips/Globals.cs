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
    public static async Task WaitForSecondsAsync(float seconds)
    {
        float elapsed = 0f;
        while (elapsed < seconds)
        {
            await Task.Yield(); // Wait for the next frame
            elapsed += Time.deltaTime; // Accumulates scaled time
        }
    }
}
