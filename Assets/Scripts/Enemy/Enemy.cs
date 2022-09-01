using Player;
using UnityEngine;
using QFSW.MOP2;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ObjectPool pool;
    [SerializeField] private ObjectPool poolDestroyEffect;
    [SerializeField] private int damage;
    [SerializeField] private int scoreWhenDefeat;

    private void OnEnable()
    {
        GetComponent<CapsuleCollider2D>().enabled = true;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            PlayerManager.Instance.DecreaseHealth(damage);
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }

    public void DestroyEnemy()
    {
        poolDestroyEffect.GetObject(transform.position);
        PlayerManager.Instance.score += scoreWhenDefeat;
        GetComponent<CapsuleCollider2D>().enabled = false;
        pool.Release(gameObject);
    }
}
