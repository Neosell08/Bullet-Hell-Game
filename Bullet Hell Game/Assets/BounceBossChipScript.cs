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
        
        if (transform.position.x > maxX + WallOffset)
        {
            Vector3 rotatedVector = new Vector3(-rb.velocity.x, rb.velocity.y);
            rb.velocity = rotatedVector;
            transform.position = new Vector2(maxX + WallOffset, transform.position.y);
        }
        else if (transform.position.x < minX - WallOffset)
        {

            Vector3 rotatedVector = new Vector3(-rb.velocity.x, rb.velocity.y);
            rb.velocity = rotatedVector;
            transform.position = new Vector2(minX - WallOffset, transform.position.y);
        }
    }
}
