using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class DestroyBox : MonoBehaviour
{
    private int HitCount = 0;

    /// <summary>
    /// 衝突時に呼び出されるメソッド
    /// </summary>
    /// <param name="collision">衝突したオブジェクトの情報</param>
    void OnCollisionEnter(Collision collision)
    {
        CP_ThrowableObject throwable = collision.gameObject.GetComponent<CP_ThrowableObject>();
        if (throwable != null)
        {
            if (throwable.isThrown)
            {
                HitCount++;
                if (HitCount >= 2)
                {
                    Destroy(gameObject, 0.2f);
                }
            }
        }

        // プレイヤーが持っているオブジェクト（Pearl）に衝突したとき
        // if (collision.gameObject.tag == "Pearl")
        // {
        //     // 0.2秒後に自分自身を破壊する
        //     Destroy(gameObject, 0.2f);
        // }
    }
}
