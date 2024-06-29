using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

/// <summary>
/// ゲームが始める前の「よ～い」、「スタート！」を表示するためのクラス
/// </summary>
public class FadeText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ready;
    [SerializeField] TextMeshProUGUI start;

    // カウントダウンに使うcanvas
    [SerializeField] GameObject startCanvas;
    [SerializeField] Image imageToFade;

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

        ready.text = ("よ～い");
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
        yield return new WaitForSeconds(2f);

        start.text = ("スタート！");
        while (true)
        {
            for (int i = 0; i < 255; i++)
            {
                start.color = start.color + new Color32(1, 1, 1, 1);
                yield return new WaitForSeconds(0.005f);
            }
            break;
        }

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
}
