using UnityEngine;
using TMPro;

public class PointManager : MonoBehaviour
{
    public static PointManager Instance; // Singleton instance
    public int totalPoints = 0; // Total points across all zones
    public TextMeshProUGUI pointsText; // Reference to the TextMeshPro UI element
	public TextMeshProUGUI pointsText1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
			totalPoints = -3;
            DontDestroyOnLoad(gameObject); // Optional, if you want to persist across scenes
        }
    }

    public void AddPoints(int points)
    {
        totalPoints += points; // Increment total points
        UpdatePointsText(); // Update the TextMeshPro UI element
        ScoreManager.Instance.UpdateHighScore(totalPoints); // Update high score if necessary
    }

    private void UpdatePointsText()
    {
		if (pointsText != null)
		{
			pointsText.text = "" + totalPoints; // Update the text
		}
		if (pointsText1 != null)
		{
			pointsText1.text = " " + totalPoints; // Update the text
		}
    }
}
