using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLabel : MonoBehaviour
{
    public int playerNumber;
    private TextMeshPro label;
    // Start is called before the first frame update
    void Start()
    {
        label =  GetComponentInChildren<TextMeshPro>();

        if (playerNumber == 1)
        {
            label.text = "P1";
        }

        if (playerNumber == 2)
        {
            label.text = "P2";
        }
    }
}
