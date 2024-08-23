using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchObject : MonoBehaviour
{
    private PlayerInput _playerInput;

    [SerializeField] private Transform CollPoint; // 衝突ポイントのTransform
    [SerializeField] private Transform grabPoint; // 持っているオブジェクトの位置
    [SerializeField] private float heightOffset = 0.5f; // オブジェクトを積む際の高さオフセット

    private List<GameObject> grabObjects = new List<GameObject>(); // 持っているオブジェクトのリスト
    private List<GameObject> objectsInTrigger = new List<GameObject>(); // トリガー内にあるオブジェクトのリスト
    public bool IsHoldingObject { get; private set; } // オブジェクトを持っているかどうか

    private int maxPearlCount = 3; // 持てるパールの最大数
    private int maxBoxCount = 1; // 持てる箱の最大数

    private Dictionary<GameObject, int> originalLayers = new Dictionary<GameObject, int>(); // オブジェクトの元のレイヤーを保持するディクショナリ
    private Dictionary<GameObject, string> originalTags = new Dictionary<GameObject, string>(); // オブジェクトの元のタグを保持するディクショナリ

    private bool canInteract = true; // インタラクション可能かどうかのフラグ
    private Animator _animator; // プレイヤーのアニメーター


    void Start()
    {
        _playerInput = GetComponent<PlayerInput>(); // PlayerInputコンポーネントを取得
        _animator = GetComponent<Animator>(); // アニメーターの取得
    }

    private void Update()
    {
        // インタラクションが無効な場合は処理を中断
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
                         !grabObjects.Exists(grabObj => grabObj.CompareTag("HeldObject")))
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

        // "Throw"アクションがトリガーされ、オブジェクトを持っている場合の処理
        if (_playerInput.actions["Throw"].triggered && grabObjects.Count > 0)
        {
            GameObject throwObj = grabObjects[0]; // 最初のオブジェクトを取得
            grabObjects.RemoveAt(0); // リストから削除
            Rigidbody rb = throwObj.GetComponent<Rigidbody>(); // Rigidbodyを取得
            throwObj.transform.SetParent(null); // 親子関係を解除
            throwObj.transform.rotation = Quaternion.identity; // 回転をリセット
            rb.isKinematic = false; // 物理演算を有効化

            // 元のレイヤーに戻す
            if (originalLayers.ContainsKey(throwObj))
            {
                int originalLayer = originalLayers[throwObj];
                StartCoroutine(ResetLayerAfterDelay(throwObj, originalLayer, 0.05f)); // 0.05秒後にレイヤーを元に戻す
                originalLayers.Remove(throwObj);
            }

            // 元のタグに戻す
            if (originalTags.ContainsKey(throwObj))
            {
                string originalTag = originalTags[throwObj];
                StartCoroutine(ResetTagAfterDelay(throwObj, originalTag, 0.05f)); // 0.05秒後にタグを元に戻す
                originalTags.Remove(throwObj);
            }

            // 投げる処理
            if (throwObj.CompareTag("HeldObject"))
            {
                rb.AddForce(CollPoint.forward * 10.0f, ForceMode.Impulse); // 前方に力を加える
                var throwableObject = throwObj.GetComponent<ThrowableObject>();
                if (throwableObject != null)
                {
                    throwableObject.isThrown = true; // 投げられたフラグを設定
                }

                StartCoroutine(ResetKinematicAfterDelay(rb, 2.0f)); // 2秒後にキネマティックをリセット
            }
              if (grabObjects.Count == 0)
            {
               _animator.SetBool("Hold", false);
            }
        }
    }

    private void GrabObject(GameObject obj)
    {
        if (!originalLayers.ContainsKey(obj))
        {
            originalLayers[obj] = obj.layer; // 元のレイヤーを保存
        }

        if (obj.CompareTag("Pearl"))
        {
            if (!originalTags.ContainsKey(obj))
            {
                originalTags[obj] = obj.tag; // 元のタグを保存
            }
            obj.tag = "HeldObject"; // パールのタグを変更
        }

        obj.layer = LayerMask.NameToLayer("HeldObjectLayer"); // 新しいレイヤーに変更

        obj.GetComponent<Rigidbody>().isKinematic = true; // 物理演算を無効化

        // オブジェクトをCollPointの子に設定
        obj.transform.SetParent(CollPoint);

        // SetParentの後に位置を再設定
        obj.transform.localPosition = Vector3.up * heightOffset * grabObjects.Count; // 掴む位置を設定
        obj.transform.localRotation = Quaternion.identity; // 回転をリセット

        grabObjects.Add(obj); // 持っているオブジェクトのリストに追加

        _animator.SetBool("Hold", true);

        Debug.Log($"Object grabbed: {obj.name}, Position: {obj.transform.position}"); // デバッグメッセージを表示
    }

    // すべてのパールを落とす処理
    public void DropAllPearls()
    {
        for (int i = grabObjects.Count - 1; i >= 0; i--)
        {
            GameObject obj = grabObjects[i];
            if (obj.CompareTag("Pearl"))
            {
                grabObjects.RemoveAt(i); // リストから削除
                Rigidbody rb = obj.GetComponent<Rigidbody>(); // Rigidbodyを取得
                obj.transform.SetParent(null); // 親子関係を解除
                obj.transform.rotation = Quaternion.identity; // 回転をリセット
                rb.isKinematic = false; // 物理演算を有効化

                // 元のレイヤーに戻す
                if (originalLayers.ContainsKey(obj))
                {
                    int originalLayer = originalLayers[obj];
                    StartCoroutine(ResetLayerAfterDelay(obj, originalLayer, 0.05f)); // 0.05秒後にレイヤーを元に戻す
                    originalLayers.Remove(obj);
                }

                // 元のタグに戻す
                if (originalTags.ContainsKey(obj))
                {
                    string originalTag = originalTags[obj];
                    StartCoroutine(ResetTagAfterDelay(obj, originalTag, 0.05f)); // 0.05秒後にタグを元に戻す
                    originalTags.Remove(obj);
                }

                StartCoroutine(ResetKinematicAfterDelay(rb, 2.0f)); // 2秒後にキネマティックをリセット
            }

            _animator.SetBool("Hold", false);
        }
    }

    // 指定された時間後にレイヤーを元に戻す
    private IEnumerator ResetLayerAfterDelay(GameObject obj, int originalLayer, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.layer = originalLayer;
    }

    // 指定された時間後にタグを元に戻す
    private IEnumerator ResetTagAfterDelay(GameObject obj, string originalTag, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.tag = originalTag;
    }

    // 指定された時間後にRigidbodyのキネマティック状態をリセット
    private IEnumerator ResetKinematicAfterDelay(Rigidbody rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // トリガー内にオブジェクトが入ったときの処理
        if (other.CompareTag("Pearl") || other.CompareTag("boxPrefab"))
        {
            if (!objectsInTrigger.Contains(other.gameObject))
            {
                objectsInTrigger.Add(other.gameObject); // トリガー内オブジェクトリストに追加
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // トリガーからオブジェクトが出たときの処理
        if (objectsInTrigger.Contains(other.gameObject))
        {
            objectsInTrigger.Remove(other.gameObject); // トリガー内オブジェクトリストから削除
        }
    }

    // インタラクションの有効/無効を設定するメソッド
    public void SetCanInteract(bool value)
    {
        canInteract = value;
    }
}
