using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Test1 : MonoBehaviour
{
    [SerializeField] private int playerNumber; // プレイヤー番号を設定するための変数
    [SerializeField] private GameObject boxPrefab; // プレハブを設定するための変数
    [SerializeField] private Transform boxGeneratePos; // 生成位置を設定するための変数
    [SerializeField] private int maxPrefabs = 8; // 保持するプレハブの最大数

    private Scoremaneger scoreManager; // スコアを管理するクラスを参照するための変数
    private AudioSource audioSource;
    public AudioClip summonSE; // プレハブを生成する際のSE

    private PlayerInput _playerInput;
    private List<GameObject> instantiatedPrefabs = new List<GameObject>();

    private void Start()
    {
        // ScoreManagerのインスタンスをシーン内から取得する（例：ScoreManagerがシーン内の別のオブジェクトにアタッチされている場合）
        scoreManager = FindObjectOfType<Scoremaneger>();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (_playerInput.actions["Summon"].triggered)
        {
            SummonBox();
        }
    }

    public void SummonBox()
    {
        int scoreToConsume = 2; // 消費するスコア

        // スコアが2以上か確認
        if (scoreManager.GetScore(playerNumber) >= scoreToConsume)
        {
            // スコアを減らす
            scoreManager.ScoreChenge(-scoreToConsume, playerNumber);

            // プレイヤーの現在位置を取得
            Vector3 playerPosition = transform.position;

            // プレイヤーの前方向に1メートル進んだ位置を計算
            Vector3 spawnPosition = playerPosition + transform.forward * 1.0f;

            // プレハブを生成
            GameObject newBox = Instantiate(boxPrefab, spawnPosition, Quaternion.identity);

            // プレハブの名前を設定（例：cube0, cube1など）
            newBox.name = "BoxPlafab" + playerNumber;

            // 生成したプレハブをリストに追加
            instantiatedPrefabs.Add(newBox);

            // プレハブの数が最大数を超えたら古いものから削除
            if (instantiatedPrefabs.Count > maxPrefabs)
            {
                GameObject oldPrefab = instantiatedPrefabs[0];
                instantiatedPrefabs.RemoveAt(0);
                Destroy(oldPrefab);
            }

            // 他の処理（UIの更新、SEの再生など）
            if (summonSE != null)
            {
                audioSource.PlayOneShot(summonSE);
            }
        }
        else
        {
            // スコアが足りない場合の処理を記述
            Debug.Log("Score is not enough to summon the box.");
        }
    }
}
