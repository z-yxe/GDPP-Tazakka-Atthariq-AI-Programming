using System;
using System.Collections;
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

    private Rigidbody rb;
    private Coroutine powerUpCoroutine;

    private void Awake()
    {
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
        if (onPowerUpStart != null)
        {
            onPowerUpStart();
        }

        yield return new WaitForSeconds(powerUpDuration);
        
        if (onPowerUpStop != null)
        {
            onPowerUpStop();
        }
    }
}