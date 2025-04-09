using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBulletAttack : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Vector2 leftRotRange;
    public Vector2 rightRotRange;
    public bool Enabled;
    public float Delay;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Enabled) return;
        timer += Time.deltaTime;
        if (timer > Delay)
        {
            timer = 0;
            Shoot();
        }
    }
    public void Shoot()
    {
        float rotation = Random.Range(0, 2) == 0 ? Random.Range(leftRotRange.x, leftRotRange.y) : Random.Range(rightRotRange.x, rightRotRange.y);
        Instantiate(BulletPrefab, transform.position, Quaternion.Euler(0, 0, rotation));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, Globals.RotateVector(Vector2.right, leftRotRange.x));
        Gizmos.DrawLine(transform.position, Globals.RotateVector(Vector2.right, leftRotRange.y));
        Gizmos.DrawLine(transform.position, Globals.RotateVector(Vector2.right, rightRotRange.x));
        Gizmos.DrawLine(transform.position, Globals.RotateVector(Vector2.right, rightRotRange.y));
    }
}
