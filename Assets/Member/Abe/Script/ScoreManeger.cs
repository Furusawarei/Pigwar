using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scoremaneger : MonoBehaviour
{
    //singleton�G���A
    private static Scoremaneger instance;
    public static Scoremaneger Instance()//����
    {
        if (instance == null)
            instance = new Scoremaneger();
        return instance;
    }
    private Scoremaneger() { }
    //�ϐ�
    public int[] PlayerScore = new int[2];


    [SerializeField] private TextMeshProUGUI[] _scoreborad=new TextMeshProUGUI[2];

    // Start is called before the first frame update
    void Awake()
    {
        //����������
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

    //�X�R�A�����Ɣ��f(������,�v���C���[)
    public void ScoreChenge(int score, int PlayerNumber)
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
}
