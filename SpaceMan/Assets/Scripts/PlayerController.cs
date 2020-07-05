using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

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
    //posicion inicial de personale
    Vector3 startPosition;

    //modifcacion- rotacion de animaciones del sprite
    private SpriteRenderer mySpriteRenderer;
    //animaciones 
    const string STATE_ALIVE = "isAlive";
    const string STATE_ONE_THE_GROUND = "isOnTheGround";

    //puntos de vida y mana del jugador - SerializeField para ver la vida en el ide --- ya que es private
    [SerializeField]
    private int healthPoints, manaPoints;

    //constantes jugador
    public const int INITIAL_HEALTH = 100, INITIAL_MANA = 15, MAX_HEALTH=200,MAX_MANA=30,MIN_HEALTH=10,MIN_MANA=0;

    public const int SUPERJUMP_COST = 5;
    public const float SUPERJUMP_FORCE = 1.7f;

    public float jumpRaycastDistance = 1.5f;

    //colider que colisiona contra el piso
    private BoxCollider2D colliderGround;
    //offet inicial de collider
    Vector2 startOffsetGround;

    public AudioClip audioJump;
    public AudioClip audioGameOver;
    public AudioClip audioDamage;


    Color colorPlayer;


    void Awake()
    {

        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        
        float r= 227;  // red component
        float g= 116;  // green component
        float b= 116;  // blue component
  
        colliderGround = GetComponent<BoxCollider2D>();

    }
    // Use this for initialization
    void Start()
    {
        //guardar posicion actual de personaje 
        startPosition=transform.position;
        startOffsetGround = colliderGround.offset;
        colorPlayer = mySpriteRenderer.color;

    }

    public void ChangeColorDamage()
    {
        Color colorDamage = new Color32(212, 105, 105, 255);
        mySpriteRenderer.color = colorDamage;
        GetComponent<AudioSource>().PlayOneShot(audioDamage);
        Invoke("RestartColorPlayer", 0.5f);
    }

    void RestartColorPlayer()
    {
        mySpriteRenderer.color = colorPlayer;
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
        //reiniciar posicion y velocidad
        transform.position=startPosition;
        rigidBody.velocity=Vector2.zero;
        //reiniciar offset de collider ground
        colliderGround.offset = startOffsetGround;
        //reiniciar camara
        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraFollow>().ResetCameraPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if ( CrossPlatformInputManager.GetButtonDown("Jump"))
            {
                Jump(false);
            }
            if ( CrossPlatformInputManager.GetButtonDown("SuperJump"))
            {
                Jump(true);
            }

            if ( CrossPlatformInputManager.GetAxis("Horizontal") < 0)
            {
                Left();
            }

            if ( CrossPlatformInputManager.GetAxis("Horizontal") > 0)
            {
                Right();

            }

        }
        else
            //si no se esta dentro de la partida detener personaje
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);


        animator.SetBool(STATE_ONE_THE_GROUND, IsTouchingTheGround());
        //Gizmos rayo
        Debug.DrawRay(transform.position, Vector2.down * jumpRaycastDistance, Color.red);

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
            //audio jump
            GetComponent<AudioSource>().PlayOneShot(audioJump);

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
        if (Physics2D.Raycast(transform.position, Vector2.down, jumpRaycastDistance, groundMask))
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
        //modificacion de offset de collider ground
        colliderGround.offset = new Vector2(0, 0);

        float travelledDistance = GetTravelledDistance();
        float previousMaxDistance = PlayerPrefs.GetFloat("maxscore", 0f);
        if (travelledDistance>0)
        {
            //persistir datos jugador - preferencias en sesion
            if (travelledDistance > previousMaxDistance)
            {
                PlayerPrefs.SetFloat("maxscore", travelledDistance);
            }
        }

        animator.SetBool(STATE_ALIVE,false);
        GameManager.sharedInstance.GameOver();
        //audio game over
        GetComponent<AudioSource>().PlayOneShot(audioGameOver);

    }


    public void CollectHealth(int points)
    {
        Debug.Log("CollectHealth " + points);
        healthPoints += points;
        if (healthPoints>=MAX_HEALTH)
        {
            healthPoints = MAX_HEALTH;
        }

        //muerte
        if (healthPoints<=0)
        {
            Die();
        }
    }

    public void CollectMana(int points)
    {
        Debug.Log("CollectMana "+points);
        manaPoints += points;
        if (manaPoints >= MAX_MANA)
        {
            manaPoints = MAX_MANA;
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
        return transform.position.x - startPosition.x;
    }

}
