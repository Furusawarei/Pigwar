using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Unity.VisualScripting;

/// <summary>
/// Time manager
/// </summary>
public class TimerController : MonoBehaviour
{
    public static float countdownMinutes = 0.5f;
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

            //  finishText‚ªFadeIn‚·‚é
            finishText.color = new Color(1, 1, 1, 0);
            finishText.DOFade(1.0f, FadeText.fadeinDuration);
            timeText.text = ("00:00");

            DOVirtual.DelayedCall(2.0f, () => FadeManager.Instance.TransScene("MatukawaResult_Copy", 2.0f));


           
        }
    }
}

