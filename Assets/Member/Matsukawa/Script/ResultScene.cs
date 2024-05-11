using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResultScene : MonoBehaviour
{
    void Update()
    {

        // Zƒ{ƒ^ƒ“‚ª‚¨‚³‚ê‚½‚çƒƒCƒ“‰æ–Ê‚É‘JˆÚ
        if (Input.GetKeyDown("joystick button 13"))
        {
            FadeManager.Instance.TransScene("Title", 2.0f);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            FadeManager.Instance.TransScene("Title", 2.0f);
        }

    }

}
