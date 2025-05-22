using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] TMP_Text endGame;

    private int score = 0;
    private int totalFoodCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        totalFoodCount = GameObject.FindGameObjectsWithTag("Food").Length;
        Debug.Log("Total de comida en la escena: " + totalFoodCount);
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Puntaje: " + score);
    }

    public void CheckWinCondition()
    {
        totalFoodCount--;
        if (totalFoodCount <= 0)
        {
            Debug.Log("¡Ganaste!");
            endGame.text = "Ganaste!";
        }
    }
}
