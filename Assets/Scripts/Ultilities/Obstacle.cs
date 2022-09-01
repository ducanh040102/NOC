using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class Obstacle : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<CapsuleCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerManager.Instance.DecreaseHealth(1);
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }
}
