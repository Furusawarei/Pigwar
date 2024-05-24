using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

public class TimerController : MonoBehaviour
{
    public float countdownMinutes;
    private float countdownSeconds;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI finishText;


    [SerializeField] private GameObject startImage;

    [SerializeField] private GameObject resultButton;

    void Start()
    {
        countdownSeconds = countdownMinutes * 60;
    }


    void Update()
    {
        //  タイマーの処理
        if (startImage.activeSelf) return;
        if (countdownSeconds > 0)
        {
            countdownSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)countdownSeconds);
            timeText.text = span.ToString(@"mm\:ss");

            //if(countdownSeconds <= 0) 
            //{
            //    timer = false;
            //}
        }

        if (FadeManager.Instance.IsFading) return;
        // タイマーが0になった時の処理
        if (countdownSeconds < 0)
        {
            // 終わり！が出る
            finishText.text = ("おわり！");

            // タイマー0に見えるように
            timeText.text = ("00:00");

            FadeManager.Instance.TransScene("Result", 2.0f);
        }
    }

}