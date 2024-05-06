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
        //ê^éÏÇÃê∂ê¨
        _pearlprefab = Resources.Load<GameObject>("pearl");
        InvokeRepeating(nameof(Production), 0.0f, 1.0f);
    }
    //ê^éÏÇ™ÇRå¬ê∂ê¨Ç≥ÇÍÇΩÇÁç¿ïWÇâ°Ç…Ç∏ÇÁÇµÇƒÇUå¬à»è„ê∂ê¨Ç≥ÇÍÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈÅB
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
            Debug.Log("ê∂ê¨");
        }
        
    }
}