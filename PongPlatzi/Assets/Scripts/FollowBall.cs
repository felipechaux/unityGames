using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour
{
    public Transform ball;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if (ball.GetComponent<BallBehavior>().gameStarted)
        {

            if (transform.position.y < ball.position.y)
            {
                //aumentar unidades arriba
                transform.position = new Vector3(transform.position.x,transform.position.y+speed,transform.position.z);
            }else if (transform.position.y > ball.position.y)
            {
              //disminuir unidades abajo
                transform.position = new Vector3(transform.position.x, transform.position.y -speed, transform.position.z);

            }
        }

    }
}
