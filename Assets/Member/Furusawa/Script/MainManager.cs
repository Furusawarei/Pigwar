using UnityEngine;

public class MainManager : MonoBehaviour
{
    void Start()
    {
           Scoremaneger.Instance().RenderSwitch();//これでスコア表示のオンオフ
    }
}
