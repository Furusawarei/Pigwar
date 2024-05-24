using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// スコアと障害物の管理用クラス
/// </summary>
public class ScoreUp : MonoBehaviour
{
    [SerializeField] public int score1;
    [SerializeField] public TextMeshProUGUI score1Text;

    [SerializeField] public int score2;
    [SerializeField] public TextMeshProUGUI score2Text;

    [SerializeField] public Transform playerPos;
    [SerializeField] public Transform playerPos2;

    // 障害物
    public GameObject boxPrefab;

    public void Update()
    {
        //✕ボタン（Zキー）を押したときスコアが２以上なら
        //障害物をプレイヤー１の目の前に生成する   
        if (score1 <= 2) return;
        if (Input.GetKeyDown("joystick button 1") || Input.GetKey(KeyCode.Z))
        {
            score1--;

            var boxPrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
            boxPrefab.transform.position = transform.position + playerPos.forward * 1;

            score1Text.text = string.Format("Player1:{0}", score1);
            Debug.Log("player1:" + score1);

        }

        //✕ボタン（Xキー）を押したときスコアが２以上なら
        //障害物をプレイヤー２の目の前に生成する   
        if (score2 <= 2) return;
        if (Input.GetKeyDown("joystick button 1") || Input.GetKey(KeyCode.X))
        {
            score2--;

            var boxPrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
            boxPrefab.transform.position = transform.position + playerPos2.forward * 1;

            score2Text.text = string.Format("Player2:{0}", score2);
            Debug.Log("player2:" + score2);

        }

    }

    public void ClickScoreUp()
    {
        score1++;
        score1Text.text = string.Format("Player1:{0}", score1);

        Debug.Log("player1:" + score1);
    }

    public void ClickScoreUp2()
    {
        score2++;
        score2Text.text = string.Format("Player2:{0}", score2);

        Debug.Log("player2:" + score2);
    }

}


