using TMPro;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// �Q�[���J�n�O�u��`���v�u�X�^�[�g�I�v�̕�����\�����邽�߂̃N���X
/// </summary>
public class FadeText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI readyText;
    [SerializeField] TextMeshProUGUI startText;

    public static float fadeinDuration = 0.5f;     // FadeIn�ɂ����鎞��
    public static float fadeoutDuration = 0.5f;    // FadeOut�ɂ����鎞��

    [SerializeField] GameObject startCanvas;       // �J�E���g�_�E���Ɏg��canvas
    [SerializeField] CanvasGroup imageToFade;

    [SerializeField] AudioSource audioSource;      // SE�Đ��p��AudioSource
    [SerializeField] AudioClip startSE;            // �X�^�[�g�̍ۂɍĐ�����SE

    // �v���C���[�������邩�ǂ����������t���O
    public static bool canMove = false;

    void Start()
    {
        readyText.gameObject.SetActive(true);
        startText.gameObject.SetActive(true);
        imageToFade.gameObject.SetActive(true);

        // �����̓����x��0�ɂ��Ă���
        readyText.text = "��`��";
        readyText.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        startText.text = "�X�^�[�g�I";
        startText.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        // ready��FadeIn
        FadeInTx(readyText, fadeinDuration);
        // 0.5�b���Start��FadeIn
        DOVirtual.DelayedCall(0.5f, () => FadeInTx(startText, fadeinDuration, true));  // SE���Đ�����^�C�~���O��ǉ�
    }

    /// <summary>
    /// �����̃t�F�[�h�C�����Ǘ�����֐�
    /// </summary>
    /// <param name="tx">�t�F�[�h�C���������� Text</param>
    /// <param name="inDuration">�t�F�[�h�C�����鎞��</param>
    /// <param name="playSE">SE���Đ����邩�ǂ����̃t���O</param>
    public void FadeInTx(TextMeshProUGUI tx, float inDuration, bool playSE = false)
    {
        //  ����������FadeIn����
        tx.color = new Color(1, 1, 1, 0);
        tx.DOFade(1.0f, inDuration).OnComplete(() =>
        {
            // �t�F�[�h�C��������������SE���Đ�
            if (playSE) PlayStartSE();  // �X�^�[�g�̃t�F�[�h�C���������ɂ̂�SE�Đ�

            // start��ready�̕�����FadeOut
            DOVirtual.DelayedCall(2.0f, () => FadeOutTx(readyText, fadeoutDuration));
            DOVirtual.DelayedCall(2.0f, () => FadeOutTx(startText, fadeoutDuration));
            DOVirtual.DelayedCall(3.5f, () => imageToFade.DOFade(0.0f, fadeinDuration));

            canMove = true;  // �v���C���[�̓���������
        });
    }

    public void FadeOutTx(TextMeshProUGUI tx, float outDuration)
    {
        tx.color = new Color(1, 1, 1, 1);
        tx.DOFade(0.0f, outDuration);

        // startCanvas���\���ɂ���i�łȂ��ƃ^�C�}�[���n�܂�Ȃ��j
        DOVirtual.DelayedCall(2.0f, () => startCanvas.SetActive(false));
    }

    // �X�^�[�gSE���Đ����郁�\�b�h
    private void PlayStartSE()
    {
        audioSource.PlayOneShot(startSE);
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
