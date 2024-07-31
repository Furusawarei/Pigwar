using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResultScene : MonoBehaviour
{

    private ActionControl _actionControl;
    private bool isScneChanging = false;

    public AudioSource audioSource; // SE再生用のAudioSource
    public AudioClip sceneChangeSE; // シーン遷移の際に再生するSE

    private void Awake()
    {
        _actionControl = new ActionControl();
        _actionControl.Enable();
    }
    void Update()
    {
        if (_actionControl.UI.Scenes.triggered && !isScneChanging)
        {
            StartCoroutine(SceneChange());
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

    private IEnumerator SceneChange()
    {
        isScneChanging = true;
        PlaySE(sceneChangeSE); // SEを再生
        FadeManager.Instance.TransScene("MainTitle", 2.0f);
        Scoremaneger.Instance().ToInGame();
        Scoremaneger.Instance().ToTitle();

        yield return new WaitForSeconds(2.0f);
        isScneChanging = false;
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
