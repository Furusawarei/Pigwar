using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    [SerializeField] private Scoremaneger scoremaneger; // スコアマネージャーの参照
    [SerializeField] public int playerNumber; // プレイヤーの番号

    public AudioClip pearlSE; // パール取得時のSE
    private AudioSource audioSource; // 音源コンポーネント

    private void Start()
    {
        // 音源コンポーネントの取得または追加
        audioSource = GetComponent<AudioSource>(); 
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();  
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // トリガーに入ったオブジェクトのタグが "Pearl" または "HoldobjectTag" の場合
        if (other.CompareTag("Pearl") || other.CompareTag("Holdobject"))
        {
            int scoreToAdd = 1; // 追加するスコア

            // スコアを変更
            Scoremaneger.Instance().ScoreChenge(scoreToAdd, playerNumber);

            // タグを "Default" に変更
            other.gameObject.tag = "Default";

            other.transform.parent = transform;

            // SEを再生
            if (pearlSE != null && audioSource != null)
            {
                audioSource.PlayOneShot(pearlSE);
            }
        }
    }
}
