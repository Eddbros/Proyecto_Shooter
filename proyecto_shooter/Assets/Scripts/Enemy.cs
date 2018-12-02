using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : Person
{

    bool isFollowingPlayer = false;

    float Horizontal;
    public GameObject[] Points;
    int Point;
    GameObject player;
    bool restarvida = false;
    AIDestinationSetter mov;
    AILerp Modspeed;
    Rigidbody enemyRB;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Point=0;
        player = GameObject.FindGameObjectWithTag("Player");
        vida = 10;
        mov = GetComponent<AIDestinationSetter>();
        Modspeed = GetComponent<AILerp>();
        Modspeed.canMove=true;
        enemyRB = GetComponent<Rigidbody>();
    }


    void Update()
    {
        moveAndRotateEnemy();
    }

    void moveAndRotateEnemy()
    {
        Horizontal = 0;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= 20.0f && !isFollowingPlayer)
        {
            isFollowingPlayer = true;
            mov.target = player.transform;
            Modspeed.canMove = true;
        }
        else if (distance > 20.0f && isFollowingPlayer)
        {
            isFollowingPlayer = false;
            mov.target = null;
        }
        if (distance <= 7.0f && isFollowingPlayer)
        {
            mov.enabled = false;
            mov.target = null;
            Modspeed.canMove = false;
            enemyRB.freezeRotation = true;
        }
        else if (distance > 7.0f && isFollowingPlayer)
        {
            mov.target = player.transform;
            mov.enabled = true;
            Modspeed.canMove = true;
            enemyRB.freezeRotation = false;
        }

        if (!isFollowingPlayer)
        {
            patrolZone();
            Modspeed.canMove = true;
        }
        else
        {
            FollowPlayer();
        }

        float currentRotation = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;

        float movex = Horizontal * velocity.x * Time.deltaTime;
        pos = transform.position;
        transform.Rotate(new Vector3(0, movex, 0));
    }
  

    void patrolZone()
    {
        if (!isFollowingPlayer)
        {
            int nexPoint = Point + 1;
            if (nexPoint >= Points.Length)
            {
                nexPoint = 0;
            }

            transform.LookAt(Points[nexPoint].transform);
            mov.target = Points[nexPoint].transform;
            float distancia = Vector3.Distance(transform.position, Points[nexPoint].transform.position);

            if (distancia < 1.0f)
            {
                if (Point >= Points.Length - 1)
                {
                    Point = 0;
                }
                else
                {
                    Point++;
                }
            }
        }
    }

    void FollowPlayer()
    {
        transform.LookAt(player.transform.position);
    }



}
