using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour {

    public Transform paddle;

    public Rigidbody2D rbBall;
    //init game
    bool gameStarted = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!gameStarted)
        {
            float positionDiference = paddle.position.x - transform.position.x;

            //agregar pelota un poco mas adelante de la paleta
            transform.position = new Vector3(paddle.position.x - positionDiference, paddle.position.y, paddle.position.z);


            if(Input.GetMouseButtonUp(0))
            {
                rbBall.velocity = new Vector2(8,8);
                gameStarted = true;
            }
        }
    }
      
}
