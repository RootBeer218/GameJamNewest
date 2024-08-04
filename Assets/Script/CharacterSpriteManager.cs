using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteChanger : MonoBehaviour
{
    public Sprite jumpSprite;  // Sprite for jumping
    public Sprite fallSprite;  // Sprite for falling

    private SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer component
    private Rigidbody2D rb;  // Reference to the Rigidbody2D component

    private void Start()
    {
        // Get the SpriteRenderer and Rigidbody2D components
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Check the vertical velocity to determine if the character is jumping or falling
        if (rb.velocity.y > 0)
        {
            // Character is jumping
            spriteRenderer.sprite = jumpSprite;
        }
        else if (rb.velocity.y < 0)
        {
            // Character is falling
            spriteRenderer.sprite = fallSprite;
        }
    }
}
