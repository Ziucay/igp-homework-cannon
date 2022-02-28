using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    public delegate void TargetCallback(Transform sender, int id);

    public static event TargetCallback OnInstantiate;
    public static event TargetCallback OnTargetDestroy;
    
    private Rigidbody[] _rigidbodies;
    public bool IsDead = false;
    private void Start()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in _rigidbodies)
        {
            rb.isKinematic = true;
        }
        
        if (OnInstantiate != null)
        {
            OnInstantiate(gameObject.transform, gameObject.GetInstanceID());
        }
    }

    private void OnDestroy()
    {
        if (OnTargetDestroy != null)
        {
            OnTargetDestroy(gameObject.transform, gameObject.GetInstanceID());
        }
    }

    private void Ragdoll()
    {
        foreach (Rigidbody rb in _rigidbodies)
        {
            rb.isKinematic = false;
        }
    }

    public void CollisionDetected(Collision other)
    {
        if (!IsDead)
        {
            IsDead = true;
            transform.gameObject.tag = "Ragdoll";
            Destroy(other.gameObject);
            if (OnTargetDestroy != null)
            {
                OnTargetDestroy(gameObject.transform, gameObject.GetInstanceID());
            }
            Ragdoll();    
        }
    } 
}
