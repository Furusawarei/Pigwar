using UnityEngine;

public class TitleManager : MonoBehaviour
{
    void Start()
    {
        // Scoremaneger のインスタンスを取得
        Scoremaneger scoreManager = Scoremaneger.Instance();

        // タイトルシーンでの処理
        scoreManager.ToTitle();
    }
}
