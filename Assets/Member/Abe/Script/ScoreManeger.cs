﻿using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scoremaneger : MonoBehaviour
{
    //singletonエリア
    private static Scoremaneger instance;
    public static Scoremaneger Instance()//生成
    {
        if (instance == null)
            instance = new Scoremaneger();
        return instance;
    }
    private Scoremaneger() { }
    //変数
    public int[] PlayerScore = new int[2];


    [SerializeField, Header("スコア表示に使うtext(TMP)")] public TextMeshProUGUI[] _scoreborad = new TextMeshProUGUI[2];
    [SerializeField, Header("スコア表示に使うtext(TMP)")] public Transform[] _scoreboardTransform = new Transform[2];
    [SerializeField, Header("リザルトに遷移したときに移動する場所")] public Transform[] _resultPos = new Transform[2];

    private Vector3[] _defPos = new Vector3[2];
    private float[] _originalFontSize = new float[2];  // 元のフォントサイズを保持する配列

    private bool _scoreRandomSwitch = false;
    private bool _RenderSwitch = true;

    void Awake()
    {
        //初期化処理
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else//インスタンスが２個以上にならないようにする
        {
            Destroy(this.gameObject);
        }

    }

    void Start()
    {
        // 初期のフォントサイズを保存
        for (int i = 0; i < _scoreborad.Length; i++)
        {
            _originalFontSize[i] = _scoreborad[i].fontSize;
        }
    }

    private void Update()
    {
        if (_scoreRandomSwitch)
        {
            for (int i = 0; i < _scoreborad.Length; i++)
            {
                _scoreborad[i].text = Random.Range(10, 100).ToString();
            }
        }
    }

    public void SetScore(int Score, int PlayerNumber)
    {
        PlayerScore[PlayerNumber - 1] = Score;
        //最初の位置を記憶しておく
        _defPos[PlayerNumber - 1] = _scoreboardTransform[PlayerNumber - 1].position;
    }
    public int GetScore(int PlayerNumber)
    {
        return PlayerScore[PlayerNumber - 1];
    }


    /// <summary>
    /// スコア増減と反映(増減数,プレイヤー)
    /// </summary>
    /// <param name="score">増減させる値</param>
    /// <param name="PlayerNumber">増減させるプレイヤー</param>
    /// <returns>スコアを変更できたか</returns>
    public bool ScoreChenge(int score, int PlayerNumber)
    {
        PlayerNumber -= 1;
        if (PlayerScore[PlayerNumber] + score < 0)//計算後のスコアが0未満になるなら反映しない
        {
            Debug.Log("スコアがマイナスになった");
            return false;
        }
        PlayerScore[PlayerNumber] += score;
        _scoreborad[PlayerNumber].text = PlayerScore[PlayerNumber].ToString();
        return true;
    }

    /// <summary>
    /// リザルト画面遷移とスコア位置移動
    /// </summary>
    public void ToResult()
    {
        for (int i = 0; i < _scoreboardTransform.Length; i++)
        {
            _scoreboardTransform[i].position = _resultPos[i].position;
            _scoreborad[i].fontSize *= 1.5f;
        }
        ScoreRandomSwitch();
    }
    /// <summary>
    /// リザルト画面用に動かしたスコアの位置を戻す
    /// </summary>
    public void ToInGame()
    {
        for (int i = 0; i < _scoreboardTransform.Length; i++)
        {
            _scoreboardTransform[i].position = _defPos[i];
            _scoreborad[i].fontSize = _originalFontSize[i];  // 元のフォントサイズに戻す
        }
    }
    /// <summary>
    /// スコアの文字の表示非表示切り替え
    /// </summary>
    public void RenderSwitch()
    {
        _RenderSwitch = !_RenderSwitch;
        for (int i = 0; i < _scoreborad.Length; i++)
        {
            _scoreborad[i].enabled = _RenderSwitch;
        }
    }

    /// <summary>
    /// ランダムのやつのトグルスイッチ 
    /// </summary>
    public void ScoreRandomSwitch()
    {
        _scoreRandomSwitch = !_scoreRandomSwitch;
        _scoreborad[0].text = PlayerScore[0].ToString();
        _scoreborad[1].text = PlayerScore[1].ToString();
    }

    /// <summary>
    /// 勝敗判定
    /// </summary>
    /// <param name="winer">勝ったプレイヤー番号-1</param>
    /// <param name="loser">負けたプレイヤー番号-1</param>
    /// <returns>決着がついたか</returns>
    public bool Judge(out int winer, out int loser)
    {
        if (PlayerScore[0] > PlayerScore[1])
        {
            winer = 0;
            loser = 1;
            return true;
        }
        else if (PlayerScore[0] < PlayerScore[1])
        {
            winer = 1;
            loser = 0;
            return true;
        }
        else
        {
            winer = -1;
            loser = -1;
            return false;
        }
    }

    public void ToTitle()
    {
        RenderSwitch();
        SetScore(0, 1);
        SetScore(0, 2);
        ToInGame();
    }
}