using TMPro;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// ゲーム開始前「よ～い」「スタート！」の文字を表示するためのクラス
/// </summary>
public class FadeText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI readyText;
    [SerializeField] TextMeshProUGUI startText;

    public static float fadeinDuration = 0.5f;     // FadeInにかかる時間
    public static float fadeoutDuration = 0.5f;    // FadeOutにかかる時間

    [SerializeField] GameObject startCanvas;       // カウントダウンに使うcanvas
    [SerializeField] CanvasGroup imageToFade;

    [SerializeField] AudioSource audioSource;      // SE再生用のAudioSource
    [SerializeField] AudioClip startSE;            // スタートの際に再生するSE

    // プレイヤーが動けるかどうかを示すフラグ
    public static bool canMove = false;

    void Start()
    {
        readyText.gameObject.SetActive(true);
        startText.gameObject.SetActive(true);
        imageToFade.gameObject.SetActive(true);

        // 文字の透明度を0にしておく
        readyText.text = "よ～い";
        readyText.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        startText.text = "スタート！";
        startText.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        // readyがFadeIn
        FadeInTx(readyText, fadeinDuration);
        // 0.5秒後にStartがFadeIn
        DOVirtual.DelayedCall(0.5f, () => FadeInTx(startText, fadeinDuration, true));  // SEを再生するタイミングを追加
    }

    /// <summary>
    /// 文字のフェードインを管理する関数
    /// </summary>
    /// <param name="tx">フェードインさせたい Text</param>
    /// <param name="inDuration">フェードインする時間</param>
    /// <param name="playSE">SEを再生するかどうかのフラグ</param>
    public void FadeInTx(TextMeshProUGUI tx, float inDuration, bool playSE = false)
    {
        //  白い文字がFadeInする
        tx.color = new Color(1, 1, 1, 0);
        tx.DOFade(1.0f, inDuration).OnComplete(() =>
        {
            // フェードインが完了したらSEを再生
            if (playSE) PlayStartSE();  // スタートのフェードイン完了時にのみSE再生

            // startとreadyの文字がFadeOut
            DOVirtual.DelayedCall(2.0f, () => FadeOutTx(readyText, fadeoutDuration));
            DOVirtual.DelayedCall(2.0f, () => FadeOutTx(startText, fadeoutDuration));
            DOVirtual.DelayedCall(3.5f, () => imageToFade.DOFade(0.0f, fadeinDuration));

            canMove = true;  // プレイヤーの動きを許可
        });
    }

    public void FadeOutTx(TextMeshProUGUI tx, float outDuration)
    {
        tx.color = new Color(1, 1, 1, 1);
        tx.DOFade(0.0f, outDuration);

        // startCanvasを非表示にする（でないとタイマーが始まらない）
        DOVirtual.DelayedCall(2.0f, () => startCanvas.SetActive(false));
    }

    // スタートSEを再生するメソッド
    private void PlayStartSE()
    {
        audioSource.PlayOneShot(startSE);
    }

    //IEnumerator FadeInR()
    //{
    //    yield return new WaitForSeconds(0.05f);

    //    ready.text = ("よ～い");
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

    //    start.text = ("スタート！");
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
