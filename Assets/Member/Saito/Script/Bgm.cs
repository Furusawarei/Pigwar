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
        if (SceneManager.GetActiveScene().name == "MainTitle")
        { // Title�V�[���ł݂̂�肽������
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainTitle);
            Debug.Log("�؂�ւ��^�C�g��");
        }
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
<<<<<<< HEAD
        Debug.Log("�V�[���ړ��m�F");
        if (SceneManager.GetActiveScene().name == "Title")
        { // Title�V�[���ł݂̂�肽������
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
            Debug.Log("�V�[���^�C�g��");
=======
        Debug.Log("�؂�ւ�����");
        //if (SceneManager.GetActiveScene().name == "CP_Title")
        if (SceneManager.GetActiveScene().name == "MainTitle")
        { // Title�V�[���ł݂̂�肽������
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainTitle);
            Debug.Log("�؂�ւ��^�C�g��");
>>>>>>> e2c8d18453f0d3e59b9befb953a07ce28515cb62
        }
        else if (SceneManager.GetActiveScene().name == "MainScenes2")
        { //MainScenes�̃V�[���ł�肽������
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainScenes);
            Debug.Log("�V�[���Q�[��");
        }

        //else if (SceneManager.GetActiveScene().name == "CP_Result")
        else if (SceneManager.GetActiveScene().name == "MatukawaResult_Copy")
        {
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Result);
            Debug.Log("�V�[�����U���g");
        }
    }
    #endregion
}

