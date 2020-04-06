using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreSystem : MonoBehaviour
{
    /// <summary>
    /// Basic scoring script
    /// To use, refrence this object via GameObject.FindObjectOfType<ScoreSystem>(); and call AddToScore as needed.
    /// Directly modifying score and highscore will accomplish little.
    /// </summary>
    
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text highScoreText;

    [SerializeField] Color scoreUpColor = Color.green;
    [SerializeField] Color scoreDownColor = Color.red;
    [SerializeField] Color newHighScoreColor = Color.yellow;
    [SerializeField] Color defaultColor = Color.white;

    public int score = 0;
    public int highScore = 0;

    private void Start()
    {
        //set up highscore and score
        highScore = PlayerPrefs.GetInt("HighScore");
        UpdateHighScore();
        AddToScore(0); //this merely updates it without modifying it
    }

    private void Update()
    {
       if(score > 15)
        {
            scoreText.text = "You Win!";
            highScoreText.text = "Nice Job Idiot!";
        }
    }

    public void AddToScore(int amount)
    {
        //Can be updated negatively or positively
        score += amount;

        //flash based on up or down. On a change of 0, no flash will occur
        if(amount > 0)
        {
            StartCoroutine(Flash(scoreText, scoreUpColor, 0.25f));
        }else if (amount < 0)
        {
            StartCoroutine(Flash(scoreText, scoreDownColor, 0.25f));
        }

        //clamp score
        if (score <= 0)
        {
            score = 0;
        }

        //update text
        scoreText.text = "Score: " + score.ToString();
        //update hs
        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        //compare score and highscore
        if (score > highScore)
        {
            //this implies that the higher the number, the better.
            highScore = score;
            //save high score
            PlayerPrefs.SetInt("HighScore", highScore);
            //flash!
            StartCoroutine(Flash(highScoreText, newHighScoreColor, 0.25f));
        }
        //update text
        highScoreText.text = "High score: " + highScore.ToString();
    }

    //simple juice.
    IEnumerator Flash(TMP_Text target, Color flashColor, float time)
    {
        target.color = flashColor;
        yield return new WaitForSecondsRealtime(time);
        target.color = defaultColor;
    }
}
