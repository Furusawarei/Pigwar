using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using Unity.VisualScripting;
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

    //�X�R�A�p�e�L�X�g
    [SerializeField] private List<TextMeshProUGUI> _scoreborad;

    void Start()
    {
        countdownSeconds = countdownMinutes * 60;

        //������
        Scoremaneger.Instance().SetScore(0, 1);
        Scoremaneger.Instance().SetScore(0, 2);
        //0��\������
        _scoreborad[0].text = Scoremaneger.Instance().PlayerScore[0].ToString();
        _scoreborad[1].text = Scoremaneger.Instance().PlayerScore[1].ToString();
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
            Debug.Log("�^�C�}�[��0�ɂȂ�܂���");

            // �I�����b�Z�[�W��\��
            finishText.text = ("�I���I");

            // �^�C�}�[��0�ɐݒ�
            timeText.text = ("00:00");

            // �X�R�A�����U���g�^�Ɉړ�
            Scoremaneger.Instance().ToResult();

            // �V�[���J��
            FadeManager.Instance.TransScene("MatukawaResult_Copy", 2.0f);
        }
    }

}
