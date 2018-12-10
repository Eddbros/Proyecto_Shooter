using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SNPR : Person {

    public float vision;
    public float shootsRemain = 1;
    public float fireRate = 5f;
    public float distancia;   
    public bool aim = false;  

    public Transform gun;   //Aquí se reacomoda la bala y basicamente de aquí salen los disparos

    public GameObject playerToTarget;
    public GameObject prefabBullet;


	// Use this for initialization
	void Start ()
    {
        prefabBullet.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        distancia = Vector3.Distance(playerToTarget.transform.position, transform.position);

        AimPlayer();
    }

    public void AimPlayer()
    {
        if (distancia < vision)
        {
            aim = true;
        }
        else
        {
            aim = false;
        }

        if (aim)
        {
            transform.LookAt(playerToTarget.transform.position);
            
            if (shootsRemain > 0)
            {
                
                StartCoroutine ("FireRate");
                

                shootsRemain = shootsRemain-1;
                
            }
        }
    }

    

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(7.0f);
        Debug.Log("Disparo Efectuado");
        
        prefabBullet.SetActive(true);
        
    }

    




    private void OnDrawGizmos()                          //Dibuja el rango de vision del jugador
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, vision);
    }




}
