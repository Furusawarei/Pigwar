using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchObject : MonoBehaviour
{
    private PlayerInput _playerInput; // プレイヤーの入力を管理するための変数
    private Animator _animator; // アニメーションを制御するための変数

    [SerializeField] private Transform CollPoint; // オブジェクトを保持する位置を示すTransform
    [SerializeField] private float heightOffset = 0.5f; // オブジェクトを積む際の高さオフセット
    private List<GameObject> grabObjects = new List<GameObject>(); // 持っているオブジェクトのリスト
    private List<GameObject> objectsInTrigger = new List<GameObject>(); // トリガー内にあるオブジェクトのリスト


    private int maxPearlCount = 3; // 最大で持てるパールの数
    private int maxBoxCount = 1; // 最大で持てる箱の数

    private Dictionary<GameObject, int> originalLayers = new Dictionary<GameObject, int>(); // オブジェクトの元のレイヤーを記憶する辞書
    private Dictionary<GameObject, string> originalTags = new Dictionary<GameObject, string>(); // オブジェクトの元のタグを記憶する辞書
    public bool IsHoldingObject { get; private set; } // オブジェクトを持っているかどうかを示すプロパティ

    private bool canInteract = true; // インタラクションが可能かどうかを示すフラグ

    private bool isThrowing = false; // 投げているかどうかを示すフラグ

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>(); // PlayerInputコンポーネントを取得
        _animator = GetComponent<Animator>(); // Animatorコンポーネントを取得
    }

    void Update()
    {
        // インタラクションが無効な場合は処理を中断
        if (!canInteract) return;

        // "Have"アクションがトリガーされたときの処理
        if (_playerInput.actions["Have"].triggered)
        {
            GameObject closestObject = GetClosestObject(); // 最も近いオブジェクトを取得
            if (closestObject != null)
            {
                _animator.SetTrigger("Pick"); // "Pick"トリガーをアニメーターに設定
                GrabObject(closestObject); // オブジェクトを掴む
            }
        }

        // "Throw"アクションがトリガーされ、オブジェクトを持っている場合の処理
        if (_playerInput.actions["Throw"].triggered && grabObjects.Count > 0 && !isThrowing)
        {
            isThrowing = true; // 投げる処理中フラグを立てる
            _animator.SetTrigger("Throw"); // アニメーションをトリガー
            StartCoroutine(WaitForThrowAnimation()); // アニメーションが終わるのを待つ
            Debug.Log("Throw");
        }
    }

    // 最も近いオブジェクトを取得するメソッド
    private GameObject GetClosestObject()
    {
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;
        foreach (var obj in objectsInTrigger)
        {
            if (obj != null)
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
        }

        return closestObject;
    }

    // オブジェクトを掴むメソッド
    private void GrabObject(GameObject obj)
    {
        if (!originalLayers.ContainsKey(obj))
        {
            originalLayers[obj] = obj.layer; // 元のレイヤーを記憶
        }

        if (obj.CompareTag("Pearl"))
        {
            if (!originalTags.ContainsKey(obj))
            {
                originalTags[obj] = obj.tag; // 元のタグを記憶
            }
            obj.tag = "HeldObject"; // タグを "HeldObject" に変更
        }

        obj.layer = LayerMask.NameToLayer("HeldObjectLayer"); // レイヤーを "HeldObjectLayer" に変更
        obj.GetComponent<Rigidbody>().isKinematic = true; // Rigidbodyをキネマティックに設定

        IsHoldingObject = true; // オブジェクトを保持していることを示すフラグを立てる

        // コルーチンを開始し、その完了を待ってから RecalculateHeldObjectPositions1 を呼び出す
        StartCoroutine(MoveObjectAndRecalculatePositions(obj, CollPoint, 0.5f));
        grabObjects.Add(obj); // オブジェクトをリストに追加
        _animator.SetBool("Hold", true); // "Hold" フラグをアニメーターに設定
    }

    // MoveObjectWithAnimation と RecalculateHeldObjectPositions1 を連続して実行するコルーチン
    private IEnumerator MoveObjectAndRecalculatePositions(GameObject obj, Transform targetPosition, float duration)
    {
        yield return StartCoroutine(MoveObjectWithAnimation(obj, targetPosition, duration));
        RecalculateHeldObjectPositions1();
    }

    // 持っているオブジェクトの位置を再計算して設定するメソッド
    private void RecalculateHeldObjectPositions1()
    {
        for (int i = 0; i < grabObjects.Count; i++)
        {
            // 各オブジェクトの位置を設定
            grabObjects[i].transform.localPosition = Vector3.up * heightOffset * i;
            grabObjects[i].transform.localRotation = Quaternion.identity; // 回転をリセット
        }
    }



    // オブジェクトを投げるメソッド
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

        // 少し遅れてオブジェクトの位置を下げるためにコルーチンを呼び出す
        StartCoroutine(DelayRecalculateHeldObjectPositions());

        // 持っているオブジェクトがゼロになったら Hold を false に設定
        if (grabObjects.Count == 0)
        {
            _animator.SetBool("Hold", false);
            IsHoldingObject = false;
        }

    }

    // オブジェクトの位置を下げる処理を遅延させるコルーチン
    private IEnumerator DelayRecalculateHeldObjectPositions()
    {
        // 0.5秒待機してからオブジェクトの位置を下げる
        yield return new WaitForSeconds(0.5f);

        RecalculateHeldObjectPositions2(); // オブジェクトの位置を再計算して下げる
    }

    // 持っているオブジェクトの位置を再計算して設定するメソッド
    private void RecalculateHeldObjectPositions2()
    {
        for (int i = 0; i < grabObjects.Count; i++)
        {
            // 各オブジェクトの位置を設定（空いたスペースを埋めるため、一段下げる）
            grabObjects[i].transform.localPosition = Vector3.up * heightOffset * i;
            grabObjects[i].transform.localRotation = Quaternion.identity; // 回転をリセット
        }
    }

    // すべてのパールをドロップするメソッド
    public void DropAllPearls()
    {
        for (int i = grabObjects.Count - 1; i >= 0; i--)
        {
            Debug.Log("aaa");
            GameObject obj = grabObjects[i];
            if (obj.CompareTag("HeldObject"))
            {
                Debug.Log("bbb");
                grabObjects.RemoveAt(i); // リストからオブジェクトを削除
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                obj.transform.SetParent(null); // 親オブジェクトから切り離す
                obj.transform.rotation = Quaternion.identity; // 回転をリセット
                rb.isKinematic = false; // キネマティックを解除

                RestoreOriginalState(obj, rb); // 元のレイヤーとタグに戻す

                _animator.SetBool("Hold", false);
            }
        }

        // 持っているオブジェクトがゼロになったら Hold を false に設定
        if (grabObjects.Count == 0)
        {
            _animator.SetBool("Idel", true); // アニメーションの "Idel" フラグを設定
            _animator.SetBool("Hold", false);
            IsHoldingObject = false; // オブジェクトを保持していないことを示すフラグを下ろす
        }
    }

    // オブジェクトの元のレイヤーとタグに戻す処理
    private void RestoreOriginalState(GameObject obj, Rigidbody rb)
    {
        if (originalLayers.ContainsKey(obj))
        {
            int originalLayer = originalLayers[obj];
            StartCoroutine(ResetLayerAfterDelay(obj, originalLayer, 0.033f)); // レイヤーをリセット
            originalLayers.Remove(obj); // 辞書から削除
        }

        if (originalTags.ContainsKey(obj))
        {
            string originalTag = originalTags[obj];
            StartCoroutine(ResetTagAfterDelay(obj, originalTag, 0.033f)); // タグをリセット
            originalTags.Remove(obj); // 辞書から削除
        }

        StartCoroutine(ResetKinematicAfterDelay(rb, 2.0f)); // キネマティックをリセット
    }

    // オブジェクトをアニメーションで移動させるメソッド
    private IEnumerator MoveObjectWithAnimation(GameObject obj, Transform targetPosition, float duration)
    {
        Vector3 startPos = obj.transform.position;
        float elapsedTime = 0.01f;

        obj.transform.SetParent(CollPoint); // 親オブジェクトに設定

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            obj.transform.position = Vector3.Lerp(startPos, targetPosition.position, t); // 線形補間で移動

            // デバッグ用ログ
            //Debug.Log($"Moving {obj.name} from {startPos} to {targetPosition.position}. Current position: {obj.transform.position}");

            yield return null;
        }

        obj.transform.position = targetPosition.position; // 最終位置に設定

        // デバッグ用ログ
        //Debug.Log($"{obj.name} reached final position: {obj.transform.position}");
    }

    // アニメーションの終了を待つコルーチン
    private IEnumerator WaitForThrowAnimation()
    {
        // アニメーションの長さに応じて待機する（例: 1秒）
        yield return new WaitForSeconds(0.5f); // 必要に応じて調整
        isThrowing = false; // 投げる処理が終わったらフラグをリセット
    }


    // レイヤーを元に戻すコルーチン
    private IEnumerator ResetLayerAfterDelay(GameObject obj, int originalLayer, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.layer = originalLayer;
    }

    // タグを元に戻すコルーチン
    private IEnumerator ResetTagAfterDelay(GameObject obj, string originalTag, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.tag = originalTag;
    }

    // キネマティックをリセットするコルーチン
    private IEnumerator ResetKinematicAfterDelay(Rigidbody rb, float delay)
    {
        yield return new WaitForSeconds(delay);
        //rb.isKinematic = true;
    }

    // オブジェクトがトリガーに入ったときの処理
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pearl") || other.CompareTag("boxPrefab"))
        {
            objectsInTrigger.Add(other.gameObject); // トリガー内オブジェクトリストに追加
        }
    }

    // オブジェクトがトリガーから出たときの処理
    private void OnTriggerExit(Collider other)
    {
        if (objectsInTrigger.Contains(other.gameObject))
        {
            objectsInTrigger.Remove(other.gameObject); // トリガー内オブジェクトリストから削除
        }
    }

    // インタラクションを無効化するメソッド
    public void DisableInteraction()
    {
        canInteract = false;
    }

    // インタラクションを有効化するメソッド
    public void EnableInteraction()
    {
        canInteract = true;
    }
}
