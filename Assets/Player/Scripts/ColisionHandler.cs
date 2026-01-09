using UnityEngine;

public class ColisionHandler : MonoBehaviour
{
    // ------------------ Latihan Collision Sederhana -----------------------

    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.GetComponent<EnemyController>() != null)
    //     {
    //         Destroy(other.gameObject);
    //     }
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
    }
}
