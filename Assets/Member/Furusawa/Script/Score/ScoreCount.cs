using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    [SerializeField] Scoremaneger scoremaneger;
    [SerializeField] public int playerNumber;

    public AudioClip pearlSE;
    private AudioSource audioSource;

      private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();  
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pearl"))
        {
            int scoreToAdd = 1;

            Scoremaneger.Instance().ScoreChenge(scoreToAdd, playerNumber);

            other.gameObject.tag = "Default";

              // Play SE
            if (pearlSE != null && audioSource != null)
            {
                audioSource.PlayOneShot(pearlSE);
            }
        }
    }
}
