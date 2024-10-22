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
            // タイトルシーンのみでBGMを再生
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainTitle);
            Debug.Log("タイトルシーンでBGM再生");
        }
        //else if (SceneManager.GetActiveScene().name == "MainScenes")
        //{
        //    // MainScenesシーンでBGMを再生
        //    SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainScenes);
        //}
        //else if (SceneManager.GetActiveScene().name == "Result")
        //{
        //    // ResultシーンでBGMを再生
        //    SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Result);
        //}
        #endregion

        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    private void OnActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        Debug.Log("シーンが変更されました");
        if (SceneManager.GetActiveScene().name == "MainTitle")
        { 
            // タイトルシーンでBGMを再生
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainTitle);
            Debug.Log("タイトルシーンでBGM再生");
        }
        else if (SceneManager.GetActiveScene().name == "MainScenes2")
        { 
            // MainScenesシーンでBGMを再生
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainScenes);
            Debug.Log("メインシーンでBGM再生");
        }
        else if (SceneManager.GetActiveScene().name == "MatukawaResult_Copy")
        {
            // ResultシーンでBGMを再生
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Result);
            Debug.Log("リザルトシーンでBGM再生");
        }
    }
    #endregion
}
