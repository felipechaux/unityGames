using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //detecta cuando la plataforma colisiona con jugador para empezar animacion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D");
        Animator animator = GetComponent<Animator>();
        animator.enabled = true;
    }

}
