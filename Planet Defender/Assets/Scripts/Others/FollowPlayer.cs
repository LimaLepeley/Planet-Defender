using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//* Este script sirve para generar un objeto que siga al jugador para que asi ataques que siguen al jugador no rompan el juego al jugador ser destruido
public class FollowPlayer : MonoBehaviour
{
    //Variable para guardar la referencia al jugador
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //Se busca al jugador para encontrarlo
        player = GameObject.FindWithTag("Player");

        //Warning por si el jugador no ha sido encontrado
        if(!player)
        {
            Debug.LogWarning("El jugador no ha sido encontrado");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Siempre y cuando que el jugador este activo o que exista lo seguira
        if(player.activeSelf == true &&  player != null)
        {
            transform.position = player.transform.position;
        }
    }
}
