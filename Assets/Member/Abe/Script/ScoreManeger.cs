using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Scoremaneger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _scoreborad=new TextMeshProUGUI[2];

    [SerializeField] private int _UsePoints = 2;
    //���V�[���܂�������
    private int[] PlayerScore = new int[2];

    // Start is called before the first frame update
    void Start()
    {
        //����������
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
    //�X�R�A�����Ɣ��f(������,�v���C���[)
    void ScoreChenge(int score, int PlayerNumber)
    {
        PlayerScore[PlayerNumber-1] += score;
        if (PlayerScore[PlayerNumber - 1] < 0)
        {
            if (PlayerScore[PlayerNumber - 1] == -1) PlayerScore[PlayerNumber-1] = 1;
            else PlayerScore[PlayerNumber - 1] = 0;
            Debug.Log("�}�C�i�X");
        }
        _scoreborad[PlayerNumber-1].text = PlayerScore[PlayerNumber-1].ToString();
    }
    //�X�R�A�擾(�v���C���[)
    int GetScore(int PlayerNumber)
    {
        return PlayerScore[PlayerNumber-1];
    }
}
