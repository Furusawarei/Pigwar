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
    
    // SE�֘A�̕ϐ���ǉ�
    [SerializeField] private AudioSource countdownSE;
    private float lastSETime = 0.0f;

    void Start()
    {
        countdownSeconds = countdownMinutes * 60;
    }

    void Update()
    {
        // �^�C�}�[�̏���
        if (startImage.activeSelf) return;
        if (countdownSeconds > 0)
        {
            countdownSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)countdownSeconds);
            timeText.text = span.ToString(@"mm\:ss");

            // �c��5�b��؂�������SE����
            if (countdownSeconds <= 5 && countdownSeconds > 0)
            {
                if (Time.time - lastSETime >= 1.0f)
                {
                    countdownSE.Play();
                    lastSETime = Time.time;
                }
            }
        }

        if (FadeManager.Instance.IsFading) return;
        // �^�C�}�[��0�ɂȂ������̏���
        if (countdownSeconds < 0)
        {
            // �I���I���o��
            finishText.text = ("�����I");

            // �^�C�}�[0�Ɍ�����悤��
            timeText.text = ("00:00");

            FadeManager.Instance.TransScene("Result", 2.0f);
        }
    }
}
