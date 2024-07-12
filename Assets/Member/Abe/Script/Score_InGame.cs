using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// このスクリプトはテスト、使い方サンプル用です　実際の動作には使わないでください
/// </summary>
public class Score_InGame : MonoBehaviour
{

    [SerializeField] private List<TextMeshProUGUI> _scoreborad;
    // Start is called before the first frame update
    void Start()
    {
        //初期化
        Scoremaneger.Instance().SetScore(0, 1);
        Scoremaneger.Instance().SetScore(0, 2);
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
        if (Input.GetKeyUp(KeyCode.Q))
        {
            Scoremaneger.Instance().ToResult();//これでスコア表示をリザルト型に移動
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            Scoremaneger.Instance().ToInGame();//これでスコア表示をゲーム型に移動
            Scoremaneger.Instance().ScoreRandomSwitch();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Scoremaneger.Instance().RenderSwitch();//これでスコア表示のオンオフ
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene("MatukawaResult_Copy");
        }
    }


}
