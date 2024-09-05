using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Rigidbody rb;
    private float upForce;
    private bool Jumping = false;
    public float moveSpeed = 5f; // 移動スピードを設定
    private TouchObject touchObject;

    public AudioClip jumpSound;
    public AudioClip playerCollisionSound; // プレイヤー同士の衝突音
    public AudioSource audioSource;

    public FadeText fadeText;
    public bool GameFinished { get; set; } = false; // ゲームが終了したかどうかを示すフラグ

    private Animator _animator; // プレイヤーのアニメーター

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>(); // リジッドボディの取得
        touchObject = GetComponent<TouchObject>();
        _animator = GetComponent<Animator>(); // アニメーターの取得
        upForce = 150;
    }

    void Update()
    {
        /*
        // ゲーム終了時や動けないときに操作を無効にする処理（変更前コード）
        if (GameFinished || !FadeText.canMove)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        // 前フレームとの位置の差分を計算してプレイヤーの向きを変更する処理（変更前コード）
        Vector3 diff = transform.position - BefoPos;
        diff.y = 0;
        BefoPos = transform.position;
        if (diff.magnitude > 0.01f)
        {
           transform.rotation = Quaternion.LookRotation(diff);
        }
        */

        _animator.SetBool("Idel", true);

        // ゲーム終了時や動けないときに操作を無効にする処理（コメントアウト）
        if (GameFinished || !FadeText.canMove)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        // プレイヤーの移動
        var pos = _playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 move = new Vector3(pos.x, 0, pos.y) * moveSpeed;

        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);

        // プレイヤーの向きを移動方向に変更
        if (move.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(new Vector3(move.x, 0, move.z));
            _animator.SetBool("Move", true); // 移動している時にMoveBoolをtrueに設定
            //_animator.SetBool("Idel", false);
        }
        else
        {
            _animator.SetBool("Move", false); // 移動していない時にMoveBoolをfalseに設定
            //_animator.SetBool("Idel", true);
        }

        // ジャンプの処理
        if (_playerInput.actions["Jump"].triggered && !Jumping && (touchObject == null || !touchObject.IsHoldingObject))
        {
            rb.AddForce(new Vector3(0, upForce, 0));
            Jumping = true;
            _animator.SetTrigger("Jumping");
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

    void JumpFlug()
    {
        Jumping = false;
    }
}
