using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //cuando un collider entrar dentro de otro 
    private void OnTriggerEnter2D(Collider2D collision)
    {
            Debug.Log("Debemos destruir el bloque anterior");

        if (collision.tag=="Player")
        {
            LevelManager.shareInstance.AddLevelBlock();
            LevelManager.shareInstance.RemoveLevelBlock();
        }

    }
}
