using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class FlyBehaviour : MonoBehaviour
{
    [SerializeField] public float _velocity = 1.5f;
    [SerializeField] public float _rotationSpeed = 10f;
    [SerializeField] private string particlePrefabPath = "Assets/Resources/Particles/ParticleSystem"; // Path to the particle prefab

    private Rigidbody2D _rb;
    private bool canControl = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (canControl && (Mouse.current.leftButton.wasPressedThisFrame || Touchscreen.current?.primaryTouch.press.wasPressedThisFrame == true))
        {
            _rb.velocity = Vector2.up * _velocity;
            SpawnParticleEffect(); // Call the function to spawn the particle
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0, 0, _rb.velocity.y * _rotationSpeed);
    }

    public void SetControl(bool controlStatus)
    {
        canControl = controlStatus;

        if (!canControl)
        {
            FreezePosition();
        }
        else
        {
            UnfreezePosition();
        }
    }

    private void FreezePosition()
    {
        _rb.velocity = Vector2.zero;
        _rb.isKinematic = true;
    }

    private void UnfreezePosition()
    {
        _rb.isKinematic = false;
    }

    private void SpawnParticleEffect()
    {
        GameObject particlePrefab = Resources.Load<GameObject>(particlePrefabPath);

        if (particlePrefab != null)
        {
            // Position the particle effect behind the character
            Vector3 spawnPosition = transform.position - new Vector3(0, 1, 0); // Adjust offset as needed

            // Instantiate the particle system
            GameObject particle = Instantiate(particlePrefab, spawnPosition, Quaternion.identity);

            // Optionally, destroy the particle system after its duration
            float particleDuration = particle.GetComponent<ParticleSystem>().main.duration;
            Destroy(particle, particleDuration);
        }
        else
        {
            Debug.LogWarning($"Particle Prefab with path {particlePrefabPath} not found.");
        }
    }
}


