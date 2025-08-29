using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 100f;
    [SerializeField]
    private Camera cam;
    private Rigidbody rb;

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
}