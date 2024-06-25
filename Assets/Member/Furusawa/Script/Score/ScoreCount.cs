using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    [SerializeField] Scoremaneger scoremaneger;
    [SerializeField] public int playerNumber;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pearl"))
        {
            int scoreToAdd = 1;

            Scoremaneger.Instance().ScoreChenge(scoreToAdd, playerNumber);

            other.gameObject.tag = "Default";
        }
    }
}
