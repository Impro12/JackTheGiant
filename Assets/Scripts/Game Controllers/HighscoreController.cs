using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HighscoreController : MonoBehaviour {

    [SerializeField]
    private Text scoreText, coinText;



	void Start ()
    {
        SetScoreBasedOnDifficulty();	
	}

    void SetScore(int score, int coinScore)
    {
        scoreText.text = score.ToString();
        coinText.text = coinScore.ToString();
    }

    public void GoBackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void SetScoreBasedOnDifficulty()
    {
        if (GamePreferences.GetEasyDifficultyState() == 0)
        {
            SetScore(GamePreferences.GetEasyDifficultyHighscore(), GamePreferences.GetEasyDifficultyCoinScore());
        }

        if (GamePreferences.GetMediumDifficultyState() == 0)
        {
            SetScore(GamePreferences.GetMediumDifficultyHighscore(), GamePreferences.GetMediumDifficultyCoinScore());
        }

        if (GamePreferences.GetHardDifficultyState() == 0)
        {
            SetScore(GamePreferences.GetHardDifficultyHighscore(), GamePreferences.GetHardDifficultyCoinScore());
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
