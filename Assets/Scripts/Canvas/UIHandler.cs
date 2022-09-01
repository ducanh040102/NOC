using System;
using System.Collections;
using UnityEngine;
using Player;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private AudioClip _summonSfx;
    [SerializeField] private AudioClip _restartGameSfx;
    [SerializeField] private AudioClip _backToMenuSfx;
    [SerializeField] private AudioClip _pauseSfx;
    [SerializeField] private AudioSource _sfxPlayer;
    [SerializeField] private AudioSource _playerSfx;
    [SerializeField] private AudioSource _bgm;
    [SerializeField] private float sfxClickDuration = 1;

    private bool gamePaused;

    private void Start()
    {
        gamePaused = false;
    }

    private void Update()
    {
        if (PlayerManager.Instance.isDead)
        {
            SceneFadeOut();
        }
    }

    private void SceneFadeOut()
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        animator.SetTrigger("Gameover");
    }

    public void FadeInComplete()
    {
        _sfxPlayer.PlayOneShot(_summonSfx);
    }
    
    public void RestartButton()
    {
        _sfxPlayer.PlayOneShot(_restartGameSfx);
        Time.timeScale = 0.5f;
        StartCoroutine(Restart());
    }

    public void ReturnToMenu()
    {
        _sfxPlayer.PlayOneShot(_backToMenuSfx);
        Time.timeScale = 0.5f;
        StartCoroutine(ToMenu());
    }

    public void Pause()
    {
        _sfxPlayer.PlayOneShot(_pauseSfx);
        _playerSfx.Pause();
        _bgm.Pause();
        gamePaused = true;
        pausePanel.SetActive(gamePaused);
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        _sfxPlayer.PlayOneShot(_pauseSfx);
        _playerSfx.Play();
        _bgm.Play();
        gamePaused = false;
        pausePanel.SetActive(gamePaused);
        Time.timeScale = 1;
        
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(sfxClickDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    IEnumerator ToMenu()
    {
        yield return new WaitForSeconds(sfxClickDuration);
        SceneManager.LoadScene("Menu");
    }
}
