using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bgm : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        { // Titleシーンでのみやりたい処理
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
        }
        else if (SceneManager.GetActiveScene().name == "MainScenes")
        { //MainScenesのシーンでやりたい処理
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainScenes);
        }
        else if (SceneManager.GetActiveScene().name == "Result")
        {
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Result);
        }
    }
}
