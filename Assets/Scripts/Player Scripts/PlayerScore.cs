using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class PlayerScore : MonoBehaviour
{

    [SerializeField]

    private AudioClip coinClip, LifeClip;

    private CameraScript cameraScript;

    private Vector3 previousPosition;
    private bool countScore;

    public static int score;
 
    public static int coinScore;


    private void Awake()
    {
        cameraScript = Camera.main.GetComponent<CameraScript>();
    }

    // Use this for initialization
    void Start()
    {
        previousPosition = transform.position;
        countScore = true;
    }

    // Update is called once per frame
    void Update()
    {
        CountScore();
    }

    void CountScore()
    {
        if (countScore)
        {
            if (transform.position.y < previousPosition.y)
            {
                score++;
            }
            previousPosition = transform.position;
            GameplayController.instance.SetScore(score);
           
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Coin")
        {
            coinScore++;
            score += 200;

            GameplayController.instance.SetScore(score);
            GameplayController.instance.SetCoinScore(coinScore);

            AudioSource.PlayClipAtPoint(coinClip, transform.position);
            target.gameObject.SetActive(false);

        }       
        if (target.tag == "Life")
        {
            GameManager.instance.lifeScore++;
            score += 300;

            GameplayController.instance.SetScore(score);
            GameplayController.instance.SetLifeScore(GameManager.instance.lifeScore);

            AudioSource.PlayClipAtPoint(LifeClip, transform.position);
            target.gameObject.SetActive(false);
        }

        if(target.tag == "Bounds" || target.tag == "Deadly")
        {
            cameraScript.moveCamera = false;
            countScore = false;

            

            transform.position = new Vector3(500, 500, 0);
            score--;
            GameManager.instance.CheckGameStatus(score, coinScore, GameManager.instance.lifeScore);

        }

        if (target.tag == "Deadly")
        {
            cameraScript.moveCamera = false;
            countScore = false;

           

            transform.position = new Vector3(500, 500, 0);
            GameManager.instance.lifeScore--;
            GameManager.instance.CheckGameStatus(score, coinScore, GameManager.instance.lifeScore);
        }
    }
}
