using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnchosenCharacter : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public Transform pipe;   // Reference to the pipe's transform
    public float followSpeed = 2f; // Speed at which the character follows the player
    private bool isFollowing = false;

    private void Update()
    {
        if (isFollowing && player != null)
        {
            // Move towards the player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * followSpeed * Time.deltaTime;
        }

        // Sync position with the pipe's movement
        if (pipe != null)
        {
            transform.position = new Vector3(transform.position.x, pipe.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isFollowing = true;
        }
    }
}

