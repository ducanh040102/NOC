using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float flySpeed;
    
    void Update()
    {
        if(PlayerManager.Instance._gameStart)
            transform.Translate(  Time.deltaTime * Vector2.left *  flySpeed);
    }
}
