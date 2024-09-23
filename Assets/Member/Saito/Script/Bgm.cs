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
        { // Titleシーンに入った時の処理
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainTitle);
            Debug.Log("タイトルシーンに入りました");
        }
        //else if (SceneManager.GetActiveScene().name == "MainScenes")
        //{ // MainScenesシーンに入った時の処理
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
        Debug.Log("シーンが変更されました");
        //if (SceneManager.GetActiveScene().name == "CP_Title")
        if (SceneManager.GetActiveScene().name == "MainTitle")
        { // Titleシーンに入った時の処理
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainTitle);
            Debug.Log("タイトルシーンに入りました");
        }
        else if (SceneManager.GetActiveScene().name == "MainScenes2")
        { // MainScenesシーンに入った時の処理
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainScenes);
            Debug.Log("メインシーンに入りました");
        }

        //else if (SceneManager.GetActiveScene().name == "CP_Result")
        else if (SceneManager.GetActiveScene().name == "MatukawaResult_Copy")
        {
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Result);
            Debug.Log("結果シーンに入りました");
        }
    }
    #endregion
}
