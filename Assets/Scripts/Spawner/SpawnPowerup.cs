using System;
using System.Collections;
using Player;
using UnityEngine;
using QFSW.MOP2;
using Random = UnityEngine.Random;

public class SpawnPowerup : MonoBehaviour
{
    [SerializeField] private ObjectPool healthPotionPool;
    [SerializeField] private ObjectPool heartPool;
    [SerializeField] private ObjectPool shieldPool;
    
    public float randomSpawnTimeMin;
    public float randomSpawnTimeMax;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    private void Update()
    {
        if (!PlayerManager.Instance._gameStart && PlayerManager.Instance.isDead)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(randomSpawnTimeMin, randomSpawnTimeMax));
        int randomSpawn = Random.Range(1, 4);
        if (randomSpawn == 1)
        {
            healthPotionPool.GetObject();
        }
        else if (randomSpawn == 2)
        {
            heartPool.GetObject();
        }else
        {
            shieldPool.GetObject();
        }

        StartCoroutine(Spawn());
    }
}
