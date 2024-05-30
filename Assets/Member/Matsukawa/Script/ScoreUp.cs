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

    // Player1の障害物
    public GameObject boxPrefab;

    // Player2の障害物
    public GameObject boxPrefab2;

    public void FixedUpdate()
    {
        //✕ボタン（Zキー）を押したときスコアが２以上なら
        //障害物をプレイヤー１の目の前に生成する   
        if (score1 <= 2) return;
        if (Input.GetKeyDown("joystick button 1") || Input.GetKey(KeyCode.Z))
        {
            GeneratePrefabs();
        }

    }
    public void Update()
    {
        //✕ボタン（Xキー）を押したときスコアが２以上なら
        //障害物をプレイヤー２の目の前に生成する   
        if (score2 <= 2) return;
        if (Input.GetKeyDown("joystick button 1") || Input.GetKey(KeyCode.X))
        {
            GeneratePrefabs2();
        }




        // Player1の障害物の数
        int count = GameObject.FindGameObjectsWithTag("boxPrefab").Length;

        if (count <= 8) return;
        if (count == 8)
        {
            Destroy(boxPrefab);
        }


        // Player２の障害物の数
        int count2 = GameObject.FindGameObjectsWithTag("boxPrefab2").Length;

        if (count2 <= 8) return;
        if (count2 == 8)
        {
            Destroy(boxPrefab2);
        }

    }

    public void GeneratePrefabs()
    {
        score1--;

        var boxPrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
        boxPrefab.transform.position = transform.position + playerPos.forward * 1;

        score1Text.text = string.Format("Player1:{0}", score1);
        Debug.Log("player1:" + score1);

    }

    public void GeneratePrefabs2()
    {
        score2--;

        var boxPrefab2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        boxPrefab.transform.position = transform.position + playerPos2.forward * 1;

        score2Text.text = string.Format("Player2:{0}", score2);
        Debug.Log("player2:" + score2);

    }


    /// <summary>
    /// ボタンを押したらプレイヤー１のスコアが１上がる関数
    /// </summary>
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


