using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePipe : MonoBehaviour
{
    [SerializeField] private float _speed = 0.65f;
    private float currentSpeed;
    private PipeSpawner pipespawner;

    private void Start()
    {
        pipespawner = FindObjectOfType<PipeSpawner>(); // Find once at start
        currentSpeed = _speed;
    }

    private void Update()
    {
        if (pipespawner != null)
        {
            // Update the speed if the speedup is active
            if (pipespawner.speedup && pipespawner.speeding >= 2f)
            {
                currentSpeed = _speed * 1.5f;
            }
            else
            {
                currentSpeed = _speed;
            }

            // Move the pipe
            transform.position += Vector3.left * currentSpeed * Time.deltaTime;
        }
        else
        {
            Debug.LogError("PipeSpawner reference is missing.");
        }
    }

    // Method to set speed from outside
    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }
}

