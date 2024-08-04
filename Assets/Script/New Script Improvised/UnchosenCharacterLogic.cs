using UnityEngine;

public class UnchosenCharacterLogic : MonoBehaviour
{
    public float followDistance = 2f; // Distance behind the player where the character should follow
    private Transform player; // Reference to the player
    private Rigidbody2D rb;
    private bool shouldFollow = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.gravityScale = 0; // Set gravity scale to 0 initially
    }

    private void Update()
    {
        // Check for the player if not already set
        if (player == null)
        {
            player = FindObjectOfType<PlayerController>()?.transform; // Get the player
            if (player != null)
            {
                shouldFollow = true; // Start following once player is found
            }
        }

        // If following the player, move towards them
        if (shouldFollow && player != null)
        {
            Vector3 targetPosition = player.position - (player.right * followDistance);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
        }
    }

    public void SetFollowPlayer(bool follow)
    {
        shouldFollow = follow;
    }

    public void SetPlayer(Transform newPlayer)
    {
        player = newPlayer;
    }
}
