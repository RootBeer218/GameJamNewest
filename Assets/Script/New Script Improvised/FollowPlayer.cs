using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player
    public float followSpeed = 2f; // Speed at which the character will follow the player
    public float characterSpeed = 8f; // Speed of the unchosen character
    public float xOffset = 8f; // Offset from the player on the X-axis

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
            // Calculate the target position exactly behind the player
            Vector3 targetPosition = new Vector3(player.position.x + xOffset, player.position.y, transform.position.z);
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            rb.MovePosition(transform.position + moveDirection * characterSpeed * Time.deltaTime);
        }
    }
}
