using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ?^?C?}?[????p??N???X
/// </summary>
public class TimerController : MonoBehaviour
{
    public float countdownMinutes;
    private float countdownSeconds;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI finishText;

    [SerializeField] private GameObject startImage;

    private bool isTimerFinished = false;

    void Start()
    {
        countdownSeconds = countdownMinutes * 60;

        Scoremaneger.Instance().SetScore(0, 1);
        Scoremaneger.Instance().SetScore(0, 2);
    }

    void Update()
    {
        if (startImage.activeSelf) return;
        if (countdownSeconds > 0)
        {
            countdownSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)countdownSeconds);
            timeText.text = span.ToString(@"mm\:ss");
        }

        if (FadeManager.Instance.IsFading) return;
        if (countdownSeconds < 0 && !isTimerFinished)
        {
            isTimerFinished = true;
            finishText.text = ("Finish!");

            timeText.text = ("00:00");

            FadeManager.Instance.TransScene("MatukawaResult_Copy", 2.0f);
        }
    }
}

