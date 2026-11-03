using UnityEngine;

public class OverchargeUI : MonoBehaviour
{
    //Variable para guardar el propio animator del objeto
    private Animator animator;
    
    //Variable para guardar la referencia del jugador
    private GameObject player; 

    //Variable que guarda referencia al script PlayerUpgrades del objeto Player
    private PlayerUpgrades playerUpgrades;

    void Start()
    {
        //Se guarda la referencia al animator
        animator = GetComponent<Animator>();

        //Se guarda una referencia hacia al jugador
        player = GameObject.FindWithTag("Player");

        //Se verifica si el objeto Player exista
        if(player)
        {
            //Se guarda la referencia al script PlayerUpgrades
            playerUpgrades = player.GetComponent<PlayerUpgrades>();
        }
        else
        {
            Debug.LogWarning("El objeto Player no fue encontrado");
        }
    }

    void Update()
    {
        //Se verifica que el script PlayerUpgrades exista
        if(playerUpgrades)
        {
            //La variables en el animator del UI de Overcharge (S2) cambian acorde a las variables de PlayerUpgrades
            if(playerUpgrades.getIsOverchargeFull())
            {
                if(!animator.GetBool("Full"))
                {
                    animator.SetBool("Full", true);
                }
            }
            else
            {
                if(animator.GetBool("Full"))
                {
                    animator.SetBool("Full", false);
                }
            }
            
            if(playerUpgrades.getIsOverchargeActive())
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
        }
        else
        {
            Debug.LogError("La referencia al script PlayerUpgrades esta vacia");
        }
    }
}