using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeBG : MonoBehaviour
{
    public Image bg;

    public Sprite[] imageToChange;

    public int index = 0;
    
    public void ChangeImage()
    {
        bg.sprite = imageToChange[index];

        if (index + 1 <= imageToChange.Length)
        {
            index += 1;
        }
        
    }

    public void EndScene()
    {
        SceneManager.LoadScene("Game");
    }
    
}
