using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    // singleton - solo habra una instancia compartida
    public static MenuManager sharedInstance;
    public Canvas menuCanvas;
    public Canvas menuGameOver;
    public Canvas gameCanvas;




    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    public void ShowGameCanvas()
    {
        gameCanvas.enabled = true;
    }

    public void HideGameCanvas()
    {
        gameCanvas.enabled = false;
    }

    public void ShowMainMenu()
    {
        menuCanvas.enabled = true;
    }


    public void HideMainMenu()
    {
        menuCanvas.enabled = false;
    }

    public void ShowGameOverMenu()
    {
        Debug.Log("ShowGameOverMenu");
        menuGameOver.enabled = true;
    }

    public void HideGameOverMenu()
    {
        menuGameOver.enabled = false;
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR 
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void Start () {
		
	}
	
	void Update () {
		
	}
}
