using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.MOP2;

public class AutoReturnToPool : MonoBehaviour
{
    [SerializeField] private ObjectPool pool;
    [SerializeField] private float minusThanX = -13;
    
    void Update()
    {
        if (transform.position.x < minusThanX)
        {
            pool.Release(gameObject);
        }
    }
}
