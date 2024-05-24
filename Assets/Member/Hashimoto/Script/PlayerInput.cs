using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEditorInternal;

public class PlayerInput : MonoBehaviour
{
    private ActionControl _actionControl;

    private Vector3 BefoPos;

    private Rigidbody rb;
    private float upForce;
    private bool Jumping = false;

    private void Awake()
    {
        _actionControl = new ActionControl();
        _actionControl.Enable();
    }

    void Start()
    {
        upForce = 250;
        rb = GetComponent<Rigidbody>();//�W�����v
    }

    void Update()
    {
        var pos = _actionControl.Player.Move.ReadValue<Vector2>();
        transform.position += new Vector3(pos.x, 0, pos.y) * 0.05f; //�ړ�

        Vector3 diff = transform.position - BefoPos;//�ړ���������
        diff.y = 0;
        BefoPos = transform.position;
        if (diff.magnitude > 0.01f)
        {
           transform.rotation = Quaternion.LookRotation(diff);
        }

        

        if (_actionControl.Player.Jump.triggered && !Jumping)//�W�����v
        {
            //rb.velocity = Vector3.up * upForce;
            rb.AddForce(new Vector3(0, upForce, 0));
            Jumping = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            Jumping = false;
        }
    }
}
