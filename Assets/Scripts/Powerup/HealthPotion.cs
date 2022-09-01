using UnityEngine;
using Player;
using QFSW.MOP2;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private int healthAmount;
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private ObjectPool poolObtainEffect;
    

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            poolObtainEffect.GetObject(transform.position);
            PlayerManager.Instance.IncreaseHealth(healthAmount);
            _pool.Release(gameObject);
        }
    }
}
