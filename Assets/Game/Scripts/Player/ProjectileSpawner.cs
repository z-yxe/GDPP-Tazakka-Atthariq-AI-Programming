using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private PlayerController player;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioClip shootSfx;
    [SerializeField] private float fireRate = 2f;
    float timer = 0f;

    private void OnEnable()
    {
        timer = 1f / fireRate;
    }

    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (timer >= 1f / fireRate)
            {
                player.PlaySFX(shootSfx);
                Instantiate(projectile, firePoint.position, firePoint.rotation);
                timer = 0f;
            }
        }
    }
}
