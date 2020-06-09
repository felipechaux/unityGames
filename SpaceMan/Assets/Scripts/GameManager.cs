using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    menu,
    inGame,
    gameOver
}
public class GameManager : MonoBehaviour
{

    public GameState currentGameState = GameState.menu;

    // singleton - solo habra una instancia compartida
	public static GameManager sharedInstance;
    private PlayerController controller;
    //control de monedas recogidas
    public int collectedObject = 0;



    void Awake()
	{
		if(sharedInstance==null){
			sharedInstance=this;
		}

    }

    // Use this for initialization
    void Start()
    {
       controller = GameObject.Find("Player").GetComponent<PlayerController>();



    }

    // Update is called once per frame
    void Update()
    {
        //tecla generica - enter
       if(Input.GetButtonDown("Submit") && currentGameState != GameState.inGame){
           
           StartGame();
       }
    }

    //inicio de partida
    public void StartGame()
    {
       SetGameState(GameState.inGame);
    }

    // finalizar partida
    public void GameOver()
    {
       SetGameState(GameState.gameOver);
        //audio game over
    }

    //menu de opciones
    public void BackToMenu()
    {
       SetGameState(GameState.menu);

    }

    private void SetGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.menu:
                //TODO: colocar la logica del menu
                MenuManager.sharedInstance.ShowMainMenu();
                MenuManager.sharedInstance.HideGameCanvas();
                MenuManager.sharedInstance.HideGameOverMenu();
                break;
            case GameState.inGame:
                //limpiar escena
                LevelManager.shareInstance.RemoveAllLevelBlocks();
                //colocar bloques nuevos
                LevelManager.shareInstance.GenerateInitialBlocks();
                //TODO: hay preparar la escena para jugar
                controller.StartGame();
                //ocultar menu
                MenuManager.sharedInstance.HideMainMenu();
                MenuManager.sharedInstance.ShowGameCanvas();
                MenuManager.sharedInstance.HideGameOverMenu();

                break;
            case GameState.gameOver:
                //TODO: preparar el juego para el Game Over
                MenuManager.sharedInstance.HideGameCanvas();
                MenuManager.sharedInstance.ShowGameOverMenu();
             
                break;
        }

        this.currentGameState=newGameState;

    }
   

    public void CollectObject(Collectable collectable)
    {
        collectedObject += collectable.value;
        Debug.Log("collect " + collectedObject);

    }





}
