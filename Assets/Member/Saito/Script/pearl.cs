using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pearl : MonoBehaviour
{
    [SerializeField] private float _PosX;
    public Transform pearl_limit;
    public List<GameObject> _pearlist = new List<GameObject>();
    private GameObject _pearlprefab;
    private float rnd;

    private void Start()
    {
        // パールをロードする
        _pearlprefab = Resources.Load<GameObject>("pearl_main");
        InvokeRepeating(nameof(Production), 4.0f, 3.0f);
    }

    private void Update()
    {
        rnd = Random.Range(2.7f, 2.2f);
    }

    private void Production()
    {
        // パールが6個以上の場合は生成しない
        if (_pearlist.Count >= 9) return;
        {
            var obj = Instantiate(_pearlprefab, Vector3.zero, Quaternion.identity, pearl_limit);
            obj.GetComponent<pearl_counter>().Setup(DeleteObject);
            obj.transform.position = new Vector3(_PosX, 0.6f, rnd);

            /* if (_pearlist.Count >= 3)
            {
                // 3個以上の場合のパールの位置を調整する
                obj.transform.position = new Vector3(-8f, 5.0f, 0.0f);
            }
            else
            {
                obj.transform.position = new Vector3(-8f, 5.0f, 0.0f);
            }*/
            
            _pearlist.Add(obj);
            Debug.Log("生成完了");
        }
    }

    // パール削除用のメソッド
    private void DeleteObject(GameObject obj)
    {
        _pearlist.Remove(obj);
    }
}
