using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainScene : MonoBehaviour
{
    void Update()
    {
        if (FadeManager.Instance.IsFading) return;
        // 〇ボタンがおされたらメイン画面に遷移
        if (Input.GetKeyDown("joystick button 13"))
        {
            FadeManager.Instance.TransScene("Main", 2.0f);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            FadeManager.Instance.TransScene("Main", 2.0f);
        }

    }

}
