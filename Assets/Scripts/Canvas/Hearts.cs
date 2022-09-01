using UnityEngine;
using UnityEngine.UI;
using Player;

public class Hearts : MonoBehaviour
{
    public Image[] hearts;
    public Sprite emptyHeart;
    public Sprite fullHeart;
    
    
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {

            if (i < PlayerManager.Instance.currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            
            if (i < PlayerManager.Instance.maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
