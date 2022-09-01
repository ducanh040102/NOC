using System.Collections;
using UnityEngine;
using QFSW.MOP2;
using Player;

public class SpawnObstacle : MonoBehaviour
{
    [SerializeField] private ObjectPool bushPool;
    [SerializeField] private ObjectPool stonePool;
    [SerializeField] private ObjectPool treePool;
    [SerializeField] private ObjectPool tombPool;
    
    public float randomSpawnTimeMin;
    public float randomSpawnTimeMax;
    
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
    
    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(Random.Range(randomSpawnTimeMin / PlayerManager.Instance.hardMultiply, randomSpawnTimeMax / PlayerManager.Instance.hardMultiply));
        int randomSpawn = Random.Range(1, 5);
        if (randomSpawn == 1)
        {
            bushPool.GetObject();
        }
        else if (randomSpawn == 2)
        {
            tombPool.GetObject();
        }
        else if (randomSpawn == 3)
        {
            treePool.GetObject();
        }
        else
        {
            stonePool.GetObject();
        }

        StartCoroutine(Spawn());
    }
}
