using System.Collections;
using UnityEngine;
using QFSW.MOP2;

public class Destroyed : MonoBehaviour
{
    [SerializeField] private AudioSource sfxPlayer;
    [SerializeField] private AudioClip destroySfx;
    [SerializeField] private ObjectPool _pool;

    private void OnEnable()
    {
        sfxPlayer.PlayOneShot(destroySfx);
        StartCoroutine(SelfDestroy());
    }

    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(1.5f);
        _pool.Release(gameObject);
    }
}
