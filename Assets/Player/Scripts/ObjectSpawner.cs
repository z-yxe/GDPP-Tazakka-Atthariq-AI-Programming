using Unity.Mathematics;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 2f;
    float timer = 0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (timer >= 1f / fireRate)
            {
                Instantiate(projectile, firePoint.position, firePoint.rotation);
                timer = 0f;
            }
        }
    }
}
