using UnityEngine;

public class TitleManager : MonoBehaviour
{
    void Start()
    {
        // タイトルシーンでの処理
        Scoremaneger.Instance().ToTitle();
    
    }
}
