using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class pearl : MonoBehaviour
{
    public Transform pearl_limit;
    public List<GameObject> _pearlist = new List<GameObject>();
    private GameObject _pearlprefab;
    private float rnd;

    private void Start()
    {
        //�^��̐���
        _pearlprefab = Resources.Load<GameObject>("pearl");
        InvokeRepeating(nameof(Production), 0.0f, 2.0f);
        
    }

    private void Update()
    {
        rnd = Random.Range(2.6f, 2.3f);
    }

    private void Production()
    {
        //�^�삪6�ȏ�ɂȂ�Ȃ��悤�ɂ���
        if (_pearlist.Count >= 6) return;
        {
            var obj = Instantiate(_pearlprefab,Vector3.zero,Quaternion.identity, pearl_limit);
            obj.GetComponent<pearl_counter>().Setup(DeleteObject);
            obj.transform.position = new Vector3(-6.5f, 1.3f, rnd);

            /* if (_pearlist.Count >= 3)
            {
                //3������������W�����ɂ��炷
                obj.transform.position = new Vector3(-8f, 5.0f, 0.0f);
            }
            else
            {
                obj.transform.position = new Vector3(-8f, 5.0f, 0.0f);
            }*/
            _pearlist.Add(obj);
            Debug.Log("����");
        }
    }
    //�^��̗v�f�����폜
    private void DeleteObject(GameObject obj)
    {
        _pearlist.Remove(obj);
    }
}