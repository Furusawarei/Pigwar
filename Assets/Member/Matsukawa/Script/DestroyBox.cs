using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class DestroyBox : MonoBehaviour
{
    /// <summary>
    /// 衝突した時
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        CP_ThrowableObject throwable = collision.gameObject.GetComponent<CP_ThrowableObject>();
        if(throwable != null)
        {
            if(throwable.isThrown)
            {
                Destroy(gameObject, 0.2f);
            }
        }
        
        // // 衝突した相手にPlayerタグが付いているとき
        // if (collision.gameObject.tag == "Pearl")
        // {
        //     // 0.2秒後に消える
        //     Destroy(gameObject, 0.2f);
        // }
    }
}
