using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*Asegurarse que cada vez que se crea una nueva estadistica hacer los respectivos getters y setters
public class Stadistics : MonoBehaviour
{
    //Variable para guardar el script y confirmar que no sea destruido
    public static Stadistics stadistics;

    //TODO: Falta colocar todas las variables privates, cambiar toda la logica de los enemigos
    //TODO: Falta implementar los ultimos 3
    public static int deaths = 0;
    public static int soldier = 0;
    public static int protector = 0;
    public static int deflector = 0;
    public static int transporter = 0;
    public static int assaulter = 0;
    public static int freighter = 0;
    public static int bulldozer = 0;
    public static int eliteAssaulter = 0;
    public static int flanker;
    public static int bomber = 0;
    public static int executor;
    public static int ultraBomber = 0;
    public static int kinnector = 0;
    public static int commander = 0;
    public static int escort = 0;
    public static int shieldsBroken = 0;
    public static int damageInflicted = 0; 
    public static int damageRecieved = 0; 
    public static int gamesPlayed = 0; 

    void Start()
    {
        //Se hace una comprobación si el script ya existe y sí si es asi el nuevo se borra
        if (stadistics == null)
        {
            stadistics = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    //Getters y Setters
}
