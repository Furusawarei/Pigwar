using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bgm : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainTitle")
        { // Title�V�[���ł݂̂�肽������
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Title);
        }
        else if (SceneManager.GetActiveScene().name == "MainScenes")
        { //MainScenes�̃V�[���ł�肽������
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.MainScenes);
        }
        else if (SceneManager.GetActiveScene().name == "MainResult")
        {
            SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Result);
        }
    }
}
