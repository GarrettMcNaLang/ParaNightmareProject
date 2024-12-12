using System;
using UnityEngine;

public class ColliderComponent : MonoBehaviour
{
    public event Action<Collider> TriggerEnter;

    public event Action<Collider> TriggerExit;

    public event Action<Collider> TriggerStay;
    
    void OnTriggerEnter(Collider other)
    {
        TriggerEnter?.Invoke(other);
    }

    private void OnTriggerStay(Collider other)
    {
          TriggerStay?.Invoke(other);
    }

    void OnTriggerExit(Collider other)
    {
        TriggerExit?.Invoke(other);
    }
}
