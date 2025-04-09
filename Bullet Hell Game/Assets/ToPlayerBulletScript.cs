using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToPlayerBulletScript : MonoBehaviour
{
    public float Speed;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        transform.rotation = Quaternion.Euler(0, 0, Globals.RotationBetweenVectors(transform.position, player.transform.position));

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * Speed;
    }

    // Update is called once per frame
    void Update()
    {
        // Height in world units
        float height = 2f * Camera.main.orthographicSize;

        // Width in world units
        float width = height * Camera.main.aspect;

        Vector3 camPosition = Camera.main.transform.position;

        float minX = camPosition.x - width / 2f;
        float maxX = camPosition.x + width / 2f;
        float minY = camPosition.y - height / 2f;
        float maxY = camPosition.y + height / 2f;

        if (transform.position.x < minX - 10 || transform.position.x > maxX + 10 || transform.position.y < minY - 10 || transform.position.y > maxY + 10)
        {
            Destroy(gameObject);
        }
    }
    
}
