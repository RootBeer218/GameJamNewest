using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Transform> spawnPoints; // Add spawn points in the editor
    private List<GameObject> unchosenCharacters;

    private void Start()
    {
        // Fetch the GameManager instance to get the unchosen characters
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            unchosenCharacters = gameManager.GetUnchosenCharacters();
        }
        else
        {
            Debug.LogError("GameManager instance not found. Make sure it's present in the scene.");
            return;
        }

        // Spawn unchosen characters at the spawn points
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (unchosenCharacters.Count == 0) break;
            int randomIndex = Random.Range(0, unchosenCharacters.Count);
            Instantiate(unchosenCharacters[randomIndex], spawnPoint.position, Quaternion.identity);
            unchosenCharacters.RemoveAt(randomIndex);
        }
    }
}
