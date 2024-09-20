﻿using TMPro;
using UnityEngine;

public class Scoremaneger : MonoBehaviour
{
    // Singletonエリア
    private static Scoremaneger instance;
    public static Scoremaneger Instance()
    {
        if (instance == null)
            instance = new Scoremaneger();
        return instance;
    }
    private Scoremaneger() { }

    // 変数
    public int[] PlayerScore = new int[2];

    [SerializeField, Header("スコア表示に使うtext(TMP)")] public TextMeshProUGUI[] _scoreborad = new TextMeshProUGUI[2];
    [SerializeField, Header("スコア表示に使うtext(TMP)")] public Transform[] _scoreboardTransform = new Transform[2];
    [SerializeField, Header("リザルトに遷移したときに移動する場所")] public Transform[] _resultPos = new Transform[2];

    private Vector3[] _defPos = new Vector3[2];
    [SerializeField, Header("元のフォントサイズ")] private float _defFontSize = 180;
    [SerializeField,Header("リザルト時のフォントサイズ")] private float _resultFontSize = 220;

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
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        // 初期化コードがあればここに記述
        for (int i = 0; i < _scoreboardTransform.Length; i++)
        {
            _defPos[i] = _scoreboardTransform[i].position;  // 最初の位置を記憶しておく
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
        _scoreborad[PlayerNumber - 1].text = Score.ToString();  // スコア表示を更新
        
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
        if (PlayerScore[PlayerNumber] + score < 0)
        {
            Debug.Log("スコアがマイナスになった");
            return false;
        }
        PlayerScore[PlayerNumber] += score;
        _scoreborad[PlayerNumber].text = PlayerScore[PlayerNumber].ToString();
        return true;
    }

    /// <summary>
    /// スコア表示位置をリザルトに移動
    /// </summary>
    public void ToResult()
    {
        for (int i = 0; i < _scoreboardTransform.Length; i++)
        {
            _scoreboardTransform[i].position = _resultPos[i].position;
            _scoreborad[i].fontSize = _resultFontSize; //フォントサイズを大きくする
        }
    }
    /// <summary>
    /// スコア表示位置をデフォルトに移動
    /// </summary>
    private void ToInGame()
    {
        for (int i = 0; i < _scoreboardTransform.Length; i++)
        {
            _scoreboardTransform[i].position = _defPos[i];
            _scoreborad[i].fontSize = _defFontSize;  // 元のフォントサイズに戻す
        }
    }

    /// <summary>
    /// 表示切り替え
    /// </summary>
    private void RenderSwitch(bool Mode)
    {
        _RenderSwitch = Mode;
        for (int i = 0; i < _scoreborad.Length; i++)
        {
            _scoreborad[i].enabled = _RenderSwitch;
        }
    }

    /// <summary>
    /// ランダムのやつのスイッチ
    /// </summary>
    public void ScoreRandomSwitch(bool Mode)
    {
        _scoreRandomSwitch = Mode;
        for (int i = 0; i < _scoreborad.Length; i++)
        {
            _scoreborad[i].text = PlayerScore[i].ToString();
        }
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

    /// <summary>
    /// TitleのStartで呼ばれる関数
    /// </summary>
    public void TitleStart()
    {
        for (int i = 0; i < _scoreborad.Length; i++)
        {
            SetScore(0, i + 1);
        }
        ToInGame();
        RenderSwitch(false);
    }

    /// <summary>
    /// GameのStartで呼ばれる関数
    /// </summary>
    public void InGameStart()
    {
        ToInGame();
        RenderSwitch(true);
    }
    /// <summary>
    /// ResultのStartで呼ばれる関数
    /// </summary>
    public void ResultStart()
    {
        ToResult();
        RenderSwitch(true);
        ScoreRandomSwitch(true);
    }
}
