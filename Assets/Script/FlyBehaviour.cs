using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlyBehaviour : MonoBehaviour
{
    [SerializeField] public float _velocity = 1.5f;
    [SerializeField] public float _rotationSpeed = 10f;

    private Rigidbody2D _rb;
    private bool canControl = true; // Flag to control player input

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (canControl && (Mouse.current.leftButton.wasPressedThisFrame || Touchscreen.current?.primaryTouch.press.wasPressedThisFrame == true))
        {
            _rb.velocity = Vector2.up * _velocity;
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
}

