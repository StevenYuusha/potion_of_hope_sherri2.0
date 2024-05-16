using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectibleitem : MonoBehaviour
{
    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        if (gameController == null)
        {
            Debug.LogError("GameController not found in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(3);

        if (collision.CompareTag("Player"))
        {
            gameController.IngredientCollected();
            Destroy(gameObject);
        }
    }
}