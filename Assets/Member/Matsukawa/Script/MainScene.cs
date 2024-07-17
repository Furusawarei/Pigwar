using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainScene : MonoBehaviour
{
    private ActionControl _actionControl;
    public AudioSource audioSource; // SE再生用のAudioSource
    public AudioClip sceneChangeSE; // シーン遷移の際に再生するSE

    private void Awake()
    {
        _actionControl = new ActionControl();
        _actionControl.Enable();
    }

    void Update()
    {
        if (_actionControl.UI.Scenes.triggered)
        {
            PlaySE(sceneChangeSE); // SEを再生
            FadeManager.Instance.TransScene("MainScenes2", 2.0f);
        }
        
        /*
        if (FadeManager.Instance.IsFading) return;
        // セレクトボタンが押されたときにシーン遷移
        if (Input.GetKeyDown("joystick button 13"))
        {
            PlaySE(sceneChangeSE); // SEを再生
            FadeManager.Instance.TransScene("Main", 2.0f);
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            PlaySE(sceneChangeSE); // SEを再生
            FadeManager.Instance.TransScene("Main", 2.0f);
        }
        */
    }

    // SEを再生するメソッド
    private void PlaySE(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
