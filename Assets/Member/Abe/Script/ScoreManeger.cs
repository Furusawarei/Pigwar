using TMPro;
using UnityEngine;

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


    [SerializeField, Header("スコア表示に使うtext(TMP)")] private TextMeshProUGUI[] _scoreborad = new TextMeshProUGUI[2];
    [SerializeField,Header("スコア表示に使うtext(TMP)")] private Transform[] _scoreboardTransform = new Transform[2];
    [SerializeField,Header("リザルトに遷移したときに移動する場所")] private Transform[] _resultPos = new Transform[2];
    private bool _scoreRandomSwitch = false;

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

    public void SetScore(int Score,int PlayerNumber)
    {
        PlayerScore[PlayerNumber - 1] = Score;
    }
    public int GetScore(int PlayerNumber)
    {
        return PlayerScore[PlayerNumber-1];
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
    /// リザルト画面遷移時のスコア位置移動
    /// </summary>
    public void ToResult()
    {
        for (int i = 0; i < _scoreboardTransform.Length; i++)
        {
            _scoreboardTransform[i].position = _resultPos[i].position;
            _scoreborad[i].fontSize *= 2;
        }
        ScoreRandomSwitch();
    }

    /// <summary>
    /// ランダムのやつを終了させる用 
    /// </summary>
    public void ScoreRandomSwitch()
    {
        _scoreRandomSwitch= !_scoreRandomSwitch;
        _scoreborad[0].text = PlayerScore[0].ToString();
        _scoreborad[1].text = PlayerScore[1].ToString();
    }

    /// <summary>
    /// 勝敗判定
    /// </summary>
    /// <returns>引き分けなら0,そうでないなら勝ったプレイヤー番号がintで</returns>
    public int Judge()
    {
        if (PlayerScore[0] > PlayerScore[1]) return 1;
        else if (PlayerScore[0] < PlayerScore[1]) return 2;
        else return 0;
    }
}
