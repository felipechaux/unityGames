using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private const float limitMovement = 3.8f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      
        Vector3 mousePos= Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        //math operaciones matematicas
        //Camera.main.ScreenToWorldPoint traduce coordenadas a posicion del mundo
        transform.position = new Vector3(transform.position.x,Mathf.Clamp(mousePos.y,-limitMovement,limitMovement),transform.position.z);
	}
}
