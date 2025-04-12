using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        BossHealth bossHealth = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossHealth>();
        GetComponent<Image>().fillAmount = bossHealth.hp/bossHealth.MaxHP;
    }
}
