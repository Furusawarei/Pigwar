using UnityEngine;

public class TitleManager : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
        // タイトルシーンでの処理
        Scoremaneger.Instance().TitleStart();
    }
}
