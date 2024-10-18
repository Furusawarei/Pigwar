using UnityEngine;

public class ResultManager : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
        Scoremaneger.Instance().ResultStart();//これでスコア表示をリザルト型に移動
    }
}
