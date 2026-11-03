using System.Collections;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    //Variable que guarda la referencia al script PlayerUpgrades
    private PlayerUpgrades playerUpgrades;

    //Variable que guarda la referencia al script PlayerAnimation
    private PlayerAnimation playerAnimation;

    //Variable que guarda la referencia al script PlayerEnergy
    private PlayerEnergy playerEnergy;

    //Variable que guarda el punto exacto donde spawnean los ataques
    //* Esta variable se coloca manualmente
    public Transform attackSpawn;

    //Prefabs de los ataques
    public GameObject laser;
    public GameObject energyLaser;
    public GameObject reinforcements;

    //Prefabs de los ataques especiales
    public GameObject overchargedStinger;
    public GameObject megatonProjectile;
    public GameObject splitMissile;

    //Variables de los costos de energia de los ataques que la utilizan
    private float energyLaserCost = 15f;
    private float overchargedStingerCost = 25f;
    private float megatonProjectileCost = 45f; 
    private float splitMissileCost = 20f;

    //Variables que contabilizan cuando se puede volver a disparar (timers)
    private float laserTimer = 0;
    private float energyLaserTimer = 0;
    private float reinforcementsTimer = 0;
    private float specialAttackTimer = 0;

    //Variables que establecen el tiempo entre cada disparo
    private float laserFireCooldown = 0.3f;
    private float energyLaserFireCooldown = 0.3f;
    private float reinforcementsCooldown = 10f;
    private float specialAttackFireCooldown = 0.3f;

    //Variables para saber cuando los reinforcements terminan
    private GameObject reinforcementsReference;
    //* La variable esta hecha para que sea vista por ReinforcementsUI
    //* True si no hay refuerzos spawneandose por lo que puede aumentar el contador o false si aun hay refuerzos y por lo tanto no aumenta el contador
    private bool reinforcementsFinished = true; 

    //Variables para obtener cual es el poder escogido
    private SpecialAttackSave specialAttackSave;
    private GameObject _specialSave;

    //Variable para guardar cual es el poder escogido
    private int specialType;

    //Variable que controla cuales poderes pueden usarse en el nivel
    //* El dato es establecido por el director de cada nivel
    private int levelProgression = 1;

    void Start()
    {
        //Variable que guarda la referencia al script PlayerUpgrades
        playerUpgrades = GetComponent<PlayerUpgrades>();

        //Variable que guarda la referencia al script PlayerAnimation
        playerAnimation = GetComponent<PlayerAnimation>();

        //Variable que guarda la referencia al script PlayerEnergy
        playerEnergy = GetComponent<PlayerEnergy>();

        //Guardado del super poder
        _specialSave = GameObject.FindWithTag("SpecialAttackSave");
        if(_specialSave) //Se verifica que el objeto halla sido encontrado
        {
            specialAttackSave = _specialSave.GetComponent<SpecialAttackSave>();
            specialType = specialAttackSave.getSpecialAttack();
        }
        else
        {
            Debug.LogWarning("El objeto SpecialAttackSave no ha sido encontrado");
        }
    }

    //*Los inputs del jugador son invalidos si se esta en una cinematica
    void Update()
    {   
        //Ataques del jugador
        //Laser
        if(Input.GetKeyDown(KeyCode.Z) && laserTimer >= (laserFireCooldown/playerUpgrades.getLaserTimerReduction()) && !playerEnergy.getUsingEnergyMovement() && !playerAnimation.getCinematic())
        {
            Instantiate(laser, attackSpawn.position, Quaternion.identity);
            laserTimer = 0;

            //Animación de ataque
            StartCoroutine(playerAnimation.Laser());
        }
        //Energy Laser
        //* El laser no se puede utilizar si se utiliza energia para desplazarse
        if(Input.GetKeyDown(KeyCode.X) && playerEnergy.getEnergy() > energyLaserCost &&  energyLaserTimer >= (energyLaserFireCooldown) && !playerEnergy.getUsingEnergyMovement() && !playerAnimation.getCinematic())
        {
            Instantiate(energyLaser, attackSpawn.position, Quaternion.identity);
            playerEnergy.setUsingEnergyLaser(true);
            energyLaserTimer = 0;
            //Si la mejora Overcharge esta activa no se usa energia
            if(!playerUpgrades.getIsOverchargeActive())
            {
                playerEnergy.setEnergy(playerEnergy.getEnergy() - energyLaserCost);
            }
            playerEnergy.setUsingEnergyLaser(true);

            //Animación de ataque
            StartCoroutine(playerAnimation.EnergyLaser());
        }
        //Reinforcements
        if(Input.GetKeyDown(KeyCode.C) && reinforcementsTimer >= reinforcementsCooldown && levelProgression > 1 && !playerEnergy.getUsingEnergyMovement() && !playerAnimation.getCinematic() && reinforcementsFinished)
        {
            reinforcementsReference = (GameObject)Instantiate(reinforcements, transform.position + new Vector3(0, -1, 0), transform.rotation);
            reinforcementsTimer = 0;

            StartCoroutine("WaitingReinforcementsToEnd", reinforcementsReference);

            //Animación de ataque
            StartCoroutine(playerAnimation.Reinforcements());
        }
        //Special
        //* Todos los ataques especiales requieren el uso de energia
        if(Input.GetKeyDown(KeyCode.V) && specialAttackTimer >= specialAttackFireCooldown && levelProgression > 3 && !playerEnergy.getUsingEnergyMovement() &&  !playerAnimation.getCinematic())
        {
            if(specialType == 1) //Overcharged Stinger
            {
                if(playerEnergy.getEnergy() > overchargedStingerCost)
                {
                    playerEnergy.setUsingEnergySpecial(true);

                    //Si la mejora Overcharge esta activa no se usa energia
                    if(!playerUpgrades.getIsOverchargeActive())
                    {
                        playerEnergy.setEnergy(playerEnergy.getEnergy() - overchargedStingerCost);
                    }
                    Instantiate(overchargedStinger, attackSpawn.position, Quaternion.identity);
                    playerEnergy.setUsingEnergyLaser(true);

                    //Animación de ataque
                    StartCoroutine(playerAnimation.OverchargedStinger());
                }
            }
            else if(specialType == 2) //Megaton Projectile
            {
                if(playerEnergy.getEnergy() > megatonProjectileCost)
                {
                    playerEnergy.setUsingEnergySpecial(true);

                    //Si la mejora Overcharge esta activa no se usa energia
                    if(!playerUpgrades.getIsOverchargeActive())
                    {
                        playerEnergy.setEnergy(playerEnergy.getEnergy() - megatonProjectileCost);
                    }
                    Instantiate(megatonProjectile, attackSpawn.position, Quaternion.identity);
                    playerEnergy.setUsingEnergyLaser(true);

                    //Animación de ataque
                    StartCoroutine(playerAnimation.MegatonProjectile());
                }
            }
            else if(specialType == 3) //Split Missile //TODO: Falta hacer que solo pueda haber un ataque de este tipo en la pantalla
            {
                if(playerEnergy.getEnergy() > splitMissileCost)
                {
                    playerEnergy.setUsingEnergySpecial(true);

                    //Si la mejora Overcharge esta activa no se usa energia
                    if(!playerUpgrades.getIsOverchargeActive())
                    {
                        playerEnergy.setEnergy(playerEnergy.getEnergy() - splitMissileCost);
                    }
                    Instantiate(splitMissile, attackSpawn.position, Quaternion.identity);
                    playerEnergy.setUsingEnergyLaser(true);

                    //Animación de ataque
                    StartCoroutine(playerAnimation.SplitMissile());
                }
            }
        }

        //Timers
        LaserTimer();
        EnergyLaserTimer();
        ReinforcementsTimer();
        SpecialAttackTimer();
    }

    //Función para incrementar el timer del ataque principal del jugador
    private void LaserTimer()
    {
        if(laserTimer <= (laserFireCooldown))
        {
            laserTimer += Time.deltaTime;
        }
    }

    //Función para incrementar el timer del ataque de energia del jugador
    private void EnergyLaserTimer()
    {
        if(energyLaserTimer <= energyLaserFireCooldown)
        {
            energyLaserTimer += Time.deltaTime;
        }
    }

    //Función para incrementar el timer de los refuerzos
    private void ReinforcementsTimer()
    {
        //* El contador no aumenta si aun hay refuerzos spawneandose
        if(reinforcementsTimer <= reinforcementsCooldown && !reinforcementsFinished)
        {
            reinforcementsTimer += Time.deltaTime;
        }
    }

    //Función para incrementar el timer del ataque especial del jugador
    private void SpecialAttackTimer()
    {
        if(specialAttackTimer <= specialAttackFireCooldown)
        {
            specialAttackTimer += Time.deltaTime;
        }
    }

    //Corutina que espera a que lo refuerzos lancen todos sus laseres 
    private IEnumerator WaitingReinforcementsToEnd(Reinforcements rein)
    {
        reinforcementsFinished = false;
        while (rein.getFinished() == false)
        {
            yield return null;
        }
        reinforcementsFinished = true;
    }

    //Getters y Setters
    public int getLevelProgression() {
		return this.levelProgression;
	}

	public void setLevelProgression(int levelProgression) {
		this.levelProgression = levelProgression;
	}

    public bool getReinforcementsFinished() {
		return this.reinforcementsFinished;
	}

    public float getReinforcementsTimer() {
		return this.reinforcementsTimer;
	}

	public float getReinforcementsCooldown() {
		return this.reinforcementsCooldown;
	}
}