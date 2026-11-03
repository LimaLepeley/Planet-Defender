using UnityEngine;

public class ReinforcementsUI : MonoBehaviour
{
    //Variable para guardar el propio animator del objeto
    private Animator animator;

    //Variable para guardar la referencia del jugador
    private GameObject player; 

    //Variable que guarda referencia al script PlayerAttacks del objeto Player
    private PlayerAttacks playerAttacks;

    void Start()
    {
        //Se guarda la referencia al animator
        animator = GetComponent<Animator>();

        //Se guarda una referencia hacia al jugador
        player = GameObject.FindWithTag("Player");

        //Se verifica que el objeto jugador fue encontrado
        if(player)
        {
            //Se guarda la referencia al script PlayerAttacks
            playerAttacks = player.GetComponent<PlayerAttacks>();
        }
        else
        {
            Debug.LogWarning("El jugador no fue encontrado");
        }
    }

    void Update()
    {
        //Se verifica que la referencia no este vacia
        if(playerAttacks)
        {
            //Si los refuerzos estan en uso
            if(playerAttacks.getReinforcementsFinished())
            {
                if(!animator.GetBool("InUse"))
                {
                    animator.SetBool("InUse", true);
                }
            }
            else
            {
                if(animator.GetBool("InUse"))
                {
                    animator.SetBool("InUse", false);
                }
            }

            //Si los refuerzos se pueden usar
            if(playerAttacks.getReinforcementsTimer() >= playerAttacks.getReinforcementsCooldown())
            {
                if(!animator.GetBool("Ready"))
                {
                    animator.SetBool("Ready", true);
                }
            }
            else
            {
                if(animator.GetBool("Ready"))
                {
                    animator.SetBool("Ready", false);
                }
            }


        }
        else
        {
            Debug.LogError("La referencia al jugador esta vacia");
        }
    }
}