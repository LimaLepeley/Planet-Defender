using UnityEngine.UI;
using UnityEngine;

public class EnergyBarUI : MonoBehaviour
{
    //Variable para guardar la referencia del jugador
    private GameObject player;

    //Variable para guardar la referencia al script PlayerEnergy
    private PlayerEnergy playerEnergy;

    //Barra de energia del jugador
    public Image energyBar;

    void Start()
    {
        //Se guarda una referencia hacia al jugador
        player = GameObject.FindWithTag("Player");

        //Se verifica que el objeto Player halla sido encontrado
        if(player)
        {
            //Se guarda una referencia al script PlayerEnergy
            playerEnergy = player.GetComponent<PlayerEnergy>();
        }
        else
        {
            Debug.LogWarning("El jugador no ha sido encontrado");
        }
    }

    void Update()
    {
        //Se verifica que la referencia al script PlayerEnergy exista
        if(playerEnergy)
        {
            //Llena la barra dependiendo de cuanta energia halla
            //* Utiliza la función getEnergy en el script PlayerEnergy
            energyBar.fillAmount = playerEnergy.getEnergy() / 100;
        }
        else
        {
            Debug.LogError("La referencia hacia el script PlayerEnergy esta vacia");
        }
    }
}