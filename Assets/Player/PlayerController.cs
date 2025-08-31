using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Action onPowerUpStart;
    public Action onPowerUpStop;

    [SerializeField]
    private float speed;
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float powerUpDuration;
    [SerializeField]
    private int playerHealth;
    [SerializeField]
    private TMP_Text healthText;
    [SerializeField]
    private Transform respawnPoint;

    private Rigidbody rb;
    private Coroutine powerUpCoroutine;
    private bool isPowerUpActive = false;

    private void Awake()
    {
        UpdateUI();
        rb = GetComponent<Rigidbody>();
        HideAndLockCursor();
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 horizontalDir = horizontalInput * cam.transform.right;
        Vector3 verticalDir = verticalInput * cam.transform.forward;
        horizontalDir.y = 0;
        verticalDir.y = 0;

        Vector3 movementDirection = horizontalDir + verticalDir;

        rb.linearVelocity = movementDirection * speed * Time.fixedDeltaTime;
    }

    private void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PickedPowerUp()
    {
        if (powerUpCoroutine != null)
        {
            StopCoroutine(powerUpCoroutine);
        }

        powerUpCoroutine = StartCoroutine(StartPowerUp());
    }

    private IEnumerator StartPowerUp()
    {
        isPowerUpActive = true;
        if (onPowerUpStart != null)
        {
            onPowerUpStart();
        }

        yield return new WaitForSeconds(powerUpDuration);

        isPowerUpActive = false;
        if (onPowerUpStop != null)
        {
            onPowerUpStop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && (isPowerUpActive == true))
        {
            collision.gameObject.GetComponent<EnemyController>().Dead();
        }
    }

    private void UpdateUI()
    {
        healthText.text = "Health : " + playerHealth;
    }

    public void Dead()
    {
        playerHealth -= 1;

        if (playerHealth > 0)
        {
            transform.position = respawnPoint.transform.position;
        }
        else
        {
            playerHealth = 0;
            Debug.Log("Lose");
        }

        UpdateUI();
    }
}