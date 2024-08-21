using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchObject : MonoBehaviour
{
    private PlayerInput _playerInput;

    // 衝突ポイントとオブジェクトを持つ位置のTransform
    [SerializeField] private Transform CollPoint;
    [SerializeField] private Transform grabPoint;
    [SerializeField] private float heightOffset = 0.5f; // オブジェクトを積む際の高さオフセット

    private List<GameObject> grabObjects = new List<GameObject>(); // 持っているオブジェクトのリスト
    private List<GameObject> objectsInTrigger = new List<GameObject>(); // トリガー内にあるオブジェクトのリスト
    public bool IsHoldingObject { get; private set; } // オブジェクトを持っているかどうか

    private int maxPearlCount = 3; // 持てるパールの最大数
    private int maxBoxCount = 1; // 持てる箱の最大数

    private Dictionary<GameObject, int> originalLayers = new Dictionary<GameObject, int>(); // オブジェクトの元のレイヤーを保持するディクショナリ

    private bool canInteract = true; // インタラクション可能かどうかのフラグ

    public Animator _HoldAnimaotr;

    void Start()
    {
        _HoldAnimaotr = GetComponent<Animator>();

        // PlayerInputのコンポーネントを取得
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        // インタラクションが無効な場合、処理を中断
        if (!canInteract) return;

        // "Have"アクションがトリガーされたときの処理
        if (_playerInput.actions["Have"].triggered)
        {
            GameObject closestObject = null;
            float closestDistance = float.MaxValue;

            // トリガー内のオブジェクトを走査して最も近いオブジェクトを取得
            foreach (var obj in objectsInTrigger)
            {
                float distance = Vector3.Distance(CollPoint.position, obj.transform.position);

                // パールを持つ条件をチェック
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
                // 箱を持つ条件をチェック
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
                //  RearrangePearls(); // パールの位置を再配置
            }
        }

        // "Throw"アクションがトリガーされ、オブジェクトを持っている場合の処理
        // "Throw"アクションがトリガーされ、オブジェクトを持っている場合の処理
        if (_playerInput.actions["Throw"].triggered && grabObjects.Count > 0)
        {
            GameObject throwObj = grabObjects[0]; // 最初のオブジェクトを取得
            grabObjects.RemoveAt(0); // リストから削除
            Rigidbody rb = throwObj.GetComponent<Rigidbody>();
            throwObj.transform.SetParent(null); // 親子関係を解除
            throwObj.transform.rotation = Quaternion.identity; // 回転をリセット
            rb.isKinematic = false; // 物理演算を有効化

            // 元のレイヤーに戻す
            if (originalLayers.ContainsKey(throwObj))
            {
                throwObj.layer = originalLayers[throwObj];
                originalLayers.Remove(throwObj);
            }

            // 投げる処理
            if (throwObj.CompareTag("Pearl"))
            {
                rb.AddForce(CollPoint.forward * 10.0f, ForceMode.Impulse); // 前方に力を加える
                var throwableObject = throwObj.GetComponent<ThrowableObject>();
                if (throwableObject != null)
                {
                    throwableObject.isThrown = true; // 投げられたフラグを設定
                }
                StartCoroutine(ResetKinematicAfterDelay(rb, 2.0f)); // 2秒後にキネマティックをリセット
            }

            // RearrangePearls(); // パールの位置を再配置

            // 投げた後に持っているオブジェクトが0になったかどうか確認
            if (grabObjects.Count == 0)
            {
                _HoldAnimaotr.SetBool("HoldBool", false); // 持っているオブジェクトがないのでHoldBoolをfalseに設定
                _HoldAnimaotr.SetBool("MoveBool", true); // 投げた後は移動可能になる
            }
        }
    }

    // private void LateUpdate()
    // {
    //     // オブジェクトを持っている場合の位置を修正
    //     if (grabObjects.Count > 0)
    //     {
    //         for (int i = 0; i < grabObjects.Count; i++)
    //         {
    //             grabObjects[i].transform.position = grabPoint.position + Vector3.up * heightOffset * i;
    //         }
    //     }
    // }


    private void GrabObject(GameObject obj)
    {
        if (!originalLayers.ContainsKey(obj))
        {
            originalLayers[obj] = obj.layer; // 元のレイヤーを保存
        }

        obj.layer = LayerMask.NameToLayer("HeldObjectLayer"); // 新しいレイヤーに変更

        obj.GetComponent<Rigidbody>().isKinematic = true; // 物理演算を無効化

        // オブジェクトをCollPointの子に設定
        obj.transform.SetParent(CollPoint);

        // SetParentの後に位置を再設定
        obj.transform.localPosition = Vector3.up * heightOffset * grabObjects.Count; // 掴む位置を設定
        obj.transform.localRotation = Quaternion.identity; // 回転をリセット

        grabObjects.Add(obj); // 持っているオブジェクトのリストに追加

        _HoldAnimaotr.SetBool("HoldBool", true); // オブジェクトを持った時にHoldBoolをtrueに設定
        _HoldAnimaotr.SetBool("MoveBool", false); // オブジェクトを持った時にMoveBoolをfalseに設定

        Debug.Log($"Object grabbed: {obj.name}, Position: {obj.transform.position}"); // デバッグメッセージを表示
    }




    // // パールの位置を再配置する処理
    // private void RearrangePearls()
    // {
    //     for (int i = 0; i < grabObjects.Count; i++)
    //     {
    //         if (grabObjects[i].CompareTag("Pearl"))
    //         {
    //             grabObjects[i].transform.position = grabPoint.position + Vector3.up * heightOffset * i; // 新しい位置を設定
    //         }
    //     }
    // }

    // すべてのパールを落とす処理
    public void DropAllPearls()
    {
        for (int i = grabObjects.Count - 1; i >= 0; i--)
        {
            GameObject obj = grabObjects[i];
            if (obj.CompareTag("Pearl"))
            {
                grabObjects.RemoveAt(i);
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                obj.transform.SetParent(null); // 親子関係を解除
                obj.transform.rotation = Quaternion.identity; // 回転をリセット
                rb.isKinematic = false; // 物理演算を有効化

                // 元のレイヤーに戻す
                if (originalLayers.ContainsKey(obj))
                {
                    obj.layer = originalLayers[obj];
                    originalLayers.Remove(obj);
                }
            }
        }
    }

    // トリガーに入った時の処理
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pearl") || other.CompareTag("boxPrefab"))
        {
            if (!objectsInTrigger.Contains(other.gameObject))
            {
                objectsInTrigger.Add(other.gameObject); // トリガー内にオブジェクトを追加
            }
        }
    }

    // トリガーから出た時の処理
    private void OnTriggerExit(Collider other)
    {
        if (objectsInTrigger.Contains(other.gameObject))
        {
            objectsInTrigger.Remove(other.gameObject); // トリガーからオブジェクトを削除
        }
    }

    // 一定時間後にRigidbodyのキネマティックをリセットするコルーチン
    private IEnumerator ResetKinematicAfterDelay(Rigidbody rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.isKinematic = false;
    }

    // インタラクションを無効にする処理
    public void DisableInteraction()
    {
        canInteract = false;
    }
}
