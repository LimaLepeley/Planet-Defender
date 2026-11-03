using UnityEngine;

//* Este director se usa para compartir información importante hacia los demás objetos de la escena al iniciarla como al PauseMenu o al Player
public class SetUpDirector : MonoBehaviour
{
    //Variable para guardar el objeto jugador
    private GameObject player;

    //Variable para guardar la referencia al script PlayerAttacks
    private PlayerAttacks playerAttacks;

    //Variable que guarda la referencia al script PlayerUpgrades
    private PlayerUpgrades playerUpgrades;

    //Variable que guarda la referencia al script PauseMenu
    private PauseMenu pauseMenu;

    //Variable que guarda la referencia al menu de pausa
    private GameObject _pauseMenu;


    void Start()
    {
        //Se guardan la referencia hacia el objeto Player y su script PlayerAttacks y PlayerUpgrades
        player = GameObject.FindWithTag("Player");
        playerAttacks = player.GetComponent<PlayerAttacks>();
        playerUpgrades = player.GetComponent<PlayerUpgrades>();

        //Se guarda el menu de pausa para saber que nivel tiene que reiniciar
        _pauseMenu = GameObject.FindWithTag("PauseMenu"); 
        pauseMenu = _pauseMenu.GetComponent<PauseMenu>();
    }

    public void SetProgression(int progression)
    {
        //Determina la disponibilidad de uso de los poderes y mejoras
        playerAttacks.setLevelProgression(progression);
        playerUpgrades.setLevelProgression(progression);

    }

    public void SetLevelToRestart(int level)
    {
        //Se coloca la escena correspondiente que tiene el nivel en la build del juego
        pauseMenu.setLevel(level); 
    }
}