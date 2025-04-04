using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float Speed;
    public int DebrisAmount;
    public GameObject DebrisPrefab;
    public bool SpawnDebris;
    public float Intertia;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.inertia = Intertia;
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
        if (transform.position.x < minX || transform.position.x > maxX || transform.position.y < minY )
        {
            if (SpawnDebris)
            {
                float randomRotation = UnityEngine.Random.Range(0f, 360f);
                for (int i = 0; i < DebrisAmount; i++)
                {
                    float rotation = (360 * i / DebrisAmount) + randomRotation;
                    Instantiate(DebrisPrefab, transform.position, Quaternion.Euler(0, 0, rotation));
                }
            }
            
            Destroy(gameObject);
        }
        if (transform.position.y > maxY + 10)
        {
            Destroy(gameObject);
        }
    }
    
}
