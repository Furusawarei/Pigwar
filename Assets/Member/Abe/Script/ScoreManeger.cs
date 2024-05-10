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


    [SerializeField] private TextMeshProUGUI[] _scoreborad = new TextMeshProUGUI[2];
    [SerializeField] private Transform[] _scoreboardTransform = new Transform[2];
    [SerializeField] private Transform[] _resultPos = new Transform[2];
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

    //スコア増減と反映(増減数,プレイヤー)
    public void ScoreChenge(int score, int PlayerNumber)
    {
        PlayerNumber -= 1;
        PlayerScore[PlayerNumber] += score;
        if (PlayerScore[PlayerNumber] < 0)
        {
            if (PlayerScore[PlayerNumber] == -1) PlayerScore[PlayerNumber] = 1;
            else PlayerScore[PlayerNumber] = 0;
            Debug.Log("マイナス");
        }
        _scoreborad[PlayerNumber].text = PlayerScore[PlayerNumber].ToString();
    }

    //リザルト画面遷移時のスコア位置移動
    public void ToResult()
    {
        for (int i = 0; i < _scoreboardTransform.Length; i++)
        {
            _scoreboardTransform[i].position = _resultPos[i].position;
            _scoreborad[i].fontSize *= 3;
        }
        ScoreRandomSwitch();
    }

    //ランダムのやつを終了させる用
    public void ScoreRandomSwitch()
    {
        _scoreRandomSwitch= !_scoreRandomSwitch;
        _scoreborad[0].text = PlayerScore[0].ToString();
        _scoreborad[1].text = PlayerScore[1].ToString();
    }
}
