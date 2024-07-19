using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    public bool isThrown = false;
    public AudioClip hitSound; // 当たった時のサウンド
     public AudioSource audioSource;

    void OnCollisionEnter(Collision collision)
    {
        // プレイヤーに当たった場合の処理
        if (isThrown && collision.gameObject.CompareTag("body"))
        {
            // SEを再生する
            audioSource.PlayOneShot(hitSound);
            isThrown = false; // 衝突後はフラグをリセット
        }
        // 地面に当たった場合の処理
        else if (isThrown && collision.gameObject.CompareTag("Ground"))
        {
            isThrown = false; // 地面に触れたらフラグをリセット
        }
    }
}
