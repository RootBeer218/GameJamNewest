using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPipe : MonoBehaviour
{
    private Transform pipeTransform;
    private Vector3 offset;

    // Initialize with the pipe's Transform
    public void Initialize(Transform pipeTransform)
    {
        this.pipeTransform = pipeTransform;
        offset = transform.position - pipeTransform.position;
    }

    private void Update()
    {
        if (pipeTransform != null)
        {
            // Update position to follow the pipe
            transform.position = pipeTransform.position + offset;
        }
    }
}
