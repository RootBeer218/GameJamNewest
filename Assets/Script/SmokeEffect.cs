using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SmokeEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public bool canControl = true; // Set this to true when the player can control the action

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.enabled = false;
    }

    void Update()
    {
        // Check if the player can control and if the left mouse button or touchscreen is pressed
        if (canControl && (Mouse.current.leftButton.wasPressedThisFrame || Touchscreen.current?.primaryTouch.press.wasPressedThisFrame == true))
        {
            StartCoroutine(ToggleVisibility());
        }
    }
	
	public void TriggerSmokeEffect()
    {
        StartCoroutine(ToggleVisibility());
    }

    private IEnumerator ToggleVisibility()
    {
        // Set the sprite renderer to active (true)
        spriteRenderer.enabled = true;

        // Wait for 1 second
        yield return new WaitForSeconds(0.5f);

        // Set the sprite renderer to inactive (false)
        spriteRenderer.enabled = false;
    }
}
