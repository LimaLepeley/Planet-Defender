using UnityEngine;

//* La variable de energia se establece aca
//* Este script sirve como un punto de convergencia para guardar la información de la energia
public class PlayerEnergy : MonoBehaviour
{
    //Variable que guarda la referencia al script PlayerSound
    private PlayerSound playerSound;

    //Variable que guarda la referencia al script PlayerUpgrades
    private PlayerUpgrades playerUpgrades;

    //Variable que guarda la energia  
    private float energy = 100;

    //Variable para saber cuando se esta usando energia para moverse
    private bool usingEnergyMovement;

    //Variable que se utiliza para saber cuando se esta utilizado el laser de energia
    private bool usingEnergyLaser;

    //Variable que se usa para saber cuando se esta usando la mejora de energia
    private bool usingEnergyUpgrade;

    //Variable que usa para saber cuando se usa el ataque especial
    private bool usingEnergySpecial;

    //Variable usadas para recargar la energia
    private float energyRechargeTimer = 0;
    private float energyRechargeRate = 1f;
    private float timeForRecharge = 3.5f;
    private bool isRecharging = false;

    void Start()
    {
        //Se guarda una referencia al script PlayerUpgrades
        playerUpgrades = GetComponent<PlayerUpgrades>();

        //Se guarda una referencia al script PlayerSound
        playerSound = GetComponent<PlayerSound>();
    }

    void Update()
    {
        //Se impide que las energia baje de cero
        energy = Mathf.Clamp(energy, 0, 100);

        //Si se gasta energia o la energia esta al maximo se deja de recargar
        if(energy == 100 || usingEnergyMovement || usingEnergyLaser || usingEnergyUpgrade || usingEnergySpecial)
        {
            playerSound.setRechargeSound(true);
            isRecharging = false;
        }
    }

    void FixedUpdate()
    {
        //Si no se esta utilizando energia y la mejora de energia no esta activa el contador se reinicia
        //* No se corrobora si la energia esta al 100 porque esa comprobación se hace por dentro
        if((usingEnergyMovement || usingEnergyLaser || usingEnergyUpgrade || usingEnergySpecial) && !playerUpgrades.getIsOverchargeActive())
        {
            energyRechargeTimer = 0;
        }
        else
        {
            //Se recarga la energia solo si esta está por abajo de 100
            if(energy < 100f)
            {
                energyRechargeTimer = energyRechargeTimer + Time.deltaTime;
                //La energia se empieza a recargar después de 3.5 segundos
                if(energyRechargeTimer >= timeForRecharge)
                {
                    energy += energyRechargeRate;
                    isRecharging = true;
                }
            }
        }
    }

    //Función para hacer los cambios respectivos cuando el jugador utiliza energia para moverse más rapido
    //* Llamado desde PlayerMovement
    public void Dash()
    {
        usingEnergyMovement = true;
        //Si la mejora Overcharge esta activa no se gasta energia
        if(!playerUpgrades.getIsOverchargeActive())
        {
            energy = energy - 0.3f;
        }
    }

    //Función que se llama cuando se activa la mejora Overcharge
    //* Usado por: PlayerUpgrades
    public void OverchargeActivation()
    {
        energy = 100;
    }

    //Setter de la variable energy
    public void setEnergy(float newEnergy)
    {
        this.energy = newEnergy;
    }

    //Getter de la variable energy 
    //* Usado por: PlayerAnimation para controlar el UI
    public float getEnergy()
    {
        return energy;
    }

    //Setter de la variable usingEnergyMovement
    public void setUsingEnergyMovement(bool isUsing)
    {
        this.usingEnergyLaser = isUsing;
    }

    //Getter de la variable usingEnergyMovement
    public bool getUsingEnergyMovement()
    {
        return usingEnergyMovement;
    }

    //Setter de la variable usingEnergyLaser
    public void setUsingEnergyLaser(bool isUsing)
    {
        this.usingEnergyLaser = isUsing;
    }

    //Getter de la variable usingEnergyLaser
    public bool getUsingEnergyLaser()
    {
        return usingEnergyLaser;
    }

    //Setter de la variable usingEnergyUpgrade
    public void setUsingEnergyUpgrade(bool isUsing)
    {
        this.usingEnergyUpgrade = isUsing;
    }

    //Getter de la variable usingEnergyUpgrade
    public bool getUsingEnergyUpgrade()
    {
        return usingEnergyUpgrade;
    }

    //Setter de la variable usingEnergySpecial
    public void setUsingEnergySpecial(bool isUsing)
    {
        this.usingEnergySpecial = isUsing;
    }

    //Getter de la variable usingEnergySpecial
    public bool getUsingEnergySpecial()
    {
        return usingEnergySpecial;
    }

    //Getter de isRecharging
    //* Usado por: PlayerSound
    public bool getIsRecharging() {
		return this.isRecharging;
	}
}