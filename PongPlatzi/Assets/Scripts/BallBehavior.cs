using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour {

    public Transform paddle;

    Rigidbody2D rbBall;
    //init game
    public bool gameStarted = false;

    float positionDiference=0;

    // Use this for initialization
    void Start () {
         positionDiference = paddle.position.x - transform.position.x;
         rbBall = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {

        if (!gameStarted)
        {
            //agregar pelota un poco mas adelante de la paleta - posicion estatica en la paleta
            transform.position = new Vector3(paddle.position.x - positionDiference, paddle.position.y, paddle.position.z);
            if(Input.GetMouseButtonUp(0))
            {
                Debug.Log("oprimir jugar");
                rbBall.velocity = new Vector2(12,12);
                gameStarted = true;
            }
        }

        //soltar pelota
    }

    //cuando pelota choque con cualquier cosa
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<AudioSource>().Play();
    }


}
