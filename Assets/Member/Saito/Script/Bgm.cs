using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bgm : MonoBehaviour
{
    #region
    private void Start()
    {
        #region 
        //if (SceneManager.GetActiveScene().name == "Title")
        //{ // Title�V�[���ł݂̂�肽������
        //    SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
        //    Debug.Log("�؂�ւ��^�C�g��");
        //}
        //else if (SceneManager.GetActiveScene().name == "MainScenes")
        //{ //MainScenes�̃V�[���ł�肽������
        //    SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainScenes);
        //}
        //else if (SceneManager.GetActiveScene().name == "Result")
        //{
        //    SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Result);
        //}
        #endregion

        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }
    private void OnActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        Debug.Log("�؂�ւ�����");
        //if (SceneManager.GetActiveScene().name == "CP_Title")
        if (SceneManager.GetActiveScene().name == "MainTitle")
        { // Title�V�[���ł݂̂�肽������
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
            Debug.Log("�؂�ւ��^�C�g��");
        }
        else if (SceneManager.GetActiveScene().name == "MainScenes2")
        { //MainScenes�̃V�[���ł�肽������
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainScenes);
            Debug.Log("�؂�ւ��Q�[��");
        }

        //else if (SceneManager.GetActiveScene().name == "CP_Result")
        else if (SceneManager.GetActiveScene().name == "MainResult")
        {
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Result);
            Debug.Log("�؂�ւ����U���g");
        }
    }
    #endregion
}

