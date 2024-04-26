using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scoremaneger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _scoreborad=new TextMeshProUGUI[2];

    [SerializeField] private int _UsePoints = 2;
    //↓シーンまたぐあれ
    private int[] PlayerScore = new int[2];

    // Start is called before the first frame update
    void Start()
    {
        //初期化処理
        PlayerScore[0]= 0;
        PlayerScore[1] = 0;
        _scoreborad[0].text = GetScore(1).ToString();
        _scoreborad[1].text = GetScore(2).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.K)) 
        {
            ScoreChenge(1, 2);
            Debug.Log("2P+1");
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            ScoreChenge(1, 1);
            Debug.Log("1P+1");
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            ScoreChenge(-_UsePoints, 2);
            Debug.Log("2P-2");
        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            ScoreChenge(-_UsePoints, 1);
            Debug.Log("1P-2"); ;
        }
    }
    //スコア増減と反映(増減数,プレイヤー)
    void ScoreChenge(int score, int PlayerNumber)
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
    //スコア取得(プレイヤー)
    int GetScore(int PlayerNumber)
    {
        return PlayerScore[PlayerNumber-1];
    }
}
