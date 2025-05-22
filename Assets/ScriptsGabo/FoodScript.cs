using UnityEngine;



public class Food : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(10); // A�ade 10 puntos por comida
            GameManager.Instance.CheckWinCondition();
            Destroy(gameObject);
        }
    }
}

