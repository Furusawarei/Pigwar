using UnityEngine;
using UnityEngine.UI;

public class Scoremaneger : MonoBehaviour
{
    [SerializeField] private Text _scoreborad1;
    [SerializeField] private Text _scoreborad2;
    private int[] Score = new int[2];
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            ScorePlus(1, 1);
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            ScoreUse(1, 2);
        }
        int result1 = GetScore(1);
    }
    void ScorePlus(int score, int PlayerNumber)
    {

    }

    void ScoreUse(int score, int PlayerNumber)
    {

    }
    int GetScore(int PlayerNumber)
    {
        return 0;
    }
}
