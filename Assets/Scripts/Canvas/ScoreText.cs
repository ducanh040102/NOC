using UnityEngine;
using TMPro;
using Player;

public class ScoreText : MonoBehaviour
{
    public int scoreLength = 9;

    public string scoreString;
    public TextMeshProUGUI thisText;
    public TextMeshProUGUI gameoverScoreText;

    private string scoreText;
    private void Update()
    {
        scoreText = "";
        scoreString = (PlayerManager.Instance.score).ToString();
        int numZeros = scoreLength - scoreString.Length;
        
        
        for(int i = 0; i < numZeros; i++){
            scoreText += "0";
        }

        scoreText += scoreString;
        
        thisText.text = scoreText;

        if (PlayerManager.Instance.isDead)
        {
            gameoverScoreText.text = scoreText;
        }
    }
}
