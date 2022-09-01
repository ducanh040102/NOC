using System.Collections;
using Player;
using UnityEngine;
using QFSW.MOP2;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private ObjectPool hellPool;
    [SerializeField] private ObjectPool ghostPool;
    [SerializeField] private ObjectPool skeletonPool;

    [SerializeField] private AudioSource _sfxPlayer;
    [SerializeField] private AudioClip hellAudio;
    

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
        int randomSpawn = Random.Range(1, 4);
        if (randomSpawn == 1)
        {
            hellPool.GetObject();
            _sfxPlayer.PlayOneShot(hellAudio);
        }
        else if (randomSpawn == 2)
        {
            ghostPool.GetObject();
        }else
        {
            skeletonPool.GetObject();
        }

        StartCoroutine(Spawn());
    }
}
