using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public List<GameObject> followers = new List<GameObject>(); // List of follower GameObjects
    private GameObject currentPlayer;
    private int currentFollowerIndex = 0;

    void Start()
    {
        int selectedCharacterIndex = PlayerPrefs.GetInt("SelectedCharacterIndex");
        GameObject selectedCharacterPrefab = FindObjectOfType<CharacterManager>().GetSelectedCharacterPrefab();
        currentPlayer = Instantiate(selectedCharacterPrefab, transform.position, Quaternion.identity);

        // Set up followers to follow the player
        foreach (GameObject follower in followers)
        {
            follower.GetComponent<FollowPlayer>().player = currentPlayer.transform;
        }
    }

    void Update()
    {
        if (currentPlayer == null && currentFollowerIndex < followers.Count)
        {
            currentPlayer = followers[currentFollowerIndex];
            currentFollowerIndex++;
            followers.RemoveAt(currentFollowerIndex);
            currentPlayer.GetComponent<FollowPlayer>().enabled = false;

            foreach (GameObject follower in followers)
            {
                follower.GetComponent<FollowPlayer>().player = currentPlayer.transform;
            }
        }
    }
}
