using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    
    public float MaxHP;
    public EndUIScript WinUI;
    public Material WhiteMaterial;
    public float WhiteDuration;
    public GameObject HitSFX;
    [HideInInspector] public float hp;

    [Header("Dead")]
    public GameObject ExplosionPrefab;
    public Vector2 ExplosionDelayRange;
    public float DeadAnimationDuration;
    public Behaviour[] DisableComponents;
    public Vector2 ExplosionSpawnSize;
    

    float whiteTimer;
    float explosionTimer;
    float deadTimer;
    [HideInInspector] public bool isDead;
    float explosionDelay;
    SpriteRenderer sr;
    Material DefaultMaterial;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        DefaultMaterial = sr.material;
        hp = MaxHP;
        whiteTimer = float.PositiveInfinity;
    }
    private void Update()
    {
        
        whiteTimer += Time.deltaTime;
        bool IsWhite = whiteTimer < WhiteDuration;
        if (IsWhite)
        {
            sr.material = WhiteMaterial;
        }
        else
        {
            sr.material = DefaultMaterial;
        }
        if (isDead)
        {
            deadTimer += Time.deltaTime;
            explosionTimer += Time.deltaTime;
            if (explosionTimer > explosionDelay)
            {
                explosionTimer = 0;
                Instantiate(ExplosionPrefab, (Vector2)transform.position + new Vector2(Random.Range(-ExplosionSpawnSize.x, ExplosionSpawnSize.x), Random.Range(-ExplosionSpawnSize.y, ExplosionSpawnSize.y)), Quaternion.identity);
            }
            if (deadTimer > DeadAnimationDuration)
            {
                WinUI.gameObject.SetActive(false);
                WinUI.Activate();
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead)
        {
            hp--;
            whiteTimer = 0;
            Destroy(collision.transform.gameObject);
            Instantiate(HitSFX);
            if (hp <= 0)
            {
                isDead = true;
                explosionDelay = Random.Range(ExplosionDelayRange.x, ExplosionDelayRange.y);
                foreach (Behaviour component in DisableComponents)
                {
                    if (component is RayBossAttackPattern rayAttack)
                    {
                        rayAttack.StopAllRays();
                    }
                    component.enabled = false;
                }
            }
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, ExplosionSpawnSize*2);
    }
}
