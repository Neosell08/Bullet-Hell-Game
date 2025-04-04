using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShootScript : MonoBehaviour
{
    public float ShootDistanceThreshold = 1;
    public GameObject BulletPrefab;
    public GameObject[] ShootSFX;

    Vector2? start;
    bool HasShot = false;
    public void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (start != null && Vector2.Distance(mousePos, start.Value) >= ShootDistanceThreshold && !HasShot)
        {
            Vector2 diff = mousePos - start.Value;
            float rotation = Mathf.Rad2Deg * MathF.Atan2(diff.y, diff.x);
            Instantiate(BulletPrefab, mousePos, Quaternion.Euler(0, 0, rotation));
            Instantiate(ShootSFX[UnityEngine.Random.Range(0, ShootSFX.Length)]);
            HasShot = true;
        }
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (context.canceled)
        {
            start = null;
            HasShot = false;
        }
    }
}
