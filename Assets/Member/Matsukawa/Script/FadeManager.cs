using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

/// <summary>
/// シーン間移動のフェードイン・アウトを管理するクラス
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
    /// デバッグモード .
    /// </summary>
    //private bool DebugMode = false;
    /// <summary>フェード中の透明度</summary>
    //private float fadeAlpha = 0;
    /// <summary>フェード中かどうか</summary>
    private bool isFading = false;
    public bool IsFading => isFading;
    /// <summary>フェード色</summary>
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

    //    // フェード .
    //    if (this.isFading)
    //    {
    //        //色と透明度を更新して描画 .
    //        this.fadeColor.a = this.fadeAlpha;
    //        GUI.color = this.fadeColor;
    //        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
    //    }

    //    if (this.DebugMode)
    //    {
    //        if (!this.isFading)
    //        {
    //            //Sceneリストを作成 .
    //            //(UnityEditorのみ使用可能な関数) .
    //            List<string> scenes = new List<string>();
    //            scenes.Add("Title");
    //            scenes.Add("Main");
    //            scenes.Add("Result");


    //            //Sceneが見つからない場合 .
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

    /// <summary>
    /// シーン遷移
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>遷移にかける時間(秒)</param>
    public async void TransScene(string scene, float interval)
    {
        await FadeIn(scene, interval);

        //シーン遷移
        await SceneManager.LoadSceneAsync(scene);

        await FadeOut(scene, interval);

    }

    public async UniTask FadeIn(string scene, float interval)
    {
        //フェードインの開始 .
        this.isFading = true;
        float time = 0;

        if (_canvasGroup != null)
        {
            _canvasGroup.gameObject.SetActive(true);
            _canvasGroup.alpha = 0;
            while (time < 1.0f)
            {
                time += Time.deltaTime / interval;
                time = Mathf.Clamp01(time);
                _canvasGroup.alpha = time;
                await UniTask.Yield();
            }
        }

        this.isFading = false;
    }

    public async UniTask FadeOut(string scene, float interval)
    {
        this.isFading = true;

        //フェードアウトの開始 .
        float time = 0;

        if (_canvasGroup != null)
        {
            _canvasGroup.alpha = 1;
            while (time < 1.0f)
            {
                time += Time.deltaTime / interval;
                time = Mathf.Clamp01(time);
                _canvasGroup.alpha = 1 - time;
                await UniTask.Yield();
            }

            _canvasGroup.gameObject.SetActive(false);
        }

        this.isFading = false;
    }
}
