using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText; // Reference to the TextMeshPro UI element for high score

    private void Start()
    {
        UpdateHighScoreDisplay();
    }

    // Method to update the high score display
    private void UpdateHighScoreDisplay()
    {
        if (highScoreText != null)
        {
            int highScore = ScoreManager.Instance.GetHighScore(); // Get high score from ScoreManager
            highScoreText.text = "High Score: " + highScore; // Update the text
        }
    }
}
