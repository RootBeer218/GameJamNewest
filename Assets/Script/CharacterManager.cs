using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public SpriteRenderer sr;
    public List<Sprite> characters = new List<Sprite>(); // List of character sprites
    public GameObject[] characterPrefabs; // Array to hold all character prefabs
    private int selectedCharacter = 0;

    private void Start()
    {
        sr.sprite = characters[selectedCharacter];
    }

    public void NextOptions()
    {
        selectedCharacter = (selectedCharacter + 1) % characterPrefabs.Length;
        sr.sprite = characters[selectedCharacter];
    }

    public void BackOptions()
    {
        selectedCharacter = (selectedCharacter - 1 + characterPrefabs.Length) % characterPrefabs.Length;
        sr.sprite = characters[selectedCharacter];
    }

    public void PlayGame()
    {
        // Store the selected character index using PlayerPrefs
        PlayerPrefs.SetInt("SelectedCharacterIndex", selectedCharacter);
        PlayerPrefs.Save(); // Save immediately

        SceneManager.LoadScene("SampleScene"); // Ensure this is the correct scene name
    }

    public GameObject GetSelectedCharacterPrefab()
    {
        return characterPrefabs[selectedCharacter];
    }
}



