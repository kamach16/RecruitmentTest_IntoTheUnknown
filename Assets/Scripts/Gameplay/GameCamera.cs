using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private float yaw;   // obr�t w osi Y (poziomy)
    private float pitch; // obr�t w osi X (pionowy)

    private void Start()
    {
        Vector3 euler = transform.eulerAngles;
        yaw = euler.y;
        pitch = euler.x;
    }

    private void Update()
    {
        Move();
        Rotate();
    }

    private void Move()
    {
        float speedMultipler = 1;
        if (Input.GetKey(KeyCode.LeftShift))
            speedMultipler = 2;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            speedMultipler = 1;

        float vertical = Time.deltaTime * moveSpeed * Input.GetAxisRaw("Vertical") * speedMultipler;
        float horizontal = Time.deltaTime * moveSpeed * Input.GetAxisRaw("Horizontal") * speedMultipler;

        transform.Translate(new Vector3(horizontal, 0, vertical));
    }

    private void Rotate()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            yaw += mouseX * rotationSpeed;
            pitch -= mouseY * rotationSpeed;

            transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }
    }
}
