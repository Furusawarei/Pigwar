using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainScene : MonoBehaviour
{
     private ActionControl _actionControl;
     private void Awake()
    {
        _actionControl = new ActionControl();
        _actionControl.Enable();
    }

    void Update()
    {
        if(_actionControl.UI.Scenes.triggered)
        {
            FadeManager.Instance.TransScene("MainScenes", 2.0f);
        }

        /*
        if (FadeManager.Instance.IsFading) return;
        // �Z�{�^���������ꂽ�烁�C����ʂɑJ��
        if (Input.GetKeyDown("joystick button 13"))
        {
            FadeManager.Instance.TransScene("Main", 2.0f);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            FadeManager.Instance.TransScene("Main", 2.0f);
        }
        */

    }

}
