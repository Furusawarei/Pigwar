using UnityEngine;

public class MeshColliderSetup : MonoBehaviour
{
    void Start()
    {
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.convex = true;  // 動的オブジェクトの場合

        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = true;        // 必要に応じて設定
    }
}
