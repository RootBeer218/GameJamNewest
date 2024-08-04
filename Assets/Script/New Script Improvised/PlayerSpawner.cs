using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform spawnPoint; // Set this in the editor to determine where the player will spawn

    void Start()
    {
        // Get the selected character index from PlayerPrefs
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex");

        // Find the CharacterManager in the scene
        CharacterManager characterManager = FindObjectOfType<CharacterManager>();
        if (characterManager != null)
        {
            // Get the selected character prefab
            GameObject selectedCharacterPrefab = characterManager.GetSelectedCharacterPrefab();

            if (selectedCharacterPrefab != null)
            {
                // Instantiate the selected character at the spawn point
                Instantiate(selectedCharacterPrefab, spawnPoint.position, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Selected character prefab is null.");
            }
        }
        else
        {
            Debug.LogError("CharacterManager not found in the scene.");
        }
    }
}
