using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score2 : MonoBehaviour
{
      private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pearl"))
        {
            // プレイヤー番号とスコアの増加値を指定
            int playerNumber = 1; // ここではプレイヤー1にスコアを加算
            int scoreToAdd = 1;

            // ScoreManager のインスタンスを取得してスコアを変更
            Scoremaneger.Instance().ScoreChenge(scoreToAdd, playerNumber);

            // オプション: パールオブジェクトを破壊する場合
            // Destroy(other.gameObject);
        }
    }
}
