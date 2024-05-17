using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int totalIngredients = 7;
    public int collectedIngredients = 0;
    public GameObject door;
    private bool isOpen = false;
    public Sprite doorOpenSprite;

    private void Start()
    {
        
    }

    public void IngredientCollected()
    {
        collectedIngredients++;
        if (collectedIngredients >= totalIngredients && !isOpen)
        {
            door.SetActive(true);
            OpenDoor();
            WinGame();
        }
    }

    private void OpenDoor()
    {
        
        isOpen = true;
        door.GetComponent<SpriteRenderer>().sprite = doorOpenSprite;
    }

    private void WinGame()
    {
        // Load the winning scene
        SceneManager.LoadScene("WinScene");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IngredientCollected();
            // Assuming this script is on the ingredient object, destroy the ingredient, not the GameController
            Destroy(collision.gameObject);
        }
    }
}
