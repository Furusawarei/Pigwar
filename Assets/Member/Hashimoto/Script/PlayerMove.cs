using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private PlayerInput _playerInput;
    public Animator _animator;

    private Rigidbody rb;
    private float upForce;
    private bool Jumping = false;

    public AudioClip jumpSound;
    public AudioSource audioSource;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();

        upForce = 150;
        rb = GetComponent<Rigidbody>(); // リジッドボディの取得
    }

    void Update()
    {
        // プレイヤーの移動
        var pos = _playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(pos.x, 0, pos.y) * 0.03f;
        transform.position += move;

        /*
        //変更前
        Vector3 diff = transform.position - BefoPos; // 前フレームとの位置の差分を計算
        diff.y = 0;
        BefoPos = transform.position;
        if (diff.magnitude > 0.01f)
        {
           transform.rotation = Quaternion.LookRotation(diff);
        }
        */

        // プレイヤーの向きを移動方向に変更
        if (move.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(move);
            _animator.SetBool("running", true);
        }
        else
        {
            _animator.SetBool("running", false);
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
    }
}

