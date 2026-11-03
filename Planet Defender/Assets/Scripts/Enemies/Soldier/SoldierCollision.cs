using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierCollision : MonoBehaviour
{
    //Variable para saber si el enemigo esta desacelerado
    private bool decelerated;

    void Start()
    {
        
    }

    //Corutina que se llama cada vez que el enemigo es golpeado por un laser de energia
    public IEnumerator Decelerated()
    {
        decelerated = true;
        yield return new WaitForSeconds(1.5f);
        decelerated = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if(other.gameObject.tag == "")
    }

    //Getters y Setters
    public bool getDecelerated() {
		return this.decelerated;
	}
}