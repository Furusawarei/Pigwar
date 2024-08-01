using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    [SerializeField] private Scoremaneger scoremaneger;
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

            // Change the tag to "Default" when it enters the trigger
            other.gameObject.tag = "Default";

            // Play SE
            if (pearlSE != null && audioSource != null)
            {
                audioSource.PlayOneShot(pearlSE);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Default"))
        {
            // Restore the tag when it exits the trigger
            other.gameObject.tag = "Pearl";
        }
    }
}
