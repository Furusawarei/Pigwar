using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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


    [SerializeField] private TextMeshProUGUI[] _scoreborad=new TextMeshProUGUI[2];

    // Start is called before the first frame update
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

    //スコア増減と反映(増減数,プレイヤー)
    public void ScoreChenge(int score, int PlayerNumber)
    {
        PlayerScore[PlayerNumber-1] += score;
        if (PlayerScore[PlayerNumber - 1] < 0)
        {
            if (PlayerScore[PlayerNumber - 1] == -1) PlayerScore[PlayerNumber-1] = 1;
            else PlayerScore[PlayerNumber - 1] = 0;
            Debug.Log("マイナス");
        }
        _scoreborad[PlayerNumber-1].text = PlayerScore[PlayerNumber-1].ToString();
    }
}
