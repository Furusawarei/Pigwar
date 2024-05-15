using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private InputActions _actionControl;

    public Vector3 direction; //追加
    // Start is called before the first frame update

    private void Awake()
    {
        _actionControl = new InputActions();
        _actionControl.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        // プレイヤーの入力を取得
        var input = _actionControl.Player.Move.ReadValue<Vector2>();

        // 入力に基づいて進行方向を計算
        direction = new Vector3(input.x, 0, input.y).normalized;

        // 進行方向がある場合にのみ回転を変更
        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }

        // プレイヤーの位置を更新
        transform.localPosition += direction * 0.005f;
    }
}

