using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchObject : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Animator _animator; // プレイヤーのアニメーター

    [SerializeField] private Transform CollPoint;  // 持っているオブジェクトの位置
    [SerializeField] private Transform grabPoint;
    [SerializeField] private float heightOffset = 0.5f; // オブジェクトを積む際の高さオフセット

    private List<GameObject> grabObjects = new List<GameObject>(); // 持っているオブジェクトのリスト
    private List<GameObject> objectsInTrigger = new List<GameObject>(); // トリガー内にあるオブジェクトのリスト
    public bool IsHoldingObject { get; private set; } // オブジェクトを持っているかどうか

    private int maxPearlCount = 3; // 持てるパールの最大数
    private int maxBoxCount = 1; // 持てる箱の最大数

    private Dictionary<GameObject, int> originalLayers = new Dictionary<GameObject, int>(); // オブジェクトの元のレイヤーを保持するディクショナリ
    private Dictionary<GameObject, string> originalTags = new Dictionary<GameObject, string>(); // オブジェクトの元のタグを保持するディクショナリ

    private bool canInteract = true; // インタラクション可能かどうかのフラグ


    void Start()
    {
        _playerInput = GetComponent<PlayerInput>(); // PlayerInputコンポーネントを取得
        _animator = GetComponent<Animator>(); // アニメーターの取得
    }

    void Update()
    {
        // インタラクションが無効な場合は処理を中断
        if (!canInteract) return;

        // "Have"アクションがトリガーされたときの処理
        if (_playerInput.actions["Have"].triggered)
        {
            GameObject closestObject = GetClosestObject(); // 近いオブジェクトを取得
            if (closestObject != null)
            {
                _animator.SetTrigger("Pick");  // オブジェクトがある場合にアニメーションをトリガー
                GrabObject(closestObject);
            }
        }

        // "Throw"アクションがトリガーされ、オブジェクトを持っている場合の処理
        if (_playerInput.actions["Throw"].triggered && grabObjects.Count > 0)
        {
            StartCoroutine(ThrowObjectCoroutine()); // コルーチンを呼び出す
        }
    }

    // コルーチンを使用して投げるアクションをスムーズに処理
    private IEnumerator ThrowObjectCoroutine()
    {
        _animator.SetTrigger("Throw"); // アニメーションをトリガー

        yield return new WaitForSeconds(0.1f); // 少し待ってから実際にオブジェクトを投げる処理を開始

        ThrowObject(); // オブジェクトを投げる処理
    }


    private GameObject GetClosestObject()
    {
        GameObject closestObject = null;
        float closestDistance = float.MaxValue;
        foreach (var obj in objectsInTrigger)
        {
            float distance = Vector3.Distance(CollPoint.position, obj.transform.position);

            // パールを持つ条件をチェック
            if (obj.CompareTag("Pearl") &&
                grabObjects.Count(grabObj => grabObj.CompareTag("HeldObject")) < maxPearlCount &&
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

        return closestObject;
    }

    private void GrabObject(GameObject obj)
    {
        if (!originalLayers.ContainsKey(obj))
        {
            originalLayers[obj] = obj.layer;  // 元のレイヤーを保存
        }

        if (obj.CompareTag("Pearl"))
        {
            if (!originalTags.ContainsKey(obj))
            {
                originalTags[obj] = obj.tag;  // 元のタグを保存
            }
            obj.tag = "HeldObject";  // パールのタグを変更
        }

        //ResetObjectPosition(obj);

        obj.layer = LayerMask.NameToLayer("HeldObjectLayer");  // 新しいレイヤーに変更

        obj.GetComponent<Rigidbody>().isKinematic = true;  // 物理演算を無効化

        StartCoroutine(MoveObjectWithAnimation(obj, CollPoint, 0.5f));  // オブジェクトを移動しつつアニメーションを再生

        grabObjects.Add(obj);  // 持っているオブジェクトのリストに追加
        _animator.SetBool("Hold", true);  // Holdアニメーションを開始

        Debug.Log($"Object grabbed: {obj.name}, Position: {obj.transform.position}");  // デバッグメッセージを表示
    }

    private void ThrowObject()
    {
        if (grabObjects.Count == 0) return; // 持っているオブジェクトがない場合は処理を終了

        GameObject throwObj = grabObjects[0]; // 最初のオブジェクトを取得
        grabObjects.RemoveAt(0); // リストから削除
        objectsInTrigger.Remove(throwObj); // トリガー内オブジェクトリストから削除
        Rigidbody rb = throwObj.GetComponent<Rigidbody>(); // Rigidbodyを取得

        throwObj.transform.SetParent(null); // 親子関係を解除
        throwObj.transform.rotation = Quaternion.identity; // 回転をリセット

        // プレイヤーのRigidbodyコンポーネントを取得して速度を加える
        Rigidbody playerRb = GetComponent<Rigidbody>(); // プレイヤーのRigidbodyを取得
        Vector3 playerVelocity = playerRb != null ? playerRb.velocity : Vector3.zero; // プレイヤーの速度を取得

        rb.isKinematic = false; // 物理演算を有効化
        rb.AddForce(transform.forward * 10f + playerVelocity, ForceMode.Impulse); // オブジェクトを前方にプレイヤーの速度を加えて投げる

        // CP_ThrowableObject コンポーネントを取得して isThrown を true に設定
        CP_ThrowableObject throwable = throwObj.GetComponent<CP_ThrowableObject>();
        if (throwable != null)
        {
            throwable.isThrown = true;
        }

        // 元のレイヤーとタグに戻す処理
        RestoreOriginalState(throwObj, rb);

        // 持っているオブジェクトがゼロになったら Hold を false に設定
        if (grabObjects.Count == 0)
        {
            _animator.SetBool("Hold", false);
        }
    }


    private void RestoreOriginalState(GameObject obj, Rigidbody rb)
    {
        // 元のレイヤーに戻す
        if (originalLayers.ContainsKey(obj))
        {
            int originalLayer = originalLayers[obj];
            StartCoroutine(ResetLayerAfterDelay(obj, originalLayer, 0.015f)); // レイヤーを元に戻す
            originalLayers.Remove(obj);
        }

        // 元のタグに戻す
        if (originalTags.ContainsKey(obj))
        {
            string originalTag = originalTags[obj];
            StartCoroutine(ResetTagAfterDelay(obj, originalTag, 0.015f)); // タグを元に戻す
            originalTags.Remove(obj);
        }

        StartCoroutine(ResetKinematicAfterDelay(rb, 2.0f)); // キネマティックをリセット
    }

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

                RestoreOriginalState(obj, rb);
            }
        }

        // 持っているオブジェクトがゼロになったら Hold を false に設定
        if (grabObjects.Count == 0)
        {
            _animator.SetBool("Hold", false);
        }
    }

    // コルーチン: オブジェクトを移動しつつアニメーションを再生
    private IEnumerator MoveObjectWithAnimation(GameObject obj, Transform targetPosition, float duration)
    {
        Vector3 startPos = obj.transform.position;  // オブジェクトの初期位置
        float elapsedTime = 0.01f;

        _animator.SetTrigger("Pick");  // アニメーションを開始

        while (elapsedTime < duration)
        {
            obj.transform.position = Vector3.Lerp(startPos, targetPosition.position, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = targetPosition.position;  // 最終位置に配置
        obj.transform.SetParent(targetPosition);  // オブジェクトをCollPointの子オブジェクトに設定

        _animator.SetBool("Hold", true);  // 持っているアニメーションを開始
    }

    // コルーチン: オブジェクトのレイヤーを遅延後にリセット
    private IEnumerator ResetLayerAfterDelay(GameObject obj, int originalLayer, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.layer = originalLayer;
    }

    // コルーチン: オブジェクトのタグを遅延後にリセット
    private IEnumerator ResetTagAfterDelay(GameObject obj, string originalTag, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.tag = originalTag;
    }

    // コルーチン: Rigidbodyのキネマティックを遅延後にリセット
    private IEnumerator ResetKinematicAfterDelay(Rigidbody rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pearl") || other.CompareTag("boxPrefab"))
        {
            objectsInTrigger.Add(other.gameObject); // トリガー内にオブジェクトを追加
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pearl") || other.CompareTag("boxPrefab"))
        {
            objectsInTrigger.Remove(other.gameObject); // トリガーからオブジェクトを削除
        }
    }

    // コルーチン: インタラクションが可能になるまで待機
    private IEnumerator WaitForInteraction()
    {
        canInteract = false;
        yield return new WaitForSeconds(1.0f); // 1秒待機
        canInteract = true;
    }
}
