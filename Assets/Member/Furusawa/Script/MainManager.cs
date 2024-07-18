using UnityEngine;

public class MainManager : MonoBehaviour
{
    void Start()
    {
        // Scoremaneger のインスタンスを取得
        Scoremaneger scoreManager = Scoremaneger.Instance();

        
        scoreManager.RenderSwitch();

    }
}
