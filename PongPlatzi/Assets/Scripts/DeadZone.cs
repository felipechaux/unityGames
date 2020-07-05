using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour {
    
    public Text scorePlayerText;
    public Text scoreEnemyText;

    int scorePlayerQuantity;
    int scoreEnemyQuantity;
    const int maxScore = 3;

    public SceneChanger sceneChanger;

    private void OnTriggerEnter2D(Collider2D ball)
    {
        Debug.Log(gameObject.tag);
        if(gameObject.tag=="Left"){
          scoreEnemyQuantity++;
          UpdateScoreLabel(scoreEnemyText,scoreEnemyQuantity);
        }else if(gameObject.CompareTag("Right")){
          scorePlayerQuantity++;
           UpdateScoreLabel(scorePlayerText,scorePlayerQuantity);
        }

        ball.GetComponent<BallBehavior>().gameStarted = false;
        CheckScore();
    }

    void CheckScore()
    {
        if (scorePlayerQuantity>=maxScore)
        {
            sceneChanger.ChangeSceneTo("WinScene");


        }
        else if (scoreEnemyQuantity>=maxScore)
        {
             sceneChanger.ChangeSceneTo("GameOverScene");
           
        }
        GetComponent<AudioSource>().Play();

    }

    void UpdateScoreLabel(Text label, int score){
        label.text=score.ToString();
    }

  

}
