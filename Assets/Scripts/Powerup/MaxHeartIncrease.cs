using UnityEngine;
using Player;
using QFSW.MOP2;

public class MaxHeartIncrease : MonoBehaviour
{
    [SerializeField] private int increaseAmount;
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private ObjectPool poolObtainEffect;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            poolObtainEffect.GetObject(transform.position);
            PlayerManager.Instance.IncreaseMaxHealth(increaseAmount);
            
            _pool.Release(gameObject);
        }
    }
}
