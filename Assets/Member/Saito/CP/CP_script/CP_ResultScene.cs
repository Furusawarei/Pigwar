using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CP_ResultScene : MonoBehaviour
{
     private ActionControl _actionControl;
     private void Awake()
    {
        _actionControl = new ActionControl();
        _actionControl.Enable();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("押した");
            FadeManager.Instance.TransScene("CP_Title", 2.0f);
        }
        /*
        // �Z�{�^���������ꂽ�烁�C����ʂɑJ��
        if (Input.GetKeyDown("joystick button 13"))
        {
            FadeManager.Instance.TransScene("Title", 2.0f);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            FadeManager.Instance.TransScene("Title", 2.0f);
        }
        */

    }

}
