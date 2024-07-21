using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResultScene : MonoBehaviour
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
           FadeManager.Instance.TransScene("MAinTitle", 2.0f);
           Scoremaneger.Instance().ToInGame();
        }

        /*
        // ?Z?{?^?????????????C??????J??
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
