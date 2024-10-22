using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMmanager : MonoBehaviour
{
    #region
    private void Start()
    {
        #region 
        if (SceneManager.GetActiveScene().name == "MainTitle")
        { 
            // �^�C�g���V�[���݂̂�BGM���Đ�
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainTitle);
            Debug.Log("�^�C�g���V�[����BGM�Đ�");
        }
        //else if (SceneManager.GetActiveScene().name == "MainScenes")
        //{
        //    // MainScenes�V�[����BGM���Đ�
        //    SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainScenes);
        //}
        //else if (SceneManager.GetActiveScene().name == "Result")
        //{
        //    // Result�V�[����BGM���Đ�
        //    SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Result);
        //}
        #endregion

        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    private void OnActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        Debug.Log("�V�[�����ύX����܂���");
        if (SceneManager.GetActiveScene().name == "MainTitle")
        { 
            // �^�C�g���V�[����BGM���Đ�
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainTitle);
            Debug.Log("�^�C�g���V�[����BGM�Đ�");
        }
        else if (SceneManager.GetActiveScene().name == "MainScenes2")
        { 
            // MainScenes�V�[����BGM���Đ�
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainScenes);
            Debug.Log("���C���V�[����BGM�Đ�");
        }
        else if (SceneManager.GetActiveScene().name == "MatukawaResult_Copy")
        {
            // Result�V�[����BGM���Đ�
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Result);
            Debug.Log("���U���g�V�[����BGM�Đ�");
        }
    }
    #endregion
}
