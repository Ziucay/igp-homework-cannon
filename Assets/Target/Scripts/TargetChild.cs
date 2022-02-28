using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetChild : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            transform.root.GetComponent<Target>().CollisionDetected(other);
        }
    }
}
