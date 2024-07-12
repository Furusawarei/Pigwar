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
        //{ // Titleシーンでのみやりたい処理
        //    SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
        //    Debug.Log("切り替えタイトル");
        //}
        //else if (SceneManager.GetActiveScene().name == "MainScenes")
        //{ //MainScenesのシーンでやりたい処理
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
        Debug.Log("切り替え成功");
        //if (SceneManager.GetActiveScene().name == "CP_Title")
        if (SceneManager.GetActiveScene().name == "MainTitle")
        { // Titleシーンでのみやりたい処理
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
            Debug.Log("切り替えタイトル");
        }
        else if (SceneManager.GetActiveScene().name == "MainScenes2")
        { //MainScenesのシーンでやりたい処理
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainScenes);
            Debug.Log("切り替えゲーム");
        }

        //else if (SceneManager.GetActiveScene().name == "CP_Result")
        else if (SceneManager.GetActiveScene().name == "MainResult")
        {
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Result);
            Debug.Log("切り替えリザルト");
        }
    }
    #endregion
}

