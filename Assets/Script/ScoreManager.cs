using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; // Singleton instance
    private int highScore = 0; // High score value

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensure this object persists across scenes
            LoadHighScore(); // Load the high score when the game starts
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
        }
    }

    // Method to update the high score if the new score is higher
    public void UpdateHighScore(int newScore)
    {
        if (newScore > highScore)
        {
            highScore = newScore;
            SaveHighScore(); // Save the new high score
        }
    }

    // Method to get the current high score
    public int GetHighScore()
    {
        return highScore;
    }

    // Method to save the high score using PlayerPrefs
    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save(); // Ensure the high score is saved
    }

    // Method to load the high score from PlayerPrefs
    private void LoadHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
    }
}
