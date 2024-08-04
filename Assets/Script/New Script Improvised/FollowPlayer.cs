using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float followRadius = 5f; // Radius within which the character will follow the player
    public float followSpeed = 2f; // Speed at which the character will follow the player
    public Vector3 offset = new Vector3(-2f, 0, 0); // Offset from the player

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Call the method to find the player and assign it
        StartCoroutine(FindAndAssignPlayer());
    }

    private IEnumerator FindAndAssignPlayer()
    {
        // Wait until the player is instantiated
        while (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            yield return null;
        }

        // Now the player is assigned, the FollowPlayer logic will work
    }

    private void Update()
    {
        if (player != null)
        {
            // Calculate the distance between the character and the player
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance <= followRadius)
            {
                // Move the character towards the player
                Vector3 targetPosition = player.position + offset;
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                rb.MovePosition(transform.position + moveDirection * followSpeed * Time.deltaTime);
            }
        }
    }
}