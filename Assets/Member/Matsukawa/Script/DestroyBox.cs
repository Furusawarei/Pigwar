using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class DestroyBox : MonoBehaviour
{

    private int HitCount = 0;

    /// <summary>
    /// Õ“Ë‚µ‚½
    /// </summary>
    /// <param name="collision"></param>
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

        // // Õ“Ë‚µ‚½‘Šè‚ÉPlayerƒ^ƒO‚ª•t‚¢‚Ä‚¢‚é‚Æ‚«
        // if (collision.gameObject.tag == "Pearl")
        // {
        //     // 0.2•bŒã‚ÉÁ‚¦‚é
        //     Destroy(gameObject, 0.2f);
        // }
    }
}
