using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PyMv : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Rigidbody rb;
    private float upForce;
    private bool Jumping = false;
    public float moveSpeed = 5f;
    private TouchObject touchObject;

    public AudioClip jumpSound;
    public AudioClip playerCollisionSound;
    public AudioSource audioSource;

    private bool canMove = true;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();

        upForce = 150;
        rb = GetComponent<Rigidbody>();

        touchObject = GetComponent<TouchObject>();
    }

    void Update()
    {
        if (!canMove)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        if (!FadeText.canMove)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        var pos = _playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(pos.x, 0, pos.y) * moveSpeed;

        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        if (move.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(move.x, 0, move.z));
        }

        if (_playerInput.actions["Jump"].triggered && !Jumping && (touchObject == null || !touchObject.IsHoldingObject))
        {
            rb.AddForce(new Vector3(0, upForce, 0));
            Jumping = true;

            if (jumpSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
            else
            {
                Debug.LogWarning("jumpSound or audioSource is null.");
            }
        }

        Debug.Log(Jumping);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("boxPrefab"))
        {
            Jumping = false;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerCollisionSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(playerCollisionSound);
            }
            else
            {
                Debug.LogWarning("playerCollisionSound or audioSource is null.");
            }
        }
    }

    public void DisableMovement()
    {
        canMove = false;
        if (touchObject != null)
        {
            touchObject.DisableInteraction();
        }
    }
}
