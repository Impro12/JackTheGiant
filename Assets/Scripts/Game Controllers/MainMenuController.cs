using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] private Button musicBtn;
    [SerializeField] private Sprite[] musicIcons;

    // Use this for initialization
    void Start()
    {
        CheckToPlayTheMusic();
    }

    void CheckToPlayTheMusic()
    {
        if (GamePreferences.GetMusicState() == 1)
        {
            MusicController.instance.PlayMusic(true);
            musicBtn.image.sprite = musicIcons[1];
        }
        else
        {
            MusicController.instance.PlayMusic(false);
            musicBtn.image.sprite = musicIcons[0];
        }
    }

    public void StartGame()
    {
        
        GameManager.instance.gameStartedFromMainMenu = true;

        //SceneManager.LoadScene("SampleScene");
        SceneFader.instance.LoadLevel("SampleScene");
    }

    public void HighscoreMenu()
    {
        //Application.LoadLevel("HighscoreScene");
        SceneManager.LoadScene("HighscoreScene");
    }

    public void OptionsMenu()
    {
        //Application.LoadLevel("OptionsMenu");
        SceneManager.LoadScene("OptionsScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        SceneFader.instance.LoadLevel("MainMenu");
    }

    public void MusicButton()
    {
        if (GamePreferences.GetMusicState() == 0)
        {
            GamePreferences.SetMusicState(1);
            MusicController.instance.PlayMusic(true);
            musicBtn.image.sprite = musicIcons[1];
        }
        else if (GamePreferences.GetMusicState() == 1)
        {
            GamePreferences.SetMusicState(0);
            MusicController.instance.PlayMusic(false);
            musicBtn.image.sprite = musicIcons[0];
        }

    }
}