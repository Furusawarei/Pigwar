using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;

/// <summary>
/// ゲーム開始前「よ～い」「スタート！」の文字を表示するためのクラス
/// </summary>
public class FadeText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ready;
    [SerializeField] TextMeshProUGUI start;

    public static float fadeinDuration = 0.5f;     // FadeInにかかる時間
    public static float fadeoutDuration = 0.5f;    // FadeOutにかかる時間

    // カウントダウンに使うcanvas
    [SerializeField] GameObject startCanvas;
    [SerializeField] CanvasGroup imageToFade;

    // SE再生用のAudioSource
    [SerializeField] AudioSource audioSource;
    // スタートの際に再生するSE
    [SerializeField] AudioClip startSE;

    // プレイヤーが動けるかどうかを示すフラグ
    public static bool canMove = false;

    void Start()
    {
        ready.gameObject.SetActive(true);
        start.gameObject.SetActive(true);

        // 文字の透明度を0にしておく
        ready.text = "よ～い";
        ready.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        start.text = "スタート！";
        start.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        // readyがFadeIn
        FadeInTx(ready, fadeinDuration);
        // 0.5秒後にStartがFadeIn
        DOVirtual.DelayedCall(0.5f, () => FadeInTx(start, fadeinDuration));

    }


    /// <summary>
    /// 文字のフェードインを管理する関数
    /// </summary>
    /// <param name="tx"><フェードインさせたい Text/param>
    /// <param name="inDuraton">< フェードインする時間FadeInDurarion（インスペクター上で設定）を使う/param>
    public void FadeInTx(TextMeshProUGUI tx, float inDuraton)
    {
        //  白い文字がFadeInする
        tx.color = new Color(1, 1, 1, 0);
        DOVirtual.DelayedCall(1, () => tx.DOFade(1.0f, inDuraton));

        PlayStartSE();          // スタートSEを再生
        canMove = true;         // プレイヤーの動きを許可

        // startとreadyの文字がFadeOut
        DOVirtual.DelayedCall(2.0f, () => FadeOutTx(ready, fadeoutDuration));
        DOVirtual.DelayedCall(2.0f, () => FadeOutTx(start, fadeoutDuration));
        DOVirtual.DelayedCall(3.5f, () => imageToFade.DOFade(0.0f, fadeinDuration));

    }

    public void FadeOutTx(TextMeshProUGUI tx, float  outDuraton)
    {
        tx.color = new Color(1, 1, 1, 1);
        DOVirtual.DelayedCall(1, () => tx.DOFade(0.0f, outDuraton));
    }

    // スタートSEを再生するメソッド
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