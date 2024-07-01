using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;

/// <summary>
/// ���ԊǗ��p�̃N���X
/// </summary>
public class TimerController : MonoBehaviour
{
    // �^�C�}�[
    public float countdownMinutes;
    private float countdownSeconds;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI finishText;

    [SerializeField] private GameObject startImage;
    //[SerializeField] private GameObject resultButton;

    void Start()
    {
        countdownSeconds = countdownMinutes * 60;
    }


    void Update()
    {
        //  �^�C�}�[�̏���
        if (startImage.activeSelf) return;
        if (countdownSeconds > 0)
        {
            countdownSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)countdownSeconds);
            timeText.text = span.ToString(@"mm\:ss");
        }

        if (FadeManager.Instance.IsFading) return;
        // �^�C�}�[��0�ɂȂ������̏���
        if (countdownSeconds < 0)
        {
            // �I���I���o��
            finishText.text = ("�����I");

            // �^�C�}�[0�Ɍ�����悤��
            timeText.text = ("00:00");

            FadeManager.Instance.TransScene("MatukawaResult_Copy", 2.0f);
        }
    }

}