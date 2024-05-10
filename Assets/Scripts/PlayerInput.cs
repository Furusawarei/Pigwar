using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private ActionControl _actionControl;
    // Start is called before the first frame update

    private void Awake()
    {
        _actionControl = new ActionControl();
        _actionControl.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        var pos = _actionControl.Player.Move.ReadValue<Vector2>();
        transform.localPosition += new Vector3(pos.x, 0, pos.y) * 0.05f;
    }
}
