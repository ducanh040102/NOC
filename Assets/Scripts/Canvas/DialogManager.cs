using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public TextMeshProUGUI dialogText;
    public Animator canvasAnim;
    public AudioSource sfxPlayer;
    public AudioClip clickButtonSfx;
    
    private Queue<string> _sentences;
    private bool firstIn;

    public static DialogManager Instance { get; private set; }
        
    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
    }

    private void Start()
    {
        firstIn = true;
    }

    public void PlayStory(Dialog dialog)
    {
        _sentences = new Queue<string>();
        
        foreach (string sentence in dialog.sentences)
        {
            _sentences.Enqueue(sentence);
        }

        StartCoroutine(WaitForAnimation());

    }

    public void DisplayNextSentence()
    {
        if (_sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        string sentence = _sentences.Dequeue();
        dialogText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        if (!firstIn)
        {
            canvasAnim.SetTrigger("FadeInOut");
        }

        firstIn = false;
        sfxPlayer.PlayOneShot(clickButtonSfx);

    }

    public void EndDialog()
    {
        canvasAnim.SetTrigger("FadeOut");
    }
    
    
    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(1);
        DisplayNextSentence();
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }
}
