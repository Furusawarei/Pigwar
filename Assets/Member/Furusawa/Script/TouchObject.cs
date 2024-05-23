using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : MonoBehaviour
{
   private float rayDistance;

   void Start()
   {
    rayDistance = 1.0f;
   }

    void Update()
    {
      Ray ray = new Ray(transform.position,transform.forward);
      Debug.DrawRay(transform.position,transform.forward * rayDistance, Color.red);
    }

    void OnTriggerEnter(Collider col)
    {
   
    }

    
}
