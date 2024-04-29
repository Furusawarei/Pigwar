using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pearl : MonoBehaviour
{
    private List<GameObject> _pearlist = new List<GameObject>();
    private GameObject _pearlprefab;
    private int count = 1;
  
    private void Start()
    {
        _pearlprefab = Resources.Load<GameObject>("pearl");
        InvokeRepeating(nameof(Production), 0.0f, 1.0f);
        


    }

    private void Production()
    {
        if (_pearlist.Count >= 3) return;
        {
            var obj = Instantiate(_pearlprefab);
            obj.transform.localPosition = Vector3.up * 5;
            _pearlist.Add(obj);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(count++);
        if(count > 4)
        {
            if(this.gameObject.CompareTag("Player"))
            {
                GameObject Obj = GameObject.Find("Pearl(Clone)");
                Destroy(Obj);
            }

        }
    }
}
