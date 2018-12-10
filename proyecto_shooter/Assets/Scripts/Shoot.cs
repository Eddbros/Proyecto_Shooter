using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shoot : MonoBehaviour {
    public int gunDamage = 1; //variable que determina el daño del arma
    public float FireRate = 0.1f; //variable que determina el tiempo de disparo entre bala
    public float weapointRange = 50f; //variable que determina el rango de distancia del alcance del hit
    public float hitForce = 100f; //variable que determina la fuerza que resivira el objeto al colisionar con el hit
    public int contadorbalas = 10; //variable que guarda el numero de balas con las que cuentas para disparar
    public int MunMax = 100; //variable que guarda el Maximo de municion que tienes 
    public GameObject destello; //variable que dará un detello al disparar
    public GameObject Marca; //punto que dejan las balas en las paredes
    int resta = 10; //variable para restar al maximo de municion 
    bool disparar = true; //puedes disparar o no
    public AudioClip SonidoDisparo; //variable con la que reproduciremos el sonido de disparo
    public AudioClip SonidoRecargar; //variable con la que reproduciremos el sonido de recarga
    public AudioClip SonidoSinBalas; //variable con la que reproduciremos el sonido cuando no tienes balas
    GameObject arma;

    public Transform gunEnd; //variable punto transform para darle el inicio al hit

    private Camera fpsCam; //variable para obtener componentes de la camara
    private WaitForSeconds shotDuration = new WaitForSeconds(2f); //duracion de la recarga
    private AudioSource gunAudio; 
    public GameObject text; //variable para obtener los componentes de texto municion a traves de un GameObject para imprimir el estdo de la municion
    Text Cbalas; //variable para modificar e imprimir el texto del objeto municion a traves de esta
   // private LineRenderer laserLine;
    private float nexFire; //Sumar al tiempo transcurrido en el juego el tiempo del proximo disparo 


	void Start () {
        gunAudio = GetComponent<AudioSource>(); //obtenet componentes de audio
        fpsCam = GetComponentInParent<Camera>(); //obtener componentes de Camara
        Cbalas = text.GetComponent<Text>(); //obtener componentes de Texto
        arma = GetComponent<GameObject>();
       
	}
	

	void Update () {
        if (MunMax < 0) //la municion maxima no disminuira de 0
        {
            MunMax = 0;
        }
        Cbalas.text = "Munición: " + contadorbalas.ToString() + " / " + MunMax.ToString(); //imprimir la informacion en el Canvas a traves de esta variable
        if (Input.GetMouseButton(0) && Time.time > nexFire && contadorbalas <= 0 && disparar)
        {
            nexFire = Time.time + FireRate; //sumar al tiempo que a trascurrido en el juego el tiempo de disparo entre balas
            contadorbalas = 0; // el contador de balas nunca bajara de 0
            GetComponent<AudioSource>().PlayOneShot(SonidoSinBalas); //reproducir el audio

        }
        if (Input.GetMouseButton(0) && Time.time > nexFire && contadorbalas > 0)
        {
            Instantiate(destello, transform.position, Camera.main.transform.rotation); //instanciar destello y hacer que tenga la rotacion de la camara
            GetComponent<AudioSource>().PlayOneShot(SonidoDisparo); //reproducir el audio
            nexFire = Time.time + FireRate; //tiempo de disparo, el tiempo que ha transcurrido en el juego más el tiempo de disparo.
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f)); //determinar hacia donde estoy observando en los valores del mundo
            RaycastHit hit; //linea de colicionador para enemigos
            RaycastHit hit2; //linea de colicionador para todo

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weapointRange)) //devuelve un valor para detectar si colicionó, si es asi, dibujara una linea hasta el objeto que colicionó
            {
                // laserLine.SetPosition(1, hit.point);
                Vidas sb = hit.collider.GetComponent<Vidas>(); //si el hit colisiona con el objeto con el script vidas
                if (sb != null) //si el rigidbody no es nulo
                {
                    sb.Damage(gunDamage); //mandar el da{o al script
                }
                if (hit.rigidbody != null && contadorbalas > 0) //si el rigidbody no es nulo y tienes balas para disparar
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce); //darle una fuerza de empuje en su Normal
                }
            }

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit2, 1000)) //devuelve un valor para detectar si colicionó, si es asi, dibujara una linea hasta el objeto que colicionó
            {
                if (hit2.collider != null)
                {

                    Quaternion rotMarca = Quaternion.FromToRotation(Vector3.up, hit2.normal); //darle rotacion en la normal del objeto que colisiona con el hit 
                    GameObject Marcaobj = Instantiate(Marca, hit2.point, rotMarca);
                    Vector3 PosObjectMarca = Marcaobj.transform.position;
                    PosObjectMarca.y += 0.1f;
                    Marcaobj.transform.position = PosObjectMarca;
                    Marcaobj.transform.parent = hit2.transform;
                }
                
            }

            contadorbalas -= 1;
        }
         if (Input.GetKeyDown(KeyCode.R) && MunMax > 0 && disparar) //recargar con R
         {
            disparar = false; //no puedes dispara mientras recargas
            StartCoroutine("Recargar"); //llamar corrutina Recargar
            
        }
    }

    IEnumerator Recargar() //corrutina que actualiza los contadores de balas al recargar y reproduce el sonido recargar
    {
        GetComponent<AudioSource>().PlayOneShot(SonidoRecargar); //reproducir el audio
        yield return shotDuration;
        if (MunMax >= 10)
        {
            resta = 10;
        }
        if (MunMax < 10)
        {
            resta = MunMax;
        }
        if (contadorbalas <=0) {
            contadorbalas = resta;
            MunMax -= resta;
        }
        if (contadorbalas > 0)
        {
            MunMax -= resta - contadorbalas;
            contadorbalas = resta;
        }
        disparar = true;
    }
    IEnumerator Destruir()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(this.destello.gameObject);
    }
}
