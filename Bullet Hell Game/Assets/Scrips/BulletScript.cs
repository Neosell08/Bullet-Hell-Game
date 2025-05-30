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
    public float Inertia;
    Rigidbody2D rb;
    public float DownAcceleration;
    // Start is called before the first frame update

    float DownSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * Speed;
    }

    // Update is called once per frame
    void Update()
    {
        DownSpeed += DownAcceleration * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + Inertia*Time.deltaTime);
        rb.velocity = (Vector2)transform.right * Speed + Vector2.down * DownSpeed;
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
        if (transform.position.y > maxY + 1)
        {
            Destroy(gameObject);
        }
    }
    
}
