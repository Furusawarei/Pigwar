using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

/// <summary>
/// �Q�[�����n�߂�O�́u��`���v�A�u�X�^�[�g�I�v��\�����邽�߂̃N���X
/// </summary>
public class FadeText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ready;
    [SerializeField] TextMeshProUGUI start;

    // �J�E���g�_�E���Ɏg��canvas
    [SerializeField] GameObject startCanvas;
    [SerializeField] Image imageToFade;

    // SE�Đ��p��AudioSource
    [SerializeField] AudioSource audioSource;
    // �X�^�[�g�̍ۂɍĐ�����SE
    [SerializeField] AudioClip startSE;

    // �v���C���[�������邩�ǂ����������t���O
    public static bool canMove = false;

    void Start()
    {
        ready.text = "ready";
        ready.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        start.text = "start";
        start.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        StartCoroutine("FadeInR");
        StartCoroutine("FadeInS");
    }

    IEnumerator FadeInR()
    {
        yield return new WaitForSeconds(0.5f);

        ready.text = ("��`��");
        while (true)
        {
            for (int i = 0; i < 255; i++)
            {
                ready.color = ready.color + new Color32(1, 1, 1, 1);
                yield return new WaitForSeconds(0.005f);
            }
            break;
        }
    }

    IEnumerator FadeInS()
    {
        yield return new WaitForSeconds(1.5f);

        start.text = ("�X�^�[�g�I");
        while (true)
        {
            for (int i = 0; i < 255; i++)
            {
                start.color = start.color + new Color32(1, 1, 1, 1);
                yield return new WaitForSeconds(0.005f);
            }
            break;
        }

        PlayStartSE(); // �X�^�[�gSE���Đ�

        // �v���C���[�̓���������
        canMove = true;

        StartCoroutine("FadeOut");

    }

    IEnumerator FadeOut()
    {
        while (true)
        {
            for (int i = 255; i > 0; i--)
            {
                imageToFade.color = imageToFade.color + new Color32(0, 0, 0, 0);
                ready.color = ready.color + new Color32(0, 0, 0, 0);
                start.color = start.color + new Color32(0, 0, 0, 0);

                yield return new WaitForSeconds(0.005f);
            }
            break;
        }

        startCanvas.SetActive(false);
    }

    // �X�^�[�gSE���Đ����郁�\�b�h
    private void PlayStartSE()
    {
        if (audioSource != null && startSE != null)
        {
            audioSource.PlayOneShot(startSE);
        }
    }
}