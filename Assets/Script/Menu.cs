using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject startMenu; // Reference to the start menu UI

    public void Play()
    {
        // Load the main game scene
        SceneManager.LoadScene("Character Selection");
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the application
    }
}
