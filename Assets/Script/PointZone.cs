using UnityEngine;

public class PointZone : MonoBehaviour
{
    public int points = 1; // Points value for this zone

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player Detection"))
        {
            PointManager.Instance.AddPoints(points); // Add points using the manager
        }
    }
}
