using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class DestroyBox : MonoBehaviour
{
    /// <summary>
    /// �Փ˂�����
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
        
        // // �Փ˂��������Player�^�O���t���Ă���Ƃ�
        // if (collision.gameObject.tag == "Pearl")
        // {
        //     // 0.2�b��ɏ�����
        //     Destroy(gameObject, 0.2f);
        // }
    }
}
