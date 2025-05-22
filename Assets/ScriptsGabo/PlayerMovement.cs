using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections; // Necesario para las Coroutines

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 5f; // Velocidad de movimiento constante hacia adelante
    [SerializeField] float turnDuration = 0.1f; // Duraci�n en segundos para completar el giro de 90 grados (m�s bajo = m�s r�pido)

    private Rigidbody rb;
    private Vector2 turnInput; // Para capturar la entrada de giro (X = -1 para Izq, 1 para Der)

    private bool isTurning = false; // Bandera para controlar si Pac-Man est� girando

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("PlayerMovement requiere un Rigidbody en el mismo GameObject.");
            enabled = false;
            return;
        }
    }

    void FixedUpdate()
    {
        // Mover Pac-Man siempre hacia adelante
        rb.MovePosition(rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
    }

    // Este m�todo es llamado autom�ticamente por el componente Player Input
    // cuando se activa la acci�n 'Move' (mapeada a un Vector2 con A/D)
    public void OnMove(InputValue value)
    {
        // Obtenemos el Vector2 de la entrada (e.g., (-1,0) para 'A', (1,0) para 'D')
        turnInput = value.Get<Vector2>();

        // Si hay una entrada de giro horizontal y Pac-Man no est� ya girando
        if (turnInput.x != 0 && !isTurning)
        {
            float targetAngle = turnInput.x * 90f; // -90 para izquierda, +90 para derecha
            StartCoroutine(Turn90Degrees(targetAngle));
        }
    }

    private IEnumerator Turn90Degrees(float angle)
    {
        isTurning = true; // Marca que Pac-Man est� girando

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.Euler(0, angle, 0); // Calcula la rotaci�n final

        float timer = 0f;
        while (timer < turnDuration)
        {
            // Interpolar suavemente entre la rotaci�n inicial y la final
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, timer / turnDuration);
            timer += Time.deltaTime;
            yield return null; // Espera al siguiente frame
        }

        transform.rotation = endRotation; // Asegura que la rotaci�n sea exactamente 90 grados al final
        isTurning = false; // Marca que el giro ha terminado
    }

    // Opcional: Manejo de colisiones si Pac-Man debe detenerse
    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Puedes detener el movimiento aqu� si lo necesitas
            // moveSpeed = 0f; 
        }
    }

    private void OnCollisionExit(Collision actualCollision)
    {
        if (actualCollision.gameObject.CompareTag("Wall"))
        {
            // moveSpeed = 5f; // Restablecer la velocidad
        }
    }
    */
}