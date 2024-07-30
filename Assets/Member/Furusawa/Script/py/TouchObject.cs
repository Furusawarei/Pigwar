using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchObject : MonoBehaviour
{
    private PlayerInput _playerInput;

    [SerializeField] private Transform CollPoint; // レイの発射位置

    [SerializeField] private Transform grabPoint; // オブジェクトを持つ位置
    [SerializeField] private float heightOffset = 0.5f; // オブジェクトを積むときの高さオフセット

    private List<GameObject> grabObjects = new List<GameObject>(); // 掴んだオブジェクトのリスト
    private List<GameObject> objectsInTrigger = new List<GameObject>(); // トリガー内のオブジェクトのリスト
    public bool IsHoldingObject { get; private set; } // オブジェクトを持っているかどうかを示すプロパティ

    private int maxPearlCount = 3; // 最大パール保持数
    private int maxBoxCount = 1; // 最大ボックス保持数

    private Dictionary<GameObject, int> originalLayers = new Dictionary<GameObject, int>(); // オブジェクトの元のレイヤーを記憶する辞書

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        // "Have"アクションがトリガーされた場合
        if (_playerInput.actions["Have"].triggered)
        {
            // トリガー内のオブジェクトから最も近いものを探す
            GameObject closestObject = null;
            float closestDistance = float.MaxValue;

            foreach (var obj in objectsInTrigger)
            {
                float distance = Vector3.Distance(CollPoint.position, obj.transform.position);

                // パールまたはボックスを掴む条件を確認
                if (obj.CompareTag("Pearl") &&
                    grabObjects.Count(grabObj => grabObj.CompareTag("Pearl")) < maxPearlCount &&
                    !grabObjects.Exists(grabObj => grabObj.CompareTag("boxPrefab")))
                {
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestObject = obj;
                    }
                }
                else if (obj.CompareTag("boxPrefab") &&
                         grabObjects.Count(grabObj => grabObj.CompareTag("boxPrefab")) < maxBoxCount &&
                         !grabObjects.Exists(grabObj => grabObj.CompareTag("Pearl")))
                {
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestObject = obj;
                    }
                }
            }

            // 最も近いオブジェクトを掴む
            if (closestObject != null)
            {
                GrabObject(closestObject);
            }
        }

        // "Throw"アクションがトリガーされた場合
        if (_playerInput.actions["Throw"].triggered && grabObjects.Count > 0)
        {
            GameObject throwObj = grabObjects[0]; // リストの最初のオブジェクトを取り出す
            grabObjects.RemoveAt(0);
            Rigidbody rb = throwObj.GetComponent<Rigidbody>();
            throwObj.transform.SetParent(null); // オブジェクトの親を解除
            throwObj.transform.rotation = Quaternion.identity; // 回転をリセット
            rb.isKinematic = false; // 物理的に固定を解除

            // オブジェクトのレイヤーを元に戻す
            if (originalLayers.ContainsKey(throwObj))
            {
                throwObj.layer = originalLayers[throwObj];
                originalLayers.Remove(throwObj);
            }

            // パールの場合、前方に力を加えて投げる
            if (throwObj.CompareTag("Pearl"))
            {
                rb.AddForce(CollPoint.forward * 10.0f, ForceMode.Impulse); // オブジェクトに力を加えて前方に発射
                // 投げたオブジェクトにisThrownを設定
                var throwableObject = throwObj.GetComponent<ThrowableObject>();
                if (throwableObject != null)
                {
                    throwableObject.isThrown = true;
                }
                StartCoroutine(ResetKinematicAfterDelay(rb, 2.0f)); // 2秒後にキネマティックを解除
            }
            // `boxPrefab`の場合、その場に置くため力を加えない
        }

        IsHoldingObject = grabObjects.Count > 0;
    }

    private void GrabObject(GameObject obj)
    {
        // 元のレイヤーを記憶
        if (!originalLayers.ContainsKey(obj))
        {
            originalLayers[obj] = obj.layer;
        }

        // レイヤーを変更（例：掴んだオブジェクトのレイヤーを「HeldObjectLayer」に変更）
        obj.layer = LayerMask.NameToLayer("HeldObjectLayer");

        obj.GetComponent<Rigidbody>().isKinematic = true; // オブジェクトを物理的に固定
        obj.transform.position = grabPoint.position + Vector3.up * heightOffset * grabObjects.Count; // 高さオフセットを適用
        obj.transform.rotation = Quaternion.identity; // 回転をリセット
        obj.transform.SetParent(transform); // オブジェクトの親をこのオブジェクトに設定
        grabObjects.Add(obj); // オブジェクトをリストに追加
    }

    public void DropAllPearls()
    {
        for (int i = grabObjects.Count - 1; i >= 0; i--)
        {
            GameObject obj = grabObjects[i];
            if (obj.CompareTag("Pearl"))
            {
                grabObjects.RemoveAt(i);
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                obj.transform.SetParent(null); // オブジェクトの親を解除
                obj.transform.rotation = Quaternion.identity; // 回転をリセット
                rb.isKinematic = false; // 物理的に固定を解除

                // オブジェクトのレイヤーを元に戻す
                if (originalLayers.ContainsKey(obj))
                {
                    obj.layer = originalLayers[obj];
                    originalLayers.Remove(obj);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // トリガー内に入ったオブジェクトをリストに追加
        if (other.CompareTag("Pearl") || other.CompareTag("boxPrefab"))
        {
            objectsInTrigger.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // トリガーから出たオブジェクトをリストから削除
        if (objectsInTrigger.Contains(other.gameObject))
        {
            objectsInTrigger.Remove(other.gameObject);
        }
    }

    private IEnumerator ResetKinematicAfterDelay(Rigidbody rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.isKinematic = false; // キネマティックを解除
    }
}
