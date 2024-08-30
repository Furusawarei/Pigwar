using UnityEngine;

public class StartParticle : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem hit_effect;
    private void OnCollisionEnter(Collision collision)
    {
        // 当たった相手が"Player"タグを持っていたら
        if (collision.gameObject.tag == "Player")
        {
            // パーティクルシステムのインスタンスを生成する。
            ParticleSystem newParticle = Instantiate(hit_effect);
            // パーティクルの発生場所をこのスクリプトをアタッチしているGameObjectの場所にする。
            newParticle.transform.position = this.transform.position;
            // パーティクルを発生させる。
            newParticle.Play();
            // インスタンス化したパーティクルシステムのGameObjectを5秒後に削除する。(任意)
            // ※第一引数をnewParticleだけにするとコンポーネントしか削除されない。
            Destroy(newParticle.gameObject, 5.0f);
        }
    }
}