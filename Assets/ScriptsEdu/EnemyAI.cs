using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float speed = 3.0f;
    public float killDistance = 1.5f;

    void Start()
    {
        // Buscar jugador automáticamente si no está asignado
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

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < killDistance)
        {
            Debug.Log("¡El enemigo atrapó al jugador!");
            Destroy(player.gameObject);
        }
    }
}
