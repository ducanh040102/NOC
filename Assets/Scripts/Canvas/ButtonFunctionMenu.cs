using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctionMenu : MonoBehaviour
{
    [SerializeField] private Animator menuAnim;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] public AudioClip _start;
    
    public void StartGame()
    {
        menuAnim.SetTrigger("GameStart");
        _audioSource.PlayOneShot(_start);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Storytell");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ToInstruction()
    {
        SceneManager.LoadScene("Instruction");
    }
}
