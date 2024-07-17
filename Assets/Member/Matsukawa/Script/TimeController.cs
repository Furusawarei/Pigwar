using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

/// <summary>
/// タイマー管理用のクラス
/// </summary>
public class TimerController : MonoBehaviour
{
    // タイマー
    public float countdownMinutes;
    private float countdownSeconds;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI finishText;

    [SerializeField] private GameObject startImage;
    //[SerializeField] private GameObject resultButton;

    //スコア用テキスト
    [SerializeField] private List<TextMeshProUGUI> _scoreborad;

    void Start()
    {
        countdownSeconds = countdownMinutes * 60;

        //初期化
        Scoremaneger.Instance().SetScore(0, 1);
        Scoremaneger.Instance().SetScore(0, 2);
        //0を表示する
        _scoreborad[0].text = Scoremaneger.Instance().PlayerScore[0].ToString();
        _scoreborad[1].text = Scoremaneger.Instance().PlayerScore[1].ToString();
    }


    void Update()
    {
        // タイマーの開始
        if (startImage.activeSelf) return;
        if (countdownSeconds > 0)
        {
            countdownSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)countdownSeconds);
            timeText.text = span.ToString(@"mm\:ss");
        }

        if (FadeManager.Instance.IsFading) return;

        // タイマーが0になった時の処理
        if (countdownSeconds < 0)
        {
            Debug.Log("タイマーが0になりました");

            // 終了メッセージを表示
            finishText.text = ("終了！");

            // タイマーを0に設定
            timeText.text = ("00:00");

            // スコアをリザルト型に移動
            Scoremaneger.Instance().ToResult();

            // シーン遷移
            FadeManager.Instance.TransScene("MatukawaResult_Copy", 2.0f);
        }
    }

}
