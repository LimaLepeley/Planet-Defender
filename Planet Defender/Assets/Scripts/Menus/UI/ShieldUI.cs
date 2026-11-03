using UnityEngine;

public class ShieldUI : MonoBehaviour
{
    //Variable para guardar la referencia del jugador
    private GameObject player; 

    //Variable para guardar una referencia al script PlayerCollision
    private PlayerCollision playerCollision;

    //Variable para guardar el propio animator del objeto
    private Animator animator;

    //Variable que guarda cual es el valor de shield para saber si este cambio
    private int actualShield = 0;

    void Start()
    {
        //Se guarda la referencia al animator
        animator = GetComponent<Animator>();

        //Se guarda una referencia hacia al jugador
        player = GameObject.FindWithTag("Player");

        //Se verifica si el objeto Player existe
        if(player)
        {
            //Se guarda una referencia al script PlayerCollision
            playerCollision = player.GetComponent<PlayerCollision>();
        }
        else
        {
            Debug.LogWarning("El objeto Player no fue encontrado");
        }
    }

    void Update()
    {
        //Se verifica que el script PlayerCollision exista
        if(playerCollision)
        {
            //Solo se actualiza la variable del animator si se detecta algun cambio
            if(actualShield != playerCollision.getShield())
            {
                animator.SetInteger("Shield", playerCollision.getShield());
                actualShield = playerCollision.getShield();
            }
        }
        else
        {
            Debug.LogError("La referencia al script PlayerCollision esta vacia");
        }
    }
}