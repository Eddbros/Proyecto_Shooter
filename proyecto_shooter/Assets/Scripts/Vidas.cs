using UnityEngine;
using System.Collections;

public class Vidas : MonoBehaviour {

	//Vida total
	public int currentHealth;

	public void Damage(int damageAmount)
	{
		//recibira el daño causado por cada tipo de arma
		currentHealth -= damageAmount;

		//cuando la vida sea 0 el objeto se destruye
		if (currentHealth <= 0) 
		{
            Destroy(gameObject);
		}
	}
}
