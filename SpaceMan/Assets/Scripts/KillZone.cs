using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

     //cuando un collider entrar dentro de otro 
	 private void OnTriggerEnter2D(Collider2D collision) {
		 if(collision.tag=="Player"){

                    PlayerController controller = collision.GetComponent<PlayerController>();
					controller.Die();
            Debug.Log("Die en killzone");
		 }
		 
	 }

}
