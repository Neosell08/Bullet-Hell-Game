using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBossChipScript : MonoBehaviour
{
    public float Speed;
    public float Bounciness;
    public float WallOffset;
    public float Inertia;

    Vector2 velocity;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        velocity = transform.right * Speed;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = velocity;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + Inertia*Time.deltaTime);
        
        // Height in world units
        float height = 2f * Camera.main.orthographicSize;

        // Width in world units
        float width = height * Camera.main.aspect;

        Vector3 camPosition = Camera.main.transform.position;

        float minX = camPosition.x - width / 2f;
        float maxX = camPosition.x + width / 2f;
        float minY = camPosition.y - height / 2f;
        float maxY = camPosition.y + height / 2f;
        if (transform.position.y < minY || transform.position.y > maxY)
        {
            Destroy(gameObject);
        }
        
        if (transform.position.x > maxX )
        {
            Vector3 rotatedVector = Quaternion.AngleAxis(-45, Vector2.up) * velocity;
            rb.velocity = rotatedVector;
            Debug.Log(rotatedVector); // Output: Rotated vector
        }
    }
}
