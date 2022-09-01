using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneBoothRing : MonoBehaviour
{
    [SerializeField] private AudioClip _phoneRingSfx;
    
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.PlayOneShot(_phoneRingSfx);
    }

    
    void Update()
    {
        if (transform.position.x < -13)
        {
            Destroy(gameObject);
        }
    }
}
