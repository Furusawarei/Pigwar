using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUp : MonoBehaviour
{
    [SerializeField] int score1 = 0;
    [SerializeField] TextMeshProUGUI score1Text;

    [SerializeField] int score2 = 0;
    [SerializeField] TextMeshProUGUI score2Text;


    public void ClickScoreUp()
    {
        score1++;
        score1Text.text = string.Format("Player1:{0}", score1);

        Debug.Log("player1:" + score1);
    }

    public void ClickScoreUp2()
    {
        score2Text.text = string.Format("Player2:{0}", score2);

        score2++;
        Debug.Log("player2:" + score2);
    }

}


