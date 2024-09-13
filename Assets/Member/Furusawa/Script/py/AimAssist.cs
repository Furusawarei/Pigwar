using UnityEngine;

public class AimAssist : MonoBehaviour
{
    public float rayDistance = 2f; // Rayを飛ばす距離
    public float followSpeed = 1f; // 追尾速度
    public LayerMask targetLayer;  // ターゲットのレイヤー（プレイヤーなど）
    
    private Transform target; // 追尾するターゲット

    void Update()
    {
        // Rayの開始位置はこのオブジェクトの位置
        Vector3 rayOrigin = transform.position;

        // Rayを正面に飛ばす
        Ray ray = new Ray(rayOrigin, transform.forward);
        RaycastHit hit;

        // Rayが何かに当たった場合
        if (Physics.Raycast(ray, out hit, rayDistance, targetLayer))
        {
            // 当たったオブジェクトが"Player"タグを持っているかを確認
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Aaa");
                // ターゲットを設定
                target = hit.transform;
            }
        }
        else
        {
            // Rayが何にも当たっていない場合はターゲットをリセット
            target = null;
        }

        // ターゲットがいる場合は追尾処理を行う
        if (target != null)
        {
            FollowTarget();
        }
    }

    // ターゲットを追尾する処理
    private void FollowTarget()
    {
        // このオブジェクトをターゲットの位置へ1fの速度で追尾
        transform.position = Vector3.MoveTowards(transform.position, target.position, followSpeed * Time.deltaTime);
    }

    // デバッグ用にRayをシーンビューに表示
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }
}
