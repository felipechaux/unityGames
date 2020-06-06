using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //velocidad de enemigo
    public float runningSpeed = 1.5f;

    //dañoagame  de enemigo
    public int enemyDamage= 10;
    //rigid
    Rigidbody2D rigidBody;
    //hacia donde mira el enemigo
    public bool facingRight = false;

    //posicion
    private Vector3 startPosition;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        //guardar posicion por primera vez
        startPosition = this.transform.position;
    }

    // Use this for initialization
    void Start () {
       // cada vez que se llame un nuevo objeto
        this.transform.position = startPosition;
	}
	
    void FixedUpdate()
    {
        float currentRunningSpeed = runningSpeed;

        if (facingRight)
        {
            //mirando hacia la dereha
            currentRunningSpeed = runningSpeed;
            //rotar 180 grados con respecto a la posicion actal
            this.transform.eulerAngles = new Vector3(0,180,0);
        }
        else
        {
            //mirando hacia la izquierda
            currentRunningSpeed = -runningSpeed;
            this.transform.eulerAngles = Vector3.zero;

        }


        if (GameManager.sharedInstance.currentGameState==GameState.inGame)
        {
            //movimiento en eje de las x, el eje y seguira igual
            rigidBody.velocity = new Vector2(currentRunningSpeed,
                rigidBody.velocity.y);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log(collision.tag);
        if (collision.tag == "Coin")
        {
            return;
        }
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().CollectHealth(-enemyDamage);
            return;
        }

        // si llegamos aqui, no hemos chocado n con monedas, ni con players
        //lo mas normal es aque aqui haya otro enemigo o bien escenario
        //vamos a hacer que el enemigo rote
        facingRight = !facingRight;
    }
}
