using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(("Enemy")))
        {
            other.GetComponent<Enemy>().DestroyEnemy();
        }  
    }
}
