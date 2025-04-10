using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoveScript : MonoBehaviour
{

    public Vector2 Speed;
    public float resetThreshold;
    public Vector2 Offset;
    public float TopYoffset;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)Speed * Time.deltaTime;
        SetBackgroundPositions();
    }
    public void SetBackgroundPositions()
    {
        Transform[] children = new Transform[4] { transform.GetChild(0), transform.GetChild(1), transform.GetChild(2), transform.GetChild(3) };

        children[0].position = new Vector2((transform.position.x % resetThreshold) - resetThreshold, (transform.position.y % resetThreshold) - resetThreshold);
        children[1].position = new Vector2((transform.position.x % resetThreshold) - resetThreshold, transform.position.y % resetThreshold + TopYoffset);
        children[2].position = new Vector2(transform.position.x%resetThreshold, (transform.position.y % resetThreshold) + resetThreshold + TopYoffset) + Offset;
        children[3].position = new Vector2(transform.position.x%resetThreshold, transform.position.y%resetThreshold) + Offset;
    }
}
