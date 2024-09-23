using UnityEngine;

public class MainManager : MonoBehaviour
{
    void Start()
    {
        Scoremaneger.Instance().InGameStart();//これでスコア表示のオンオフ
    }
}
