using UnityEngine.UI;
using UnityEngine;

public class ImprovedRefrigerationUI : MonoBehaviour
{
    //Variable para guardar la referencia del jugador
    private GameObject player;

    //Variable para guardar el propio animator del objeto
    private Animator animator;

    //Variable que guarda referencia al script PlayerUpgrade del objeto Player
    private PlayerUpgrades playerUpgrades;

    void Start()
    {
        //Se guarda la referencia al animator
        animator = GetComponent<Animator>();

        //Se guarda una referencia hacia al jugador
        player = GameObject.FindWithTag("Player");

        //Se verifica si el objeto Player fue encontrado
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
        if(playerUpgrades)
        {
            //La variables en el animator del UI de ImprovedRefrigeration cambian acorde a las variables de PlayerUpgrades
            //* Las variables de animator solo cambian una vez que se nota un cambio
            if(playerUpgrades.getIsImprovedRefrigerationFull())
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
            
            if(playerUpgrades.getIsImprovedRefrigerationActive())
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