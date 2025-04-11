using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoveScript : MonoBehaviour
{

    public Vector2 Speed;
    public Vector2 Offset;

    Vector2 tileSize;
    // Start is called before the first frame update
    void Start()
    {
        tileSize = transform.GetComponentInChildren<SpriteRenderer>().size;
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

        children[0].position = CalculateTilePosition(transform.position, new Vector2(0, 0));
        children[1].position = CalculateTilePosition(transform.position, new Vector2(1, 0));
        children[2].position = CalculateTilePosition(transform.position, new Vector2(0, 1));
        children[3].position = CalculateTilePosition(transform.position, new Vector2(1, 1));
    }
    private Vector2 CalculateTilePosition(Vector2 basePosition, Vector2 gridPosition)
    {
        return new Vector2(
            basePosition.x % tileSize.x + (gridPosition.x - 1) * tileSize.x,
            basePosition.y % tileSize.y + (gridPosition.y - 1) * tileSize.y
        ) + Offset;
    }
}
