using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class DestroyBox : MonoBehaviour
{
    private int HitCount = 0;

    /// <summary>
    /// �Փˎ��ɌĂяo����郁�\�b�h
    /// </summary>
    /// <param name="collision">�Փ˂����I�u�W�F�N�g�̏��</param>
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

        // �v���C���[�������Ă���I�u�W�F�N�g�iPearl�j�ɏՓ˂����Ƃ�
        // if (collision.gameObject.tag == "Pearl")
        // {
        //     // 0.2�b��Ɏ������g��j�󂷂�
        //     Destroy(gameObject, 0.2f);
        // }
    }
}
