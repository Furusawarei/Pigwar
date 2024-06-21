using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    public InputActions _inputActions;
    public GameObject gameObject;

    void Start()
    {
        _inputActions = new InputActions();
        _inputActions.Enable();
    }
    void Update()
    {

    }

    
    void  OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "player")
        {
          transform.SetParent(gameObject.transform);
        }
        Debug.Log("すり抜けた！");
    }
}


