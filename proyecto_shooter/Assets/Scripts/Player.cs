using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Person
{
    
    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        moveAndRotatePlayer();
    }

    void moveAndRotatePlayer()
    {
        pos = transform.position;
        float movez = 1;
        pos.x = (pos.x + velocity.x * Time.deltaTime) * movez;

        //moverse con W
        if (Input.GetKey(KeyCode.W))
        {
            movez = 1;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            movez = 0;
        }

        //moverse con s
        if (Input.GetKey(KeyCode.W))
        {

        }
        if (Input.GetKeyUp(KeyCode.W))
        {

        }

        //moverse con a
        if (Input.GetKey(KeyCode.W))
        {

        }
        if (Input.GetKeyUp(KeyCode.W))
        {

        }

        //moverse con d

        if (Input.GetKey(KeyCode.W))
        {

        }
        if (Input.GetKeyUp(KeyCode.W))
        {

        }
        transform.position = pos;


        //correr
        if (run)
        {

        }
    }

}
