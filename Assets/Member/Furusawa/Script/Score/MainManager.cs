using UnityEngine;

public class MainManager : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
        Scoremaneger.Instance().InGameStart();//これでスコア表示のオンオフ
    }
}
