using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// スコアの表示と、障害物の管理用クラス
/// </summary>
public class ScoreUp : MonoBehaviour
{
    // 他関数
    public Scoremaneger scoreManager;

    // あべさんのスコアとスコアを表示するテキストリストをいれるところ
    protected int score1;
    protected int score2;
    private TextMeshProUGUI scoreTextList;

    // 障害物を生成する場所
    [SerializeField] private Transform boxGeneratePos;
    [SerializeField] private Transform boxGeneratePos2;

    [SerializeField] private GameObject boxPrefab;    // Player1の障害物
    [SerializeField] protected GameObject boxPrefab2;   // Player2の障害物

    [Header("Player1の障害物"), SerializeField] List<GameObject> boxList = new List<GameObject>();    // Player1の生成した障害物を保存しておくためのリスト
    [Header("Player2の障害物"), SerializeField] List<GameObject> boxList2 = new List<GameObject>();    // Player2の障害物

    // 障害物管理時に使う (リスト用)
    private int countPrefabs;


    public void Update()
    {
        // Player2の障害物生成用関数呼び出し
        GeneratePrefabs();

        // Player2の障害物生成用関数呼び出し
        GeneratePrefabs2();
    }

    //void GeneratePrefabs()
    //{
    //    // あべさんの変数たち
    //    score1 = scoreManager.PlayerScore[0];
    //    TextMeshProUGUI[] scoreTextList = scoreManager._scoreborad;

    //    // Player1の障害物生成

    //    // ✕ボタン（Zキー）を押したときスコアが２以上なら
    //    // 障害物をプレイヤー１の目の前に生成する   
    //    if (score1 < 2) return;
    //    if (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Z))
    //    {
    //        // Player2の前にboxPrefab（障害物）を生成
    //        var obj = Instantiate(boxPrefab, boxGeneratePos.transform.position, Quaternion.identity);

    //        // Player1のスコアを2減らす
    //        score1 -= 2;
    //        scoreTextList[0].text = string.Format("Player1:{0}", score1);
    //        Debug.Log("player1:" + score1);

    //        // リストに追加
    //        obj.name = "cube" + countPrefabs;
    //        countPrefabs++;
    //        boxList.Add(obj);

    //        // Player2の障害物の数を管理
    //        // boxPrefab(障害物)の数が3個になった時、
    //        // 1番最初に生成された障害物を消す
    //        if (boxList.Count > 2)
    //        {
    //            Destroy(boxList[0]);
    //            boxList.Remove(boxList[0]);
    //        }

    //    }

    //    // Player1の障害物の数
    //    int count = GameObject.FindGameObjectsWithTag("boxPrefab").Length;

    //    if (count <= 8) return;
    //    if (count == 8)
    //    {
    //        Destroy(boxPrefab);
    //    }

    //}

    /// <summary>
    /// Player1の障害物を管理する関数
    /// </summary>
    public void GeneratePrefabs()
    {
        // あべさんの所から来たスコア
        scoreManager.PlayerScore[0] = score1;

        //✕ボタン（Xキー）を押したときスコアが２以上なら
        //障害物をプレイヤー２の目の前に生成する   
        if (score1 < 2) return;
        if (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Z))
        {
            // あべさんの所から来たスコア
            score1 = scoreManager.PlayerScore[0];
            TextMeshProUGUI[] scoreTextList = scoreManager._scoreborad;


            // Player2の前にboxPrefab（障害物）を生成
            var obj = Instantiate(boxPrefab, boxGeneratePos.transform.position, Quaternion.identity);

            // Player2のスコアを2減らす
            score1 -= 2;
            scoreTextList[0].text = string.Format("Player1:{0}", score1);
            Debug.Log("player1:" + score1);

            // リストに追加
            obj.name = "cube" + countPrefabs;
            countPrefabs++;
            boxList.Add(obj);

            // Player2の障害物の数を管理
            // boxPrefab(障害物)の数が3個になった時、
            // 1番最初に生成された障害物を消す
            if (boxList.Count > 2)
            {
                Destroy(boxList2[0]);
                boxList.Remove(boxList[0]);
            }

        }

        // Player２の障害物の数
        int count2 = GameObject.FindGameObjectsWithTag("boxPrefab2").Length;

        if (count2 <= 8) return;
        if (count2 == 8)
        {
            Destroy(boxPrefab2);
        }

    }


    /// <summary>
    /// Player2の障害物を管理する
    /// </summary>
    public void GeneratePrefabs2()
    {
        // あべさんの所から来たスコア
        scoreManager.PlayerScore[1] = score2;

        //✕ボタン（Xキー）を押したときスコアが２以上なら
        //障害物をプレイヤー２の目の前に生成する   
        if (score2 < 2) return;
        if (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.X))
        {
            // あべさんの所から来たスコア
            score2 = scoreManager.PlayerScore[1];
            TextMeshProUGUI[] scoreTextList = scoreManager._scoreborad;


            // Player2の前にboxPrefab（障害物）を生成
            var obj = Instantiate(boxPrefab2, boxGeneratePos2.transform.position, Quaternion.identity);

            // Player2のスコアを2減らす
            score2 -= 2;
            scoreTextList[1].text = string.Format("Player2:{0}", score2);
            Debug.Log("player2:" + score2);

            // リストに追加
            obj.name = "cube" + countPrefabs;
            countPrefabs++;
            boxList2.Add(obj);

            // Player2の障害物の数を管理
            // boxPrefab(障害物)の数が3個になった時、
            // 1番最初に生成された障害物を消す
            if (boxList2.Count > 2)
            {
                Destroy(boxList2[0]);
                boxList2.Remove(boxList2[0]);
            }

        }

        // Player２の障害物の数
        int count2 = GameObject.FindGameObjectsWithTag("boxPrefab2").Length;

        if (count2 <= 8) return;
        if (count2 == 8)
        {
            Destroy(boxPrefab2);
        }

    }

    /// <summary>
    /// ボタンを押したらプレイヤー１のスコアが１上がる関数
    /// </summary>
    public void ClickScoreUp()
    {
        TextMeshProUGUI[] scoreTextList = scoreManager._scoreborad;

        score1++;
        scoreTextList[0].text = string.Format("Player1:{0}", score1);

        Debug.Log("player1:" + score1);
    }

    public void ClickScoreUp2()
    {
        TextMeshProUGUI[] scoreTextList = scoreManager._scoreborad;

        score2++;
        scoreTextList[1].text = string.Format("Player2:{0}", score2);

        Debug.Log("player2:" + score2);
    }

}


