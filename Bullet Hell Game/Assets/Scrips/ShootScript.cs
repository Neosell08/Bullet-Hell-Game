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
    public Transform ShootSeperator;

    Vector2? start;
    bool HasShot = false;
    public void Update()
    {
        Vector2 mousePos = Input.mousePosition;

        if (start != null && Vector2.Distance(mousePos, start.Value) >= ShootDistanceThreshold && Time.timeScale > 0 && Camera.main.ScreenToWorldPoint(Vector2.Lerp(mousePos, start.Value,0.5f)).y <= ShootSeperator.position.y && !HasShot)
        {
            Vector2 ShootPos = Camera.main.ScreenToWorldPoint(Vector2.Lerp(mousePos, start.Value, 0.5f));
            Vector2 diff = mousePos - start.Value;
            float rotation = Mathf.Rad2Deg * MathF.Atan2(diff.y, diff.x);
            Instantiate(BulletPrefab, ShootPos, Quaternion.Euler(0, 0, rotation));
            Instantiate(ShootSFX[UnityEngine.Random.Range(0, ShootSFX.Length)]);
            HasShot = true;
        }
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            start = Input.mousePosition;
        }
        else if (context.canceled)
        {
            start = null;
            HasShot = false;
        }
    }
}
