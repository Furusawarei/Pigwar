using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pearl_counter : MonoBehaviour
{

    private System.Action<GameObject> _hitCallback;

    public void Setup(System.Action<GameObject> hitCallback)
    {
        _hitCallback = hitCallback;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _hitCallback?.Invoke(gameObject);
            Debug.Log("“–‚½‚Á‚½");
        }
    }
    void OnTriggerEnter(Collider other)
    {

    }

}
