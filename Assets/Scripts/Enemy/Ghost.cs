using Player;
using System.Collections;
using UnityEngine;
using QFSW.MOP2;

public class Ghost : MonoBehaviour
{
    [SerializeField] private ObjectPool fireBallPool;
    [SerializeField] private AudioSource _sfxPlayer;
    [SerializeField] private AudioClip _fireBallSfx;
    [SerializeField] private float fireRate;
    [SerializeField] private float stopFireAfterDistanceWithPlayer;

    private Vector2 player;

    private void OnEnable()
    {
        StartCoroutine(FireFireball());

        player = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    private void Update()
    {
        
        float distance = Vector2.Distance(transform.position, player);

        if (distance <= stopFireAfterDistanceWithPlayer || PlayerManager.Instance.isDead)
        {
            StopAllCoroutines();
        }
    }

    IEnumerator FireFireball()
    {
        yield return new WaitForSeconds(fireRate);
        fireBallPool.GetObject(transform.position);
        _sfxPlayer.PlayOneShot(_fireBallSfx);
        StartCoroutine(FireFireball());
    }
}
