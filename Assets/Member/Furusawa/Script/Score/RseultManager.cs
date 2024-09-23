using UnityEngine;

public class ResultManager : MonoBehaviour
{
    void Start()
    {
        Scoremaneger.Instance().ResultStart();//これでスコア表示をリザルト型に移動
    }
}
