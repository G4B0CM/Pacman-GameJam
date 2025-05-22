using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections; // Necesario para las Coroutines

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 5f; 
    [SerializeField] float turnDuration = 0.1f; 

    private Rigidbody rb;
    private Vector2 turnInput; 

    private bool isTurning = false; 

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
        
        rb.MovePosition(rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
    }

 
    public void OnMove(InputValue value)
    {
        
        turnInput = value.Get<Vector2>();

        
        if (turnInput.x != 0 && !isTurning)
        {
            float targetAngle = turnInput.x * 90f; 
            StartCoroutine(Turn90Degrees(targetAngle));
        }
    }

    private IEnumerator Turn90Degrees(float angle)
    {
        isTurning = true;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.Euler(0, angle, 0);

        float timer = 0f;
        while (timer < turnDuration)
        {
           
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, timer / turnDuration);
            timer += Time.deltaTime;
            yield return null; 
        }

        transform.rotation = endRotation; 
        isTurning = false;
    }
    // Ejemplo en un script de Fantasma o en un script de Colisiones en Pac-Man
    


}