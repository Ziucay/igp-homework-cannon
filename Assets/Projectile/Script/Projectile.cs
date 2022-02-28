using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(DestroyOnTimeout());
    }

    IEnumerator DestroyOnTimeout()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
