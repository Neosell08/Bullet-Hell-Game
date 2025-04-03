using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisScript : MonoBehaviour
{
    public float Speed;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 velocity = RotationToVector(transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * Speed;

        // Height in world units
        float height = 2f * Camera.main.orthographicSize;

        // Width in world units
        float width = height * Camera.main.aspect;

        Vector3 camPosition = Camera.main.transform.position;

        float minX = camPosition.x - width / 2f;
        float maxX = camPosition.x + width / 2f;
        float minY = camPosition.y - height / 2f;
        float maxY = camPosition.y + height / 2f;

        if (transform.position.x < minX-10 || transform.position.x > maxX + 10 || transform.position.y < minY - 10 || transform.position.y > maxY + 10)
        {
            Destroy(gameObject);
        }
    }
    Vector2 RotationToVector(float rotation)
    {
        rotation = rotation * Mathf.Deg2Rad;
        return new Vector2(MathF.Round(MathF.Cos(rotation), 6), MathF.Round(MathF.Sin(rotation), 6));
    }
}
