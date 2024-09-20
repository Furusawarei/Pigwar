using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;

public class ObstacleSummon : MonoBehaviour
{
    [SerializeField] private int playerNumber; // プレイヤー番号を設定するための変数
    [SerializeField] private GameObject boxPrefab; // プレハブを設定するための変数
    [SerializeField] private Transform boxGeneratePos; // 生成位置を設定するための変数
    [SerializeField] private int maxPrefabs = 2; // 保持するプレハブの最大数

    private Scoremaneger scoreManager; // スコアを管理するクラスを参照するための変数
    private AudioSource audioSource;
    public AudioClip summonSE; // プレハブを生成する際のSE

    private PlayerInput _playerInput;
    private List<GameObject> instantiatedPrefabs = new List<GameObject>();

    // スプライトを変更するためのUI要素とスプライトを設定
    [SerializeField] private Image uiSprite; // UIのスプライトを表示するImageコンポーネント
    [SerializeField] private Sprite sprite1; // 1個未満の時のスプライト
    [SerializeField] private Sprite sprite2; // 2個の時のスプライト
    [SerializeField] private Sprite sprite4; // 4個の時のスプライト

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
        UpdateSprite(); // 初期スプライトの設定
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
    int scoreToConsume = 1; // 消費するスコア

    // スコアが2以上か確認
    if (scoreManager.GetScore(playerNumber) >= scoreToConsume)
    {
        // スコアを減らす
        scoreManager.ScoreChenge(-scoreToConsume, playerNumber);

        // プレイヤーの現在位置を取得
        Vector3 playerPosition = transform.position;

        // プレイヤーの前方向に1.5メートル進んだ位置を計算
        // さらにY軸方向に+1メートルのオフセットを追加
        Vector3 spawnPosition = playerPosition + transform.forward * 1.5f + Vector3.up * 1.0f;

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

        // スプライトの更新
        UpdateSprite();

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


    private void UpdateSprite()
    {
        if (instantiatedPrefabs.Count >= 2)
        {
            uiSprite.sprite = sprite4;
        }
        else if (instantiatedPrefabs.Count >= 1)
        {
            uiSprite.sprite = sprite2;
        }
        else
        {
            uiSprite.sprite = sprite1;
        }
    }
}
