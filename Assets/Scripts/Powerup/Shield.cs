using UnityEngine;
using Player;

public class Shield : MonoBehaviour
{
    [SerializeField] private Animator shieldAnimator;
    
    void Update()
    {
        shieldAnimator.SetBool("have_shield",PlayerManager.Instance.haveShield);
    }
}
