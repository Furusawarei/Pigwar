using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    public ActionControl _inputActions;
    public GameObject _gameObject;

    void Start()
    {
        _inputActions = new ActionControl();
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


