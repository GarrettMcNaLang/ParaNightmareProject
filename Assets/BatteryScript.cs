using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class BatteryScript : MonoBehaviour
{

    private ObjectPool<BatteryScript> Pool;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag("Player"))
    //    {
    //        Debug.Log("Battery Acquired");
    //        //Destroy(gameObject);
    //        Pool.Release(this);
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.CompareTag("PlayerCollider"))
    //    {
            
    //    }
    //}

    public void ReturnBattery()
    {
        Debug.Log("Battery Acquired");
        Pool.Release(this);
    }
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void SetPool(ObjectPool<BatteryScript> pool)
    {
        Pool = pool;
    }
  
}
