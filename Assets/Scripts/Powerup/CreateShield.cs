using UnityEngine;
using Player;
using QFSW.MOP2;

public class CreateShield : MonoBehaviour
{
    [SerializeField] private float shieldDuration;
    [SerializeField] private int shieldStrength;
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private ObjectPool poolObtainEffect;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            poolObtainEffect.GetObject(transform.position);
            PlayerManager.Instance.CreateShield(shieldDuration,shieldStrength);
            _pool.Release(gameObject);
        }
    }
}
