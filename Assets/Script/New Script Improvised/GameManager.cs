using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] characterPrefabs; // All available character prefabs
    private List<GameObject> unchosenCharacters;

    private void Start()
    {
        // Initialize unchosen characters
        unchosenCharacters = new List<GameObject>(characterPrefabs);
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex", -1);

        if (selectedCharacterIndex >= 0 && selectedCharacterIndex < characterPrefabs.Length)
        {
            // Remove the selected character from the list
            unchosenCharacters.RemoveAt(selectedCharacterIndex);
        }
    }

    public List<GameObject> GetUnchosenCharacters()
    {
        return unchosenCharacters;
    }
}
