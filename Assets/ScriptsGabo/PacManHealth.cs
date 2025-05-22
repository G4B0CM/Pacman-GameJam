using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para recargar la escena o manejar el fin del juego

public class PacManHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] int maxLives = 3; 
    private int currentLives;

    [Header("Respawn Settings")]
    [SerializeField] Vector3 respawnPosition = new Vector3(2f, 1.5f, 2f);
    [SerializeField] TMP_Text scoreBoardText;
    [SerializeField] TMP_Text endGame;



    private PlayerMovement playerMovement;

    void Awake()
    {
        currentLives = maxLives;
        playerMovement = GetComponent<PlayerMovement>(); 
        if (playerMovement == null)
        {
            Debug.LogError("PacManHealth requiere el script PlayerMovement en el mismo GameObject.");
            enabled = false; 
        }
    }

    public void LoseLife()
    {
        currentLives--;
        Debug.Log("Pac-Man ha perdido una vida. Vidas restantes: " + currentLives);
        scoreBoardText.text = currentLives.ToString();

        if (currentLives <= 0)
        {
            GameOver();
        }
        else
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

      
        transform.position = respawnPosition;
        
        transform.rotation = Quaternion.identity; 

  
        Debug.Log("Pac-Man reaparece en: " + respawnPosition);

        
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
    }

    private void GameOver()
    {
        Debug.Log("¡Juego Terminado! Pac-Man se ha quedado sin vidas.");
        endGame.text = "Perdiste!";

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        
        Time.timeScale = 0f;

       
    }

    
    private void ReloadCurrentScene()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public int GetCurrentLives()
    {
        return currentLives;
    }

    public int GetMaxLives()
    {
        return maxLives;
    }
}