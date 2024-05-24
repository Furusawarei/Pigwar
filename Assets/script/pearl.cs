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
        //ê^éÏÇÃê∂ê¨
        _pearlprefab = Resources.Load<GameObject>("pearl");
        InvokeRepeating(nameof(Production), 0.0f, 1.0f);
    }

    private void Production()
    {
        //ê^éÏÇ™6å¬à»è„Ç…Ç»ÇÁÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
        if (_pearlist.Count >= 6) return;
        {
            var obj = Instantiate(_pearlprefab,Vector3.zero,Quaternion.identity, pearl_limit);
            obj.GetComponent<pearl_counter>().Setup(DeleteObject);
            if (_pearlist.Count >= 3)
            {
                //3å¬ê∂ê¨ÇµÇΩÇÁç¿ïWÇâ°Ç…Ç∏ÇÁÇ∑
                obj.transform.position = new Vector3(1.5f, 5.0f, 0.0f);
            }
            else
            {
                obj.transform.position = Vector3.up * 5;
            }
            _pearlist.Add(obj);
            Debug.Log("ê∂ê¨");
        }
    }

    private void DeleteObject(GameObject obj)
    {
        _pearlist.Remove(obj);
    }
}