using System.Collections;
using System.Collections.Generic;
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
    private GameObject grabbedBox = null; // 掴んだ箱オブジェクト
    private int maxGrabCount = 3; // 最大保持オブジェクト数
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
            // 掴んだ箱がない場合のみ掴む
            if (grabbedBox == null)
            {
                // 最大保持数未満の場合のみ掴む
                if (grabObjects.Count < maxGrabCount)
                {
                    // レイキャストを実行
                    if (Physics.Raycast(rayPoint.position, rayPoint.forward, out hit, rayDistance))
                    {
                        // ヒットしたオブジェクトが"pearl"タグを持つ場合
                        if (hit.collider != null && hit.collider.CompareTag("Pearl"))
                        {
                            GameObject grabbedObj = hit.collider.gameObject;
                            grabbedObj.GetComponent<Rigidbody>().isKinematic = true; // オブジェクトを物理的に固定
                            grabbedObj.transform.position = grabPoint.position + Vector3.up * heightOffset * grabObjects.Count; // 高さオフセットを適用
                            grabbedObj.transform.rotation = Quaternion.identity; // 回転をリセット
                            grabbedObj.transform.SetParent(transform); // オブジェクトの親をこのオブジェクトに設定
                            grabObjects.Add(grabbedObj); // オブジェクトをリストに追加
                        }
                        // ヒットしたオブジェクトが"boxPrefab"タグを持つ場合
                        else if (hit.collider != null && hit.collider.CompareTag("boxPrefab"))
                        {
                            grabbedBox = hit.collider.gameObject;
                            grabbedBox.GetComponent<Rigidbody>().isKinematic = true; // オブジェクトを物理的に固定
                            grabbedBox.transform.position = grabPoint.position; // プレイヤーの正面に移動
                            grabbedBox.transform.rotation = Quaternion.Euler(0, 0, 0); // 回転を横に設定
                            grabbedBox.transform.SetParent(transform); // オブジェクトの親をこのオブジェクトに設定
                        }
                    }
                }
            }
        }
        // "Throw"アクションがトリガーされた場合
        else if (_playerInput.actions["Throw"].triggered)
        {
            // 掴んでいる箱がある場合
            if (grabbedBox != null)
            {
                grabbedBox.transform.SetParent(null); // オブジェクトの親を解除
                grabbedBox.GetComponent<Rigidbody>().isKinematic = false; // 物理的に固定を解除
                grabbedBox = null; // 掴んだ箱の参照をクリア
            }
            // 掴んでいるオブジェクトがある場合
            else if (grabObjects.Count > 0)
            {
                GameObject throwObj = grabObjects[0];
                grabObjects.RemoveAt(0); // リストの最初のオブジェクトを取り出す
                Rigidbody rb = throwObj.GetComponent<Rigidbody>();
                throwObj.transform.SetParent(null); // オブジェクトの親を解除
                throwObj.transform.rotation = Quaternion.identity; // 回転をリセット
                rb.isKinematic = false; // 物理的に固定を解除

                // フラグを設定
                ThrowableObject throwable = throwObj.GetComponent<ThrowableObject>();
                if (throwable != null)
                {
                    throwable.isThrown = true;
                }

                rb.AddForce(rayPoint.forward * 10.0f, ForceMode.Impulse); // オブジェクトに力を加えて前方に発射

                // 残りのオブジェクトを再配置
                for (int i = 0; i < grabObjects.Count; i++)
                {
                    grabObjects[i].transform.position = grabPoint.position + Vector3.up * heightOffset * i; // 高さオフセットを適用
                    grabObjects[i].transform.rotation = Quaternion.identity; // 回転をリセット
                }
            }
        }
    }
}
