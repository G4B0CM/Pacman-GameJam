using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector3 moveDirection;

    public Transform cameraTransform; // Arrastra la cámara aquí desde el editor

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal"); // A/D o flechas izquierda/derecha
        float moveZ = Input.GetAxisRaw("Vertical");   // W/S o flechas arriba/abajo

        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
    }

    private void FixedUpdate()
    {
        if (moveDirection != Vector3.zero)
        {
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        }

        // Opcional: rota el personaje hacia la dirección de movimiento
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.fixedDeltaTime);
        }

        // Hacer que la cámara siga al jugador (manteniendo la distancia)
        if (cameraTransform != null)
        {
            cameraTransform.position = transform.position + new Vector3(0, 10, -10); // Ajusta según tu escena
            cameraTransform.LookAt(transform);
        }
    }
}
