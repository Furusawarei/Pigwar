using TMPro;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// テキストのフェードイン・フェードアウトを管理するクラス
/// </summary>
public class FadeText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI readyText;
    [SerializeField] TextMeshProUGUI startText;

    public static float fadeinDuration = 0.7f;     // フェードインの時間
    public static float fadeoutDuration = 0.7f;    // フェードアウトの時間

    [SerializeField] GameObject startCanvas;       // スタート画面のキャンバス
    [SerializeField] CanvasGroup imageToFade;

    [SerializeField] AudioSource audioSource;      // SEを再生するAudioSource
    [SerializeField] AudioClip startSE;            // スタート時のSE

    // ゲーム開始時の移動制御フラグ
    public static bool canMove = false;

    void Start()
    {
        readyText.gameObject.SetActive(true);
        startText.gameObject.SetActive(true);
        imageToFade.gameObject.SetActive(true);

        // 初期テキストの設定
        readyText.text = "よ～い";
        readyText.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        startText.text = "スタート！";
        startText.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);

        // readyのフェードイン
        FadeInTx(readyText, fadeinDuration);
        // 0.5秒後にstartのフェードイン
        DOVirtual.DelayedCall(0.5f, () => FadeInTx(startText, fadeinDuration, true));  // SE再生後、スタートキャンバスを非表示に
    }

    /// <summary>
    /// テキストのフェードイン処理
    /// </summary>
    /// <param name="tx">フェードインするText</param>
    /// <param name="inDuration">フェードインの時間</param>
    /// <param name="playSE">SE再生の有無</param>
    public void FadeInTx(TextMeshProUGUI tx, float inDuration, bool playSE = false)
    {
        // 初期化
        tx.color = new Color(1, 1, 1, 0);
        tx.DOFade(1.0f, inDuration).OnComplete(() =>
        {
            // テキストの表示後にSE再生
            if (playSE)
            {
                PlayStartSE();
                // SE再生後にスタートキャンバスを非表示に
                DOVirtual.DelayedCall(1.5f, () => startCanvas.SetActive(false));
            }

            // readyとstartのフェードアウト処理
            DOVirtual.DelayedCall(5.0f, () => FadeOutTx(readyText, fadeoutDuration));
            DOVirtual.DelayedCall(5.0f, () => FadeOutTx(startText, fadeoutDuration));
            DOVirtual.DelayedCall(5.5f, () => imageToFade.DOFade(0.0f, fadeinDuration));

            canMove = true;  // 移動可能フラグを有効に
        });
    }

    public void FadeOutTx(TextMeshProUGUI tx, float outDuration)
    {
        tx.color = new Color(1, 1, 1, 1);
        tx.DOFade(0.0f, outDuration);

        // スタートキャンバスを非表示に
        DOVirtual.DelayedCall(2.0f, () => startCanvas.SetActive(false));
    }

    // スタート時のSEを再生
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
    //    StartCoroutine("FadeOut");
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
