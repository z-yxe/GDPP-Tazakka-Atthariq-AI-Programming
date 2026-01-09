using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Action onPowerUpStart;
    public Action onPowerUpStop;

    [Header("Refrence")]
    [SerializeField] private Camera cam;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private AudioSource audioSource;

    [Header("Value")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float powerUpDuration;
    [SerializeField] private int playerHealth;

    [Serializable]
    public struct WeaponSlot {
        public PickableType weaponType;
        public GameObject weaponPrefab;
    }

    [SerializeField] List<WeaponSlot> weapon;

    private Coroutine powerUpCoroutine;

    private void Awake()
    {
        UpdateUI();
        HideAndLockCursor();
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 horizontalMovement = horizontalInput * cam.transform.right;
        Vector3 verticalMovement = verticalInput * cam.transform.forward;
        horizontalMovement.y = 0;
        verticalMovement.y = 0;

        Vector3 movementDirection = horizontalMovement + verticalMovement;
        rb.linearVelocity = movementDirection.normalized * movementSpeed;

        Vector3 frontOfCamera = cam.transform.forward;
        frontOfCamera.y = 0;
        transform.forward = frontOfCamera;
    }

    private void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void PickedPowerUp(PickableType type)
    {
        if (powerUpCoroutine != null)
        {
            StopCoroutine(powerUpCoroutine);
        }

        powerUpCoroutine = StartCoroutine(StartPowerUp(type));
    }

    private IEnumerator StartPowerUp(PickableType type)
    {
        onPowerUpStart?.Invoke(); 

        foreach (var wp in weapon)
        {
            if (wp.weaponType == type)
            {
                wp.weaponPrefab.SetActive(true);
            } 
            else
            {
                wp.weaponPrefab.SetActive(false);
            }
        }

        yield return new WaitForSeconds(powerUpDuration);

        onPowerUpStop?.Invoke();

        foreach (var wp in weapon)
        {
            if (wp.weaponType == type)
            {
                wp.weaponPrefab.SetActive(false);
            }
        }
    }

    public void PlaySFX(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
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
            SceneManager.LoadScene("LoseScreen");
        }

        UpdateUI();
    }
}