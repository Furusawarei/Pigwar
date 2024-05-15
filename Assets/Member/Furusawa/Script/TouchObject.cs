using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : MonoBehaviour
{
    public GameObject gameObject;
    public GameObject target;
    public bool isRelese; //追加
    GameObject cube; //追加
    PlayerMove ballDir; //追加
    Rigidbody rb;


    void Start()
    {
        isRelese = true;
        rb = GetComponent<Rigidbody>(); //追加
        cube = GameObject.Find("Player"); //追加
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isRelese)
            {
                transform.parent = null;
                isRelese = true;
                ballDir = cube.GetComponent<PlayerMove>(); //追加
                rb.velocity = ballDir.direction * 10.0f; //追加
            }
        }
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player")
        {
            isRelese = false; //追加
            this.transform.position = new Vector3(target.transform.position.x, 0.5f, target.transform.position.z);
            transform.SetParent(gameObject.transform);
        }
    }
}