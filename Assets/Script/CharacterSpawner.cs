using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject[] characterPrefabs; // Array to hold character prefabs
    private int selectedCharacterIndex;

    private void Start()
    {
        // Get the selected character index from PlayerPrefs
        selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex");

        // Check if the index is valid
        if (selectedCharacterIndex >= 0 && selectedCharacterIndex < characterPrefabs.Length)
        {
            // Get the prefab based on the selected index
            GameObject selectedCharacterPrefab = characterPrefabs[selectedCharacterIndex];

            // Instantiate the selected character prefab at the position of the GameObject this script is attached to
            if (selectedCharacterPrefab != null)
            {
                GameObject player = Instantiate(selectedCharacterPrefab, transform.position, Quaternion.identity);

                // Enable the FlyBehaviour script on the selected character
                FlyBehaviour flyBehaviour = player.GetComponent<FlyBehaviour>();
                if (flyBehaviour != null)
                {
                    flyBehaviour.enabled = true;
                }
                else
                {
                    Debug.LogError("FlyBehaviour script is missing on the selected character prefab.");
                }
            }
            else
            {
                Debug.LogError("Selected character prefab is null.");
            }
        }
        else
        {
            Debug.LogError("Selected character index is out of range.");
        }
    }
}


