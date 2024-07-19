using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// �^�C�}�[�Ǘ��p�̃N���X
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


        // ������
        Scoremaneger.Instance().SetScore(0, 1);
        Scoremaneger.Instance().SetScore(0, 2);
       
    }

    void Update()
    {
        // �^�C�}�[�̊J�n
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
            // �I�����b�Z�[�W��\��
            finishText.text = ("�I���I");

            // �^�C�}�[��0�ɐݒ�
            timeText.text = ("00:00");

            // �V�[���J��
            FadeManager.Instance.TransScene("MatukawaResult_Copy", 2.0f);
        }
    }
}
