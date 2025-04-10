using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    
    public float MaxHP;
    public EndUIScript WinUI;
    public Material WhiteMaterial;
    public float WhiteDuration;
    public GameObject HitSFX;
    [HideInInspector] public float hp;

    float WhiteTimer;
    SpriteRenderer sr;
    Material DefaultMaterial;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        DefaultMaterial = sr.material;
        hp = MaxHP;
    }
    private void Update()
    {
        WhiteTimer += Time.deltaTime;
        bool IsWhite = WhiteTimer < WhiteDuration;
        if (IsWhite)
        {
            sr.material = WhiteMaterial;
        }
        else
        {
            sr.material = DefaultMaterial;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hp--;
        WhiteTimer = 0;
        Destroy(collision.transform.gameObject);
        Instantiate(HitSFX);
        if (hp <= 0)
        {
            WinUI.gameObject.SetActive(true);
            WinUI.Activate();
        }
    }
}
