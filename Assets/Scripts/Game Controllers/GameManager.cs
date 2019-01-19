using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [HideInInspector]
    public bool gameStartedFromMainMenu, gameRestartedAfterPlayerDied;

    [HideInInspector]
    public int score, coinScore, lifeScore;

    void Awake()
    {
        MakeSingleton();
    }

    void Start()
    {
        InitializeVariables();
    }

    void OnEnable()

    {
        SceneManager.sceneLoaded += LevelFinishedLoading;
    }


    void OnDisable()
    {
        SceneManager.sceneLoaded -= LevelFinishedLoading;
    }

   

    void LevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene")
        {
            if (gameStartedFromMainMenu)
            {
                score = 0;
                coinScore = 0;
                lifeScore = 2;
            }
            else if (gameRestartedAfterPlayerDied)
            {
                score = score;
                coinScore = coinScore;
                lifeScore = lifeScore;
            }
        }
    }
    void OnLevelWasLoaded()
    {

        if (gameStartedFromMainMenu) { 

        PlayerScore.score = 0;

        PlayerScore.coinScore = 0;

        GameManager.instance.lifeScore = 2;

        GameplayController.instance.SetScore(0);

        GameplayController.instance.SetCoinScore(0);

        GameplayController.instance.SetLifeScore(2);

        Debug.Log("working");

    }
    else if(gameRestartedAfterPlayerDied)
        {

            PlayerScore.score = score;

            PlayerScore.coinScore = coinScore;

            GameManager.instance.lifeScore = lifeScore;

            GameplayController.instance.SetScore(score);

            GameplayController.instance.SetCoinScore(coinScore);

            GameplayController.instance.SetLifeScore(GameManager.instance.lifeScore);

        } 

    }

    void InitializeVariables()
    {
        if(!PlayerPrefs.HasKey("Game Initialized"))
        {
            GamePreferences.SetEasyDifficultyState(0);
            GamePreferences.SetEasyDifficultyCoinScore(0);
            GamePreferences.SetEasyDifficultyHighscore(0);

            GamePreferences.SetMediumDifficultyState(1);
            GamePreferences.SetMediumDifficultyCoinScore(0);
            GamePreferences.SetMediumDifficultyHighscore(0);

            GamePreferences.SetHardDifficultyState(0);
            GamePreferences.SetHardDifficultyCoinScore(0);
            GamePreferences.SetHardDifficultyHighscore(0);

            GamePreferences.SetMusicState(0);

            PlayerPrefs.SetInt("Game Initialized", 123);
        }
    }

    void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void CheckGameStatus(int score, int coinScore, int lifeScore)
    {
        GameplayController.instance.SetLifeScore(GameManager.instance.lifeScore);

        if (lifeScore < 0)
        {

            if (GamePreferences.GetEasyDifficultyState() == 1)
            {
                int highScore = GamePreferences.GetEasyDifficultyHighscore();
                int coinHighScore = GamePreferences.GetEasyDifficultyCoinScore();

                if (highScore < score)
                {
                    GamePreferences.SetEasyDifficultyHighscore(score);
                }

                if (coinHighScore < coinScore)
                {
                    GamePreferences.SetEasyDifficultyCoinScore(coinScore);
                }
            }

            if (GamePreferences.GetMediumDifficultyState() == 1)
            {
                int highScore = GamePreferences.GetMediumDifficultyHighscore();
                int coinHighScore = GamePreferences.GetMediumDifficultyCoinScore();

                if (highScore < score)
                {
                    GamePreferences.SetMediumDifficultyHighscore(score);
                }

                if (coinHighScore < coinScore)
                {
                    GamePreferences.SetMediumDifficultyCoinScore(coinScore);
                }
            }

            if (GamePreferences.GetHardDifficultyState() == 1)
            {
                int highScore = GamePreferences.GetHardDifficultyHighscore();
                int coinHighScore = GamePreferences.GetHardDifficultyCoinScore();

                if (highScore < score)
                {
                    GamePreferences.SetHardDifficultyHighscore(score);
                }

                if (coinHighScore < coinScore)
                {
                    GamePreferences.SetHardDifficultyCoinScore(coinScore);
                }
            }

            gameStartedFromMainMenu = false;
            gameRestartedAfterPlayerDied = false;

            GameplayController.instance.GameOverShowPanel(score, coinScore);
        }

        
        else
        {
            this.score = score;
            this.coinScore = coinScore;
            this.lifeScore = lifeScore;



            gameStartedFromMainMenu = false;
            gameRestartedAfterPlayerDied = true;

            GameplayController.instance.PleyerDiedRestartTheGame();
        }
    }

}
