using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [HideInInspector] public bool IsConnectedToMouse = false;
    Collider2D Collider;
    public float LerpSpeed;
    public GameObject DeathUI;


    private void Start()
    {
        Collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (IsConnectedToMouse && Time.timeScale > 0)
        {
            transform.position = Vector3.Lerp(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position, LerpSpeed);

            // Height in world units
            float height = 2f * Camera.main.orthographicSize;

            // Width in world units
            float width = height * Camera.main.aspect;

            Vector3 camPosition = Camera.main.transform.position;

            float minX = camPosition.x - width / 2f;
            float maxX = camPosition.x + width / 2f;
            float minY = camPosition.y - height / 2f;
            float maxY = camPosition.y + height / 2f;

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), 0);
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        if (Collider.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)) && context.performed)
        {
            IsConnectedToMouse = true;
        }
        else if (context.canceled)
        {
            IsConnectedToMouse = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Die();
    }
    public void Die()
    {
        DeathUI.SetActive(true);
        DeathUI.GetComponent<DeadUIScript>().OnDeath();
    }
}
