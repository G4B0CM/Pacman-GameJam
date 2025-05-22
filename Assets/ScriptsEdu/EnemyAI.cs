using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float speed = 3.0f;
    public float killDistance = 2f;

    void Start()
    {
        // Buscar jugador autom�ticamente si no est� asignado
        if (player == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            if (obj != null) player = obj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        transform.LookAt(player);

        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            speed * Time.deltaTime
        );

        
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PacManHealth pacManHealth = collision.gameObject.GetComponent<PacManHealth>();
            if (pacManHealth != null)
            {
                pacManHealth.LoseLife();
            }
        }
    }
}
