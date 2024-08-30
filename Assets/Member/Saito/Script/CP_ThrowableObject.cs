using UnityEngine;

public class CP_ThrowableObject : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem hit_effect; //当たった時のエフェクト
    public bool isThrown = false;
    public AudioClip hitSound; // 当たった時のサウンド
    public AudioSource audioSource;

    void OnCollisionEnter(Collision collision)
    {
        // プレイヤーに当たった場合の処理
        if (isThrown && collision.gameObject.CompareTag("Player"))
        {
            // SEを再生する
            audioSource.PlayOneShot(hitSound);

            // パーティクルシステムのインスタンスを生成する。
            ParticleSystem newParticle = Instantiate(hit_effect);
            // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
            newParticle.transform.position = this.transform.position;
            // パーティクルを発生させる。
            newParticle.Play();
            // インスタンス化したパーティクルシステムのGameObjectを1秒後に削除する。
            Destroy(newParticle.gameObject, 1.0f);

            // 衝突したプレイヤーのTouchObjectコンポーネントを取得
            TouchObject touchObject = collision.gameObject.GetComponent<TouchObject>();
            if (touchObject != null)
            {
                // そのプレイヤーが持っているパールをすべて落とす
                touchObject.DropAllPearls();
            }

            isThrown = false; // 衝突後はフラグをリセット
        }
        // 地面に当たった場合の処理
        else if (isThrown && collision.gameObject.CompareTag("Ground"))
        {
            isThrown = false; // 地面に触れたらフラグをリセット
        }
    }
}
