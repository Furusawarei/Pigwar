using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchObject : MonoBehaviour
{
    private PlayerInput _playerInput;

    [SerializeField] private Transform grabPoint; // オブジェクトを持つ位置
    [SerializeField] private Transform rayPoint; // レイの発射位置
    [SerializeField] private float rayDistance = 2.0f; // レイの距離
    [SerializeField] private float heightOffset = 0.5f; // オブジェクトを積むときの高さオフセット
    private List<GameObject> grabObjects = new List<GameObject>(); // 掴んだオブジェクトのリスト
    private int maxPearlCount = 3; // 最大パール保持数
    private int maxBoxCount = 1; // 最大ボックス保持数
    RaycastHit hit; // レイキャストのヒット情報

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        // "Have"アクションがトリガーされた場合
        if (_playerInput.actions["Have"].triggered)
        {
            // レイキャストを実行
            if (Physics.Raycast(rayPoint.position, rayPoint.forward, out hit, rayDistance))
            {
                // ヒットしたオブジェクトが"Pearl"タグを持つ場合
                if (hit.collider != null && hit.collider.CompareTag("Pearl"))
                {
                    if (grabObjects.Count(obj => obj.CompareTag("Pearl")) < maxPearlCount &&
                        !grabObjects.Exists(obj => obj.CompareTag("boxPrefab")))
                    {
                        GrabObject(hit.collider.gameObject);
                    }
                }
                // ヒットしたオブジェクトが"boxPrefab"タグを持つ場合
                else if (hit.collider != null && hit.collider.CompareTag("boxPrefab"))
                {
                    if (grabObjects.Count(obj => obj.CompareTag("boxPrefab")) < maxBoxCount &&
                        !grabObjects.Exists(obj => obj.CompareTag("Pearl")))
                    {
                        GrabObject(hit.collider.gameObject);
                    }
                }
            }
        }

        // "Throw"アクションがトリガーされた場合
        if (_playerInput.actions["Throw"].triggered && grabObjects.Count > 0)
        {
            GameObject throwObj = grabObjects[grabObjects.Count - 1];
            grabObjects.RemoveAt(grabObjects.Count - 1); // リストの最後のオブジェクトを取り出す
            Rigidbody rb = throwObj.GetComponent<Rigidbody>();
            throwObj.transform.SetParent(null); // オブジェクトの親を解除
            throwObj.transform.rotation = Quaternion.identity; // 回転をリセット
            rb.isKinematic = false; // 物理的に固定を解除

            // パールの場合、前方に力を加えて投げる
            if (throwObj.CompareTag("Pearl"))
            {
                rb.AddForce(rayPoint.forward * 10.0f, ForceMode.Impulse); // オブジェクトに力を加えて前方に発射
            }

            // `boxPrefab`の場合、その場に置くため力を加えない
        }
    }

    private void GrabObject(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().isKinematic = true; // オブジェクトを物理的に固定
        obj.transform.position = grabPoint.position + Vector3.up * heightOffset * grabObjects.Count; // 高さオフセットを適用
        obj.transform.rotation = Quaternion.identity; // 回転をリセット
        obj.transform.SetParent(transform); // オブジェクトの親をこのオブジェクトに設定
        grabObjects.Add(obj); // オブジェクトをリストに追加
    }
}
