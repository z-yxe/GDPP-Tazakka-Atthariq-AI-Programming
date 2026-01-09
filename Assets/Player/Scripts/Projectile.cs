using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float destroyTime = 3f;
    [SerializeField] private float damage = 15f;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * projectileSpeed;

        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            Destroy(gameObject);
        } 
        else if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
