using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private Animator _animator; 
    [SerializeField] private float riseAfterDistanceWithPlayer;
    
    private Vector2 player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform.position;
    }
    
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player);

        if (distance <= riseAfterDistanceWithPlayer)
        {
            _animator.SetBool("rise",true);
        }
    }
}
