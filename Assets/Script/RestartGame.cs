using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;  // Import the SceneManager namespace
using UnityEngine.UI;  // For UI elements

public class RestartGame : MonoBehaviour
{
    private GameObject gameOverPanel;  // Reference to the UI panel
    private Button restartButton;  // Reference to the restart button
    private Button menuButton;  // Reference to the menu button

    private bool playerInRange;  // To check if the player is in range

    private void Start()
    {
        // Find the GameObject with the assigned tag, even if it's initially inactive
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("RestartUI"))
            {
                gameOverPanel = obj;
                break;
            }
        }

        // Ensure the game over panel is found
        if (gameOverPanel != null)
        {
            // Temporarily activate the panel to find its child buttons
            gameOverPanel.SetActive(true);

            // Find the buttons within the now-active panel
            restartButton = GameObject.FindGameObjectWithTag("RestartBtn").GetComponent<Button>();
            menuButton = GameObject.FindGameObjectWithTag("MenuBtn").GetComponent<Button>();

            // Deactivate the panel again after finding the buttons
            gameOverPanel.SetActive(false);

            // Ensure the buttons are found
            if (restartButton == null || menuButton == null)
            {
                Debug.LogError("Buttons not found. Make sure they are tagged correctly.");
                return;
            }

            // Assign button listeners
            restartButton.onClick.AddListener(Restart); // Assign Restart method to Restart button
            menuButton.onClick.AddListener(GoToMainMenu); // Assign GoToMainMenu method to Menu button
        }
        else
        {
            Debug.LogError("Game Over Panel not found. Make sure it is tagged correctly.");
            return;
        }
    }

    // This method is called when another collider enters the trigger collider attached to the object this script is on
    private void OnTriggerEnter2D(Collider2D other)
    {
   
        if (other.CompareTag("Player"))  // Check if the object entering the trigger is the player
        {
            Debug.Log("Dead Triggered by: " + other.name);  // Add this line
            playerInRange = true;
            ShowGameOverUI();  // Call method to show the game over UI
        }
    }

    // Method to show the game over UI panel
    private void ShowGameOverUI()
    {
        if (playerInRange && gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // Activate the game over UI panel
            Time.timeScale = 0f; // Pause the game
        }
    }

    // Method to restart the game
    private void Restart()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

    // Method to load the Main Menu scene
    private void GoToMainMenu()
    {
        Time.timeScale = 1f; // Resume the game
        SceneManager.LoadScene("MainMenu"); // Load the Main Menu scene by name
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(false); // Hide the game over panel
            }
        }
    }
}
