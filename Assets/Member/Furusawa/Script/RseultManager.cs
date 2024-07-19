using UnityEngine;

public class ResultManager : MonoBehaviour
{
    void Start()
    {
        Scoremaneger.Instance().ToResult();//これでスコア表示をリザルト型に移動
    }
}
