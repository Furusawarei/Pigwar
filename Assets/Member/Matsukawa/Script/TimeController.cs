using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class TimerController : MonoBehaviour
{
    public static float countdownMinutes = 0.1f;
    private float countdownSeconds;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI finishText;
    [SerializeField] private GameObject startImage;

    void Start()
    {
        countdownSeconds = countdownMinutes * 60;

        Scoremaneger.Instance().SetScore(0, 1);
        Scoremaneger.Instance().SetScore(0, 2);

        // •¶Žš‚Ì“§–¾“x‚ð0‚É‚µ‚Ä‚¨‚­
        finishText.text = "‚¨‚í‚è";
        finishText.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }
    void Update()
    {
        if (startImage.activeSelf) return;
        if (countdownSeconds > 0)
        {
            countdownSeconds -= Time.deltaTime;
            var span = new TimeSpan(0, 0, (int)countdownSeconds);
            timeText.text = span.ToString(@"mm\:ss");
        }

        if (FadeManager.Instance.IsFading) return;
        if (countdownSeconds < 0)
        {
            // ”O‚Ìˆ×ŽžŠÔ‚ð0‚É‚µ‚Ä‚¨‚­
            timeText.text = ("00:00");

            //  finishText‚ªFadeIn‚·‚é
            finishText.color = new Color(0, 0, 0, 0);
            finishText.DOFade(1.0f, (FadeText.fadeinDuration + 1.5f));

            // finish‚Ì•¶Žš‚ð•\Ž¦‚µ‚Ä‚©‚çA1.0•bŒã‚ÉƒŠƒUƒ‹ƒg‚Ö‘JˆÚ
            DOVirtual.DelayedCall(0.5f, () => FadeManager.Instance.TransScene("MatukawaResult_Copy", 2.0f));
           
        }
    }
}

