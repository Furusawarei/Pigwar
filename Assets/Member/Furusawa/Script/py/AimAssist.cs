using UnityEngine;

public class AimAssist : MonoBehaviour
{
    public float rayLength = 10f;       // Rayの長さ
    public Transform rayOrigin;         // Rayを飛ばす開始位置
    public float rotationSpeed = 5f;    // 回転速度
    public string targetTag = "Player"; // タグを設定する（例: "Player"）

    private Transform targetPlayer;     // 追尾するプレイヤー

    void Update()
    {
        AimAssistRay();
    }

    void AimAssistRay()
    {
        // Rayを飛ばす
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, rayLength))
        {
            // タグが指定されたプレイヤーかを確認
            if (hit.transform.CompareTag(targetTag))
            {
                targetPlayer = hit.transform; // ヒットしたプレイヤーをターゲットとして設定
            }
        }
        else
        {
            targetPlayer = null; // プレイヤーにヒットしていなければターゲットを解除
        }

        // ターゲットが存在する場合、その方向に回転させる
        if (targetPlayer != null)
        {
            // ターゲットの位置を取得し、Y軸を中心に回転
            Vector3 directionToTarget = targetPlayer.position - transform.position;
            directionToTarget.y = 0; // Y軸の回転のみを行うため、Y成分を無視する

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    // デバッグ用にRayを視覚化
    private void OnDrawGizmos()
    {
        if (rayOrigin != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(rayOrigin.position, rayOrigin.forward * rayLength);
        }
    }
}
