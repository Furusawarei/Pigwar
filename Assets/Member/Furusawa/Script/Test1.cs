using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Test1 : MonoBehaviour
{
    [SerializeField] private ActionControl _actionControl;
    [SerializeField] private GameObject boxPrefab2; // 障害物のプレハブ
    [SerializeField] private Transform playerTransform; // プレイヤーのTransform
    [SerializeField] private float spawnDistance = 2.0f; // プレイヤーの前に生成する距離

    private Scoremaneger scoreManager;

    private void Awake()
    {
        _actionControl = new ActionControl();
        _actionControl.Enable();
        scoreManager = Scoremaneger.Instance();
    }

    private void Update()
    {
        Generate();
    }

    public void Generate()
    {
        // プレイヤー2のスコアを取得
        int score2 = scoreManager.GetScore(2);

        // スコアが2未満なら生成しない
        if (score2 < 2) return;

        // プレイヤー1の召喚アクションがトリガーされたとき
        if (_actionControl.Player1.Summon.triggered)
        {
            // プレイヤーの前方に障害物を生成
            Vector3 spawnPosition = playerTransform.position + playerTransform.forward * spawnDistance;
            Instantiate(boxPrefab2, spawnPosition, Quaternion.identity);

            // スコアを減らす
            scoreManager.ScoreChenge(-2, 2);
        }

        // "boxPrefab2"タグを持つオブジェクトの数を数える
        int count2 = GameObject.FindGameObjectsWithTag("boxPrefab2").Length;

        // オブジェクトが8個を超えた場合は古いものを破壊する
        if (count2 > 8)
        {
            GameObject oldestBox = GameObject.FindWithTag("boxPrefab2");
            if (oldestBox != null)
            {
                Destroy(oldestBox);
            }
        }
    }
}
