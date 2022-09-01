using System;
using Player;
using UnityEngine;

public class Homming : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private string tag = "Player";
    [SerializeField] private float focusDistance = 5;

    private Enemy enemy;
    private Vector2 playerDirection;
    private bool playerPosTaken;

    private Transform target;

    private void OnEnable()
    {
        playerPosTaken = false;
    }

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = GameObject.FindGameObjectWithTag(tag).transform;
    }

    void Update()
    {
        if (!PlayerManager.Instance.isDead)
        {
            float distance = Vector2.Distance(transform.position, target.position);
            float step = speed * Time.deltaTime;

            if (distance > focusDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, step);
            }

            else
            {
                if (!playerPosTaken)
                {
                    playerDirection = target.position;
                    playerPosTaken = true;
                }
            
                transform.Translate(playerDirection.normalized * step,Space.Self);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(tag) && !PlayerManager.Instance.isDead)
        {
            enemy.DestroyEnemy();
        }
    }
}
