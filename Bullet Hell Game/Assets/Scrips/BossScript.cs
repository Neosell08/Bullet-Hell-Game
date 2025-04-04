using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BossScript : MonoBehaviour
{
    /// <summary>
    /// Points where the boss should move
    /// </summary>
    public Vector2[] MovePoints;
    /// <summary>
    /// At what index the current target point is 
    /// </summary>
    int CurPointIndex;

    float WhiteTimer = 9999;
    bool IsWhite;
    Vector2 MoveDir;
    Material DefaultMaterial;
    SpriteRenderer sr;
    int hp;

    public float WhiteDuration;
    public float Speed;
    public float LerpSpeed;
    public float NewPointDistanceThreshold;
    public int maxHP;
    public Material WhiteMaterial;
    public EndUIScript WinUI;
    public GameObject ExplosionSFX;



    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        DefaultMaterial = sr.material;
        hp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        WhiteTimer += Time.deltaTime;
        IsWhite = WhiteTimer < WhiteDuration;

        Vector2 targetDir = MovePoints[CurPointIndex] - (Vector2)transform.position;

        MoveDir = Vector2.Lerp(MoveDir, targetDir, LerpSpeed);
        MoveDir.Normalize();
        
        transform.position += (Vector3)MoveDir * Speed * Time.deltaTime;


        if (Vector2.Distance(transform.position, MovePoints[CurPointIndex]) < NewPointDistanceThreshold)
        {
            CurPointIndex = CircularClamp(CurPointIndex + 1, 0, MovePoints.Length - 1);
        }
        if (IsWhite)
        {
            sr.material = WhiteMaterial;
        }
        else
        {
            sr.material = DefaultMaterial;
        }
    }
    public static int CircularClamp(int value, int min, int max)
    {
        if (value < min)
        {
            return max;
        }
        else if (value > max)
        {
            return min;
        }
        return value;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hp--;
        WhiteTimer = 0;
        Destroy(collision.transform.gameObject);
        Instantiate(ExplosionSFX);
        if(hp <= 0)
        {
            WinUI.gameObject.SetActive(true);
            WinUI.Activate();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2? lastPoint = null;
        for (int i = 0; i < MovePoints.Length; i++)
        {
            if (i > 0)
            {
                Gizmos.color = Color.yellow;
            }
            Gizmos.DrawSphere(MovePoints[i], 0.1f);
            if (lastPoint != null)
            {
                Gizmos.DrawLine(MovePoints[i], lastPoint.Value);
            }
            lastPoint = MovePoints[i];
        }
    }
}


