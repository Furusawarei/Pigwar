using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;


/// <summary>
/// �V�[���J�ڎ��̃t�F�[�h�C���E�A�E�g�𐧌䂷�邽�߂̃N���X
/// </summary>
public class FadeManager : MonoBehaviour
{

    #region Singleton

    private static FadeManager instance;

    public static FadeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (FadeManager)FindObjectOfType(typeof(FadeManager));

                if (instance == null)
                {
                    Debug.LogError(typeof(FadeManager) + "is nothing");
                }
            }

            return instance;
        }
    }

    #endregion Singleton

    /// <summary>
    /// �f�o�b�O���[�h .
    /// </summary>
    //private bool DebugMode = false;
    /// <summary>�t�F�[�h���̓����x</summary>
    //private float fadeAlpha = 0;
    /// <summary>�t�F�[�h�����ǂ���</summary>
    private bool isFading = false;
    public bool IsFading => isFading;
    /// <summary>�t�F�[�h�F</summary>
    public Color fadeColor = Color.black;

    [SerializeField] CanvasGroup _canvasGroup;

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    //public void OnGUI()
    //{

    //    // Fade .
    //    if (this.isFading)
    //    {
    //        //�F�Ɠ����x���X�V���Ĕ��e�N�X�`����`�� .
    //        this.fadeColor.a = this.fadeAlpha;
    //        GUI.color = this.fadeColor;
    //        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
    //    }

    //    if (this.DebugMode)
    //    {
    //        if (!this.isFading)
    //        {
    //            //Scene�ꗗ���쐬 .
    //            //(UnityEditor���O��Ԃ��g��Ȃ��Ǝ����擾�ł��Ȃ�) .
    //            List<string> scenes = new List<string>();
    //            scenes.Add("Title");
    //            scenes.Add("Main");
    //            scenes.Add("Result");


    //            //Scene������Ȃ� .
    //            if (scenes.Count == 0)
    //            {
    //                GUI.Box(new Rect(10, 10, 200, 50), "Fade Manager(Debug Mode)");
    //                GUI.Label(new Rect(20, 35, 180, 20), "Scene not found.");
    //                return;
    //            }


    //            GUI.Box(new Rect(10, 10, 300, 50 + scenes.Count * 25), "Fade Manager(Debug Mode)");
    //            GUI.Label(new Rect(20, 30, 280, 20), "Current Scene : " + SceneManager.GetActiveScene().name);

    //            int i = 0;
    //            foreach (string sceneName in scenes)
    //            {
    //                if (GUI.Button(new Rect(20, 55 + i * 25, 100, 20), "Load Level"))
    //                {
    //                    TransScene(sceneName, 1.0f);
    //                }
    //                GUI.Label(new Rect(125, 55 + i * 25, 1000, 20), sceneName);
    //                i++;
    //            }
    //        }
    //    }



    //}

    /// </summary>
    /// <param name='scene'>�V�[����</param>
    /// <param name='interval'>�Ó]�ɂ����鎞��(�b)</param>
    public async void TransScene(string scene, float interval)
    {
        await FadeIn(scene, interval);

        //�V�[���ؑ�
        await SceneManager.LoadSceneAsync(scene);

        await FadeOut(scene, interval);

    }

    public async UniTask FadeIn(string scene, float interval)
    {
        //���񂾂�Â� .
        this.isFading = true;
        float time = 0;
        //while (time <= interval)
        //{
        //    this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
        //    time += Time.deltaTime;
        //    await UniTask.Yield();
        //}
        _canvasGroup.gameObject.SetActive(true);
        _canvasGroup.alpha = 0;
        while (time < 1.0f)
        {
            time += Time.deltaTime / interval;
            time = Mathf.Clamp01(time);
            _canvasGroup.alpha = time;
            await UniTask.Yield();
        }

        this.isFading= false;
    }


    public async UniTask FadeOut(string scene, float interval)
    {
        this.isFading = true;

        //���񂾂񖾂邭 .
        float time = 0;
        //while (time <= interval)
        //{
        //    time += Time.deltaTime;
        //    this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
        //    await UniTask.Yield();
        //}
        _canvasGroup.alpha = 1;
        while (time < 1.0f)
        {
            time += Time.deltaTime / interval;
            time = Mathf.Clamp01(time);
            _canvasGroup.alpha = 1 - time;
            await UniTask.Yield();
        }

        this.isFading = false;
        _canvasGroup.gameObject.SetActive(false);
    }
}

