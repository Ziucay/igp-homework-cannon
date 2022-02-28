using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static Dictionary<int, Transform> targets = new Dictionary<int, Transform>();
    private void Awake()
    {
        Target.OnInstantiate += AddTarget;
        Target.OnTargetDestroy += RemoveTarget;
    }

    private void OnDestroy()
    {
        Target.OnInstantiate -= AddTarget;
        Target.OnTargetDestroy -= RemoveTarget;
    }

    private void AddTarget(Transform target, int id)
    {
        Debug.Log("Target added");
        targets.Add(id, target);
    }

    private void RemoveTarget(Transform target, int id)
    {
        Debug.Log("Target removed");
        targets.Remove(id);
    }
}
