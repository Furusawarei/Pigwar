using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput2 : MonoBehaviour
{
    private ActionControl _actionControl;
    private Rigidbody rb;
    private float upForce;
    private bool Jumping = false;
    
    public AudioClip jumpSound;
    public AudioSource audioSource;
    internal object actions;

    private void Awake()
    {
        _actionControl = new ActionControl();
        _actionControl.Enable();
    }

    void Start()
    {
        upForce = 150;
        rb = GetComponent<Rigidbody>(); // リジッドボディの取得
    }

    void Update()
    {
        // プレイヤーの移動
        var pos = _actionControl.Player1.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(pos.x, 0, pos.y) * 0.03f;
        transform.position += move;

        // プレイヤーの向きを移動方向に変更
        //修正後
        if (move.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(move);
        }

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

        // ジャンプの処理
        if (_actionControl.Player1.Jump.triggered && !Jumping)
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
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Jumping = false;
        }
    }
}
