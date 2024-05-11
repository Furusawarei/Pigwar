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
    private float countdownSeconds ;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI finishText;

    // タイマーが付いているかどうか
    private bool timer = true; 

    [SerializeField] private GameObject startImage;

    [SerializeField] private GameObject resultButton;
    void Start()
    {
        //StartCoroutine("DelayCroutine");
        countdownSeconds = countdownMinutes * 60;
    }

    //IEnumerator DelayCroutine()
    //{
    //    yield return new WaitForSeconds(3f);
    //    Debug.Log("コルーチンはじめ");

    //    Timer();
    //}

    void Update()
    {
        //Debug.Log("Timerの処理はじめ");

        //  タイマーの処理
        if (startImage.activeSelf) return;
        if (countdownSeconds >= 0)
        {
            countdownSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)countdownSeconds);
            timeText.text = span.ToString(@"mm\:ss");

            if(countdownSeconds <= 0) 
            {
                timer = false;
            }
        }


        // タイマーが0になった時の処理
        if(countdownSeconds <= 0)
        {
            // 終わり！が出る
            finishText.text = ("おわり！");

            // タイマー0に見えるように
            timeText.text = ("00:00");

            FadeManager.Instance.TransScene("Result", 2f);

            timer = true;
        }
    }

}