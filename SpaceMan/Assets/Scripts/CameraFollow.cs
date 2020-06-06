using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//perseguir cualquier objetivo
public class CameraFollow : MonoBehaviour {

    //objetivo de camara
    public Transform target;

    //distancia para seguir objetivo
    public Vector3 offset = new Vector3(0.2f, 0.0f, -10f);

    //efecto de seguimiento
    public float dampingTime = 0.3f;

    //velocidad camara
    public Vector3 velocity = Vector3.zero;

    void Awake()
    {    
        //frames game
        Application.targetFrameRate = 60;

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveCamera(true);
	}

    public void ResetCameraPosition()
    {
        MoveCamera(false);
    }

    void MoveCamera(bool smooth)
    {
        Vector3 destination = new Vector3(target.position.x - offset.x, offset.y, offset.z);

       
        if (smooth)
        {
            //barrido suavisado
            this.transform.position = Vector3.SmoothDamp(this.transform.position,destination,ref velocity,dampingTime);
        }
        else
        {   //sin efecto suavisado
            this.transform.position = destination;
        }
    }



}
