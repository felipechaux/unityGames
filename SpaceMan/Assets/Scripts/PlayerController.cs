using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

//Variables del movimiento del personaje
public float jumForce=6f;
Rigidbody2D rigidBody;
public LayerMask groundMask;
Animator animator;

 const string STATE_ALIVE="isAlive";
 const string STATE_ONE_THE_GROUND="isOnTheGround";

   void Awake() {
	   
	   rigidBody = GetComponent<Rigidbody2D>();
	   animator = GetComponent<Animator>();
      
   }
	// Use this for initialization
	void Start () {
 	animator.SetBool(STATE_ALIVE,true);
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ){
			Jump();

		}

	   animator.SetBool(STATE_ONE_THE_GROUND,IsTouchingTheGround());

		//Gizmos rayo
		Debug.DrawRay(this.transform.position,Vector2.down*1.31f,Color.red);
		//Debug.Log("X: "+this.transform.position.y);

	    Debug.Log("IsStatic: "+IsStatic());

	}

    void Jump(){
		if(IsTouchingTheGround()){
        rigidBody.AddForce(Vector2.up*jumForce,ForceMode2D.Impulse);	
		}
	}

     //Nos indica si el personaje o no tocando el suelo
	bool IsTouchingTheGround()
	{
    if(Physics2D.Raycast(this.transform.position,Vector2.down,1.31f,groundMask))
		{
			//TODO: programar logica de contacto suelo
			return true;			  
		}
		else
		{
			//TODO: programar logica de no contacto
			return false;		  
			
		}

	}


	bool IsStatic()
	{
    if(Physics2D.Raycast(this.transform.position,Vector2.up,1.0f,groundMask))
		{
			//TODO: programar logica de contacto suelo
			return true;			  
		}
		else
		{
			//TODO: programar logica de no contacto
			return false;		  
			
		}

	}

}
