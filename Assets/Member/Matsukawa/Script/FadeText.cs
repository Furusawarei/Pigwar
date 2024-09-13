using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// �Q�[���J�n�O�u��`���v�u�X�^�[�g�I�v�̕�����\�����邽�߂̃N���X
/// </summary>
public class FadeText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ready;
    [SerializeField] TextMeshProUGUI start;

    public static float fadeinDuration = 0.5f;     // FadeIn�ɂ����鎞��
    public static float fadeoutDuration = 0.5f;    // FadeOut�ɂ����鎞��

    // �J�E���g�_�E���Ɏg��canvas
    [SerializeField] GameObject startCanvas;
    [SerializeField] CanvasGroup imageToFade;

    // SE�Đ��p��AudioSource
    [SerializeField] AudioSource audioSource;
    // �X�^�[�g�̍ۂɍĐ�����SE
    [SerializeField] AudioClip startSE;

    // �v���C���[�������邩�ǂ����������t���O
    public static bool canMove = false;

    void Start()
    {
        ready.gameObject.SetActive(true);
        start.gameObject.SetActive(true);

        // �����̓����x��0�ɂ��Ă���
        ready.text = "��`��";
        ready.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        start.text = "�X�^�[�g�I";
        start.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        // ready��FadeIn
        FadeInTx(ready, fadeinDuration);
        // 0.5�b���Start��FadeIn
        DOVirtual.DelayedCall(0.5f, () => FadeInTx(start, fadeinDuration));

    }


    /// <summary>
    /// �����̃t�F�[�h�C�����Ǘ�����֐�
    /// </summary>
    /// <param name="tx"><�t�F�[�h�C���������� Text/param>
    /// <param name="inDuraton">< �t�F�[�h�C�����鎞��FadeInDurarion�i�C���X�y�N�^�[��Őݒ�j���g��/param>
    public void FadeInTx(TextMeshProUGUI tx, float inDuraton)
    {
        //  ����������FadeIn����
        tx.color = new Color(1, 1, 1, 0);
        DOVirtual.DelayedCall(1, () => tx.DOFade(1.0f, inDuraton));

        PlayStartSE();          // �X�^�[�gSE���Đ�
        canMove = true;         // �v���C���[�̓���������

        // start��ready�̕�����FadeOut
        DOVirtual.DelayedCall(2.0f, () => FadeOutTx(ready, fadeoutDuration));
        DOVirtual.DelayedCall(2.0f, () => FadeOutTx(start, fadeoutDuration));
        DOVirtual.DelayedCall(3.5f, () => imageToFade.DOFade(0.0f, fadeinDuration));

    }

    public void FadeOutTx(TextMeshProUGUI tx, float  outDuraton)
    {
        tx.color = new Color(1, 1, 1, 1);
        DOVirtual.DelayedCall(1, () => tx.DOFade(0.0f, outDuraton));
    }

    // �X�^�[�gSE���Đ����郁�\�b�h
    private void PlayStartSE()
    {
        if (audioSource != null && startSE != null)
        {
            audioSource.PlayOneShot(startSE);
        }
    }


    //IEnumerator FadeInR()
    //{
    //    yield return new WaitForSeconds(0.05f);

    //    ready.text = ("��`��");
    //    while (true)
    //    {
    //        for (int i = 0; i < 255; i++)
    //        {
    //            ready.color = ready.color + new Color32(1, 1, 1, 1);
    //            yield return new WaitForSeconds(0.0005f);
    //        }
    //        break;
    //    }
    //}


    //IEnumerator FadeInS()
    //{
    //    yield return new WaitForSeconds(1.5f);

    //    start.text = ("�X�^�[�g�I");
    //    while (true)
    //    {
    //        for (int i = 0; i < 255; i++)
    //        {
    //            start.color = start.color + new Color32(1, 1, 1, 1);
    //            yield return new WaitForSeconds(0.005f);
    //        }
    //        break;
    //    }


    //StartCoroutine("FadeOut");

    //}

    //IEnumerator FadeOut()
    //{
    //    while (true)
    //    {
    //        for (int i = 255; i > 0; i--)
    //        {
    //            imageToFade.color = imageToFade.color + new Color32(0, 0, 0, 0);
    //            ready.color = ready.color + new Color32(0, 0, 0, 0);
    //            start.color = start.color + new Color32(0, 0, 0, 0);

    //            yield return new WaitForSeconds(0f);
    //        }
    //        break;
    //    }

    //    startCanvas.SetActive(false);
    //}

}