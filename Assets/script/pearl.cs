using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class pearl : MonoBehaviour
{
    public List<GameObject> _pearlist = new List<GameObject>();
    private GameObject _pearlprefab;

    private void Start()
    {
        //真珠の生成
        _pearlprefab = Resources.Load<GameObject>("pearl");
        InvokeRepeating(nameof(Production), 0.0f, 1.0f);
    }
    //真珠が３個生成されたら座標を横にずらして６個以上生成されないようにする。
    private void Production()
    {
        if (_pearlist.Count >= 6) return;
        {
            var obj = Instantiate(_pearlprefab);
            if (_pearlist.Count >= 3)
            {
                obj.transform.position= new Vector3(1.5f, 5.0f, 0.0f);
            }
            else
            {
                obj.transform.position = Vector3.up * 5;
            }
            _pearlist.Add(obj);
            Debug.Log("生成");
        }
        
    }
}