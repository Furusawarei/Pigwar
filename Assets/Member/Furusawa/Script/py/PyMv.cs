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

    public float moveSpeed = 5f; // 移動スピードを設定

    public AudioClip jumpSound;
    public AudioClip playerCollisionSound; // プレイヤー同士の衝突音
    public AudioSource audioSource;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();

        upForce = 150;
        rb = GetComponent<Rigidbody>(); // リジッドボディの取得
    }

    void Update()
    {
        // プレイヤーの移動
        var pos = _playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(pos.x, 0, pos.y) * moveSpeed;

        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        // プレイヤーの向きを移動方向に変更
        if (move.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(move.x, 0, move.z));
        }

        // ジャンプの処理
        if (_playerInput.actions["Jump"].triggered && !Jumping)
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
        if (collision.gameObject.CompareTag("Ground"))
        {
            Jumping = false;
        }
        if(collision.gameObject.CompareTag("boxPrefab"))
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
}
