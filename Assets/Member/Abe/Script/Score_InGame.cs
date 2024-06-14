using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Score_InGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _scoreborad = new TextMeshProUGUI[2];
    // Start is called before the first frame update
    void Start()
    {
        //初期化
        Scoremaneger.Instance().PlayerScore[0] = 0;
        Scoremaneger.Instance().PlayerScore[1] = 0;
        //0を表示する
        _scoreborad[0].text = Scoremaneger.Instance().PlayerScore[0].ToString();
        _scoreborad[1].text = Scoremaneger.Instance().PlayerScore[1].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            Scoremaneger.Instance().ScoreChenge(1, 2);//増減値,プレイヤー
            Debug.Log("2P+1");
        }
        if (Input.GetKeyUp(KeyCode.H))
        {
            Scoremaneger.Instance().ScoreChenge(1, 1);
            Debug.Log("1P+1");
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            Scoremaneger.Instance().ScoreChenge(-2, 2);//マイナスで減算する
            Debug.Log("2P-2");
        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            Scoremaneger.Instance().ScoreChenge(-2, 1);
            Debug.Log("1P-2"); ;
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            Scoremaneger.Instance().ToResult();//ここでスコア表示を移動
            SceneManager.LoadScene("MatukawaResult_Copy");
        }
    }
}
