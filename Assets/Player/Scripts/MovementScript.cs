using UnityEngine;

public class MovementScript : MonoBehaviour
{
    // ------------------- Latihan Timer Sederhana ------------------------

    // private float timer = 5f;

    // private void Update()
    // {
    //     timer -= Time.deltaTime;

    //     if (timer <= 0f)
    //     {
    //         Debug.Log("waktu habis");
    //         timer = 5f;
    //     }
    // }

    // ------------------- Latihan Input Sederhana ------------------------

    [SerializeField] private float speed = 10f;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }

    private void Update()
    {
        float horizontalDir = Input.GetAxis("Horizontal");
        float verticalDir = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(horizontalDir, 0f, verticalDir).normalized;
        transform.position += moveDir * speed * Time.deltaTime;
    }

    // private void FixedUpdate()
    // {
    //     float horizontalDir = Input.GetAxis("Horizontal");
    //     float verticalDir = Input.GetAxis("Vertical");

    //     Vector3 moveDir = new Vector3(horizontalDir, 0f, verticalDir).normalized;
    //     rb.linearVelocity += moveDir * speed * Time.fixedDeltaTime;
    // }
}
