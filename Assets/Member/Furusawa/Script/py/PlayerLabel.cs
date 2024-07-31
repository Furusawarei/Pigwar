using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLabel : MonoBehaviour
{
    public int playerNumber;
    private TextMeshPro label;
    private Camera mainCamera;

    void Start()
    {
        label = GetComponentInChildren<TextMeshPro>();
        mainCamera = Camera.main; // メインカメラを取得

        if (playerNumber == 1)
        {
            label.text = "P1";
        }

        if (playerNumber == 2)
        {
            label.text = "P2";
        }
    }

    void LateUpdate()
    {
        // ラベルを常にカメラの方向に向ける
        label.transform.rotation = Quaternion.LookRotation(label.transform.position - mainCamera.transform.position);
    }
}
