using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{

    public Text coinsText, scoreText, maxScoreText,lastCoinsText,lastScoreText;

    private PlayerController controller;

    // Use this for initialization
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            //labels 

            int coins = GameManager.sharedInstance.collectedObject;
            float score = controller.GetTravelledDistance();
            float maxScore = PlayerPrefs.GetFloat("maxscore",0f);

            coinsText.text = coins.ToString();
            if (score>0)
            {
                scoreText.text = "Score " + score.ToString("f1");
                lastScoreText.text = score.ToString("f1");
            }
            else
            {
                scoreText.text = "Score " + 0;
            }
            maxScoreText.text = "MaxScore: " + maxScore.ToString("f1");
            lastCoinsText.text = coins.ToString();
        }
    }
}
