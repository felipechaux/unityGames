using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Variables del movimiento del personaje
    public float jumpForce = 20f;
    public float runningSpeed = 2f;
    //fisica
    Rigidbody2D rigidBody;
    //identificacion de suelo
    public LayerMask groundMask;
    Animator animator;
    Vector3 startPosition; 

    //modifcacion- rotacion de animaciones del sprite
    private SpriteRenderer mySpriteRenderer;
    //animaciones 
    const string STATE_ALIVE = "isAlive";
    const string STATE_ONE_THE_GROUND = "isOnTheGround";

    //puntos de vida y mana del jugador
    private int healthPoints, manaPoints;

    //constantes jugador
    public const int INITIAL_HEALTH = 100, INITIAL_MANA = 15, MAX_HEALTH=200,MAX_MANA=30,MIN_HEALTH=10,MIN_MANA=0;

    public const int SUPERJUMP_COST = 5;
    public const float SUPERJUMP_FORCE = 1.5f;


    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();


    }
    // Use this for initialization
    void Start()
    {
        //guardar posicion actual de personaje 
        startPosition=this.transform.position;
    }

    public void StartGame(){
         
        animator.SetBool(STATE_ALIVE, true);
        //animator.SetBool(STATE_ONE_THE_GROUND, true);

        //inicializacion de variables del jugador
        healthPoints = INITIAL_HEALTH;
        manaPoints = INITIAL_MANA;

       //retrasar animacion para no notar que el personaje al revivir aparezca muerto
        Invoke("RestartPosition",0.0f);
    }

    void RestartPosition(){
        this.transform.position=startPosition;
        this.rigidBody.velocity=Vector2.zero;
        //reiniciar camara
        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraFollow>().ResetCameraPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump(false);
            }
            if (Input.GetButtonDown("SuperJump"))
            {
                Jump(true);
            }

            if (Input.GetAxis("Horizontal") < 0)
            {
                Left();
            }

            if (Input.GetAxis("Horizontal") > 0)
            {
                Right();

            }

        }
        else
            //si no se esta dentro de la partida detener personaje
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);


        animator.SetBool(STATE_ONE_THE_GROUND, IsTouchingTheGround());
        //Gizmos rayo
        Debug.DrawRay(this.transform.position, Vector2.down * 1.31f, Color.red);

    }

    //actualizacion a ritmo fijo - para no experimentar bajones de frames
    void FixedUpdate()
    {
        if (rigidBody.velocity.x < runningSpeed)
        {
            //  rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);
        }

    }


    void Jump(bool superjump)
    {
        float jumpForceFactor = jumpForce;
        if (superjump && manaPoints >= SUPERJUMP_COST)
        {
            manaPoints -= SUPERJUMP_COST;
            jumpForceFactor *= SUPERJUMP_FORCE;
        }

        if (IsTouchingTheGround())
        {
            rigidBody.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
        }
    }

    void Left()
    {
        rigidBody.velocity = new Vector2(-runningSpeed, rigidBody.velocity.y);
        if (mySpriteRenderer != null)
        {
            // flip the sprite
            mySpriteRenderer.flipX = true;
        }
    }

    void Right()
    {
        rigidBody.velocity = new Vector2(runningSpeed, rigidBody.velocity.y);

        if (mySpriteRenderer != null)
        {
            // flip the sprite
            mySpriteRenderer.flipX = false;
        }
    }

    //Nos indica si el personaje o no tocando el suelo
    bool IsTouchingTheGround()
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 1.31f, groundMask))
        {
            //instancia compartida
            // GameManager.sharedInstance.currentGameState=GameState.inGame;
            return true;
        }
        else
        {
            return false;

        }

    }

    public void Die(){

        float travelledDistance = GetTravelledDistance();
        //persistir datos jugador - preferencias en sesion
        float previousMaxDistance = PlayerPrefs.GetFloat("maxscore",0f);
        if (travelledDistance > previousMaxDistance)
        {
            PlayerPrefs.SetFloat("maxscore", travelledDistance);
        }

        this.animator.SetBool(STATE_ALIVE,false);
        GameManager.sharedInstance.GameOver();
    }


    public void CollectHealth(int points)
    {
        Debug.Log("CollectHealth " + points);
        this.healthPoints += points;
        if (this.healthPoints>=MAX_HEALTH)
        {
            this.healthPoints = MAX_HEALTH;
        }
    }

    public void CollectMana(int points)
    {
        Debug.Log("CollectMana "+points);
        this.manaPoints += points;
        if (this.manaPoints >= MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
    }

    public int GetHealth()
    {
        return healthPoints;
    }

    public int GetMana()
    {
        return manaPoints;
    }


    public float GetTravelledDistance()
    {
        return this.transform.position.x - startPosition.x;
    }

}
