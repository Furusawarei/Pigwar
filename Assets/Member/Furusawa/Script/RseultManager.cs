using UnityEngine;

public class ResultManager : MonoBehaviour
{
    void Start()
    {
        // Scoremaneger のインスタンスを取得
        Scoremaneger scoreManager = Scoremaneger.Instance();

       // スコアをリザルト型に移動
            Scoremaneger.Instance().ToResult();

    }
}
