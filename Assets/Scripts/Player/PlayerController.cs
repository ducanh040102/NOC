using System.Collections;
using DigitalRubyShared;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _hitbox;
    
    [Header("Properties")]
    [SerializeField] private float _jumpForce = 7;
    [SerializeField] private float _crouchDuration = 0.2f;
    [SerializeField] private float _attackDelay = 0.1f;
    [SerializeField] private float _attackTime = 0.5f;
    
    [Header("SFX")]
    
    [SerializeField] private AudioSource _sfxPlayer;
    [SerializeField] private AudioSource _bgPlayer;
    [SerializeField] private AudioClip _attackSfx;
    [SerializeField] private AudioClip _crouchSfx;
    [SerializeField] private AudioClip _hurtSfx;
    [SerializeField] private AudioClip _jumpSfx;
    [SerializeField] private AudioClip _landingSfx;
    [SerializeField] private AudioClip _downSfx;
    
    [SerializeField] private AudioClip[] _bgmSfx;
    
    
    [Header("VFX")]
    [SerializeField] private ParticleSystem _runningVfx;
    [SerializeField] private ParticleSystem _crouchVfx;
    [SerializeField] private ParticleSystem _landingVfx;
    
    
    private CapsuleCollider2D _topCollider;
    
    private Animator _animator;
    private Rigidbody2D _rigidbody;

    private TapGestureRecognizer _tapGesture;
    private SwipeGestureRecognizer _swipeUpGesture;
    private SwipeGestureRecognizer _swipeDownGesture;

    private bool _isGrounded;
    private bool _isCrouching;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _topCollider = GetComponent<CapsuleCollider2D>();
        
        CreateTapGesture();
        CreateSwipeUpGesture();
        CreateSwipeDownGesture();
        
        _tapGesture.AllowSimultaneousExecution(_swipeUpGesture);
        _swipeUpGesture.AllowSimultaneousExecution(_tapGesture);
        
        _isGrounded = false;
        _isCrouching = false;
        PlayerManager.Instance._gameStart = false;
        
        _animator.Play("Landing-anim");
        
        int randomBGM = Random.Range(0, _bgmSfx.Length);
        _bgPlayer.clip = _bgmSfx[randomBGM];

    }

    private void Update()
    {
        _animator.SetBool("is_grounded", _isGrounded);
        _animator.SetBool("is_crouching", _isCrouching);
    }

    void CreateTapGesture()
    {
        _tapGesture = new TapGestureRecognizer();
        _tapGesture.StateUpdated += TapGestureOnStateUpdated;
        FingersScript.Instance.AddGesture(_tapGesture);
    }

    private void TapGestureOnStateUpdated(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended && PlayerManager.Instance._gameStart)
        {
            _animator.Play("Attack-anim");
            StartCoroutine(StartAttacking());
        }
    }

    void CreateSwipeUpGesture()
    {
        _swipeUpGesture = new SwipeGestureRecognizer();
        _swipeUpGesture.Direction = SwipeGestureRecognizerDirection.Up;
        _swipeUpGesture.StateUpdated += SwipeUpGestureOnStateUpdated;
        _swipeUpGesture.DirectionThreshold = 2.5f;
        FingersScript.Instance.AddGesture(_swipeUpGesture);

    }

    private void SwipeUpGestureOnStateUpdated(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended && _isGrounded && PlayerManager.Instance._gameStart)
        {
            Jump();
        }
    }
    
    void CreateSwipeDownGesture()
    {
        _swipeDownGesture = new SwipeGestureRecognizer();
        _swipeDownGesture.Direction = SwipeGestureRecognizerDirection.Down;
        _swipeDownGesture.StateUpdated += SwipeDownGestureOnStateUpdated;
        _swipeDownGesture.DirectionThreshold = 2.5f;
        FingersScript.Instance.AddGesture(_swipeDownGesture);

    }

    private void SwipeDownGestureOnStateUpdated(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended && PlayerManager.Instance._gameStart)
        {
            Crouch();
        }
    }

    private void Attack()
    {
        _hitbox.SetActive(true);
        _sfxPlayer.PlayOneShot(_attackSfx);
        StartCoroutine(StopAttacking());
    }
    
    private void Jump()
    {
        _animator.Play("Jump-anim");
   
        _sfxPlayer.Stop();
        _sfxPlayer.PlayOneShot(_jumpSfx);

        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _isGrounded = false;
    }

    private void Crouch()
    {
        if (!_isCrouching && _isGrounded)
        {
            _isCrouching = true;
            _animator.Play("Crouch-anim");
            _topCollider.enabled = false;
            StartCoroutine(StopCrouching());
            
            _sfxPlayer.Stop();
            _runningVfx.Stop();
            _crouchVfx.Play();
            _sfxPlayer.PlayOneShot(_crouchSfx);
        }
        else if (!_isGrounded)
        {
            _rigidbody.AddForce(Vector2.up * -_jumpForce * 1.5f, ForceMode2D.Impulse);
        }
    }
    
    public void Hurt()
    {
        _animator.Play("Hurt-anim");
        _sfxPlayer.PlayOneShot(_hurtSfx);
    }

    public void Dead()
    {
        _animator.SetBool("gameover",true);
        Hurt();
        _crouchVfx.Stop();
        _runningVfx.Stop();
        _landingVfx.Stop();
        _sfxPlayer.Stop();
        _bgPlayer.Stop();
        _sfxPlayer.clip = null;
        _sfxPlayer.PlayOneShot(_downSfx);

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") && !PlayerManager.Instance.isDead)
        {
            _isGrounded = true;
            _animator.Play("Run-anim");
            
            if(PlayerManager.Instance._gameStart)
                _sfxPlayer.PlayOneShot(_landingSfx);
            
            _sfxPlayer.Play();
            _landingVfx.Play();
            if (!PlayerManager.Instance._gameStart && !PlayerManager.Instance.isDead)
            {
                
                _bgPlayer.Play();
            }
            PlayerManager.Instance._gameStart = true;
        }
    }

   
    

    IEnumerator StopCrouching()
    {
        yield return new WaitForSeconds(_crouchDuration);
        _isCrouching = false;
        _topCollider.enabled = true;
        
        _sfxPlayer.Play();
        _runningVfx.Play();
        
        _crouchVfx.Stop();
    }

    IEnumerator StartAttacking()
    {
        yield return new WaitForSeconds(_attackDelay);
        Attack();
    
    }
    
    IEnumerator StopAttacking()
    {
        yield return new WaitForSeconds(_attackTime);
        _hitbox.SetActive(false);

    }
}
}


