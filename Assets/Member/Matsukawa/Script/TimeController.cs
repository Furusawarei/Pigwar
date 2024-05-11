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

    // �^�C�}�[���t���Ă��邩�ǂ���
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
    //    Debug.Log("�R���[�`���͂���");

    //    Timer();
    //}

    void Update()
    {
        //Debug.Log("Timer�̏����͂���");

        //  �^�C�}�[�̏���
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


        // �^�C�}�[��0�ɂȂ������̏���
        if(countdownSeconds <= 0)
        {
            // �I���I���o��
            finishText.text = ("�����I");

            // �^�C�}�[0�Ɍ�����悤��
            timeText.text = ("00:00");

            FadeManager.Instance.TransScene("Result", 2f);

            timer = true;
        }
    }

}