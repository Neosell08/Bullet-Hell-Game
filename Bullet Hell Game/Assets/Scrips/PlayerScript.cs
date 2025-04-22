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
    public GameObject PlayerDeadSFX;
    public float MoveDistanceThreshold;
    public float WASDSpeed;

    Vector2 WASDMovement;
    private void Start()
    {
        Collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (IsConnectedToMouse && Time.timeScale > 0)
        {
            float dist = Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position);
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
        else if (Time.timeScale > 0)
        {
            transform.position += (Vector3)WASDMovement * WASDSpeed * Time.deltaTime;
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        
        if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), transform.position) <= MoveDistanceThreshold && context.started)
        {
            IsConnectedToMouse = true;
        }
        else if (context.canceled)
        {
            IsConnectedToMouse = false;
        }
    }
    public void OnMoveWASD(InputAction.CallbackContext context)
    {

        Vector2 input = context.ReadValue<Vector2>();


        WASDMovement = new Vector2(input.x, input.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameObject.FindGameObjectWithTag("Boss").GetComponent<BossHealth>().isDead == false)
        {
            Die();
        }
        
    }
    public void Die()
    {
        Instantiate(PlayerDeadSFX);
        DeathUI.SetActive(true);
        DeathUI.GetComponent<EndUIScript>().Activate();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, MoveDistanceThreshold);
    }
}
