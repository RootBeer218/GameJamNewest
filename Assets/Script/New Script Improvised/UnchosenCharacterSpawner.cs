using System.Collections.Generic;
using UnityEngine;

public class UnchosenCharacterSpawner : MonoBehaviour
{
    public List<GameObject> unchosenCharacters; // List to hold unchosen characters

    private void Start()
    {
        // Fetch unchosen characters from GameManager or another source if necessary
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            unchosenCharacters = gameManager.GetUnchosenCharacters(); // Fetch unchosen characters
        }
        else
        {
            Debug.LogError("GameManager instance not found. Make sure it's present in the scene.");
        }
    }
}
