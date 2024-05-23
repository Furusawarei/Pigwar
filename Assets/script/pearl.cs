using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class pearl : MonoBehaviour
{
    public Transform pearl_limit;
    public List<GameObject> _pearlist = new List<GameObject>();
    private GameObject _pearlprefab;


    private void Start()
    {
        //真珠の生成
        _pearlprefab = Resources.Load<GameObject>("pearl");
        InvokeRepeating(nameof(Production), 0.0f, 1.0f);
    }

    private void Production()
    {
        //真珠が6個以上にならないようにする
        if (_pearlist.Count >= 6) return;
        {
            var obj = Instantiate(_pearlprefab,Vector3.zero,Quaternion.identity, pearl_limit);
            obj.GetComponent<pearl_counter>().Setup(DeleteObject);
            if (_pearlist.Count >= 3)
            {
                //3個生成したら座標を横にずらす
                obj.transform.position = new Vector3(1.5f, 5.0f, 0.0f);
            }
            else
            {
                obj.transform.position = Vector3.up * 5;
            }
            _pearlist.Add(obj);
            Debug.Log("生成");
        }
    }

    private void DeleteObject(GameObject obj)
    {
        _pearlist.Remove(obj);
    }
}