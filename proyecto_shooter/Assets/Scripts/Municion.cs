using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municion : MonoBehaviour {
     public Shoot shoot; //variable para obtener el componente de Script Shoot
    private AudioSource clic; //audio de recoger municion
    bool tocar = true; //detectar la colicion solo una vez
	void Start () {
        clic = GetComponent<AudioSource>();
        shoot = GameObject.Find("Personaje").GetComponentInChildren<Shoot>(); //obtener el componente Shoot (script) encontrandolo con su nombre
	}
	
	void Update () {
        
            
        
	}


    private void OnTriggerEnter(Collider col)
    {
        string name = col.gameObject.name; //dar el nombre del Gameobject que colisione con la munición a la variable name
        if (name == "Personaje" && tocar) //si la variable name es igual al nombre del gameobject "Personaje" y tocar es verdadero 
        {
            clic.Play();
            tocar = false;
            Debug.Log("Toque al jugador");
            shoot.MunMax += 100;
            Destroy(gameObject);
        }
    }
}
