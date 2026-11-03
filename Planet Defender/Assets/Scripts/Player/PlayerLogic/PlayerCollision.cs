using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    //Variable que guarda la referencia al script PlayerUpgrades
    private PlayerUpgrades playerUpgrades;

    //Variable que guarda la referencia al script PlayerAnimation
    private PlayerAnimation playerAnimation;
    
    //Variable del escudo el cual le permite al jugador recibir más golpes sin morir
    //* El escudo puede tener un maximo de 3 cargas y siempre empieza en 0
    private int shield = 0;

    //Variable para que el GameDirector sepa que el jugador ha muerto
    private bool dead = false;
    
    void Update()
    {
        //Se impide que la variable shield vaya por encima de 3 y por debajo de 0
        shield = Mathf.Clamp(shield, 0, 3);

        //Se guarda una referencia al script PlayerUpgrades
        playerUpgrades = GetComponent<PlayerUpgrades>();

        //Se guarda una referencia al script PlayerAnimation
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    //* El objeto del jugador jamas es destruido sino que solo es desactivado
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if(shield > 0)
            {
                Destroy(other.gameObject);
                shield -= 1;
            }
            else
            {
                Destroy(other.gameObject);
                Destroyed();
            }
        }
        else if(other.gameObject.tag == "ReinforcementLaser")
        {
            if(shield > 0)
            {
                Destroy(other.gameObject);
                shield -= 1;
            }
            else
            {
                Destroy(other.gameObject);
                Destroyed();
            } 
        }
        else if(other.gameObject.tag == "GigaLaser")
        {
            Destroyed();
        }
        else if(other.gameObject.tag == "EnemyAttack")
        {
            if(shield > 0)
            {
                Destroy(other.gameObject);
                shield -= 1;
            }
            else
            {
                Destroy(other.gameObject);
                Destroyed();
            }
        }
        else if(other.gameObject.tag == "Upgrade")
        {
            //TODO: Cambiar todo el sistema de mejoras
            if(other.gameObject.name == "PowerUp_1(Clone)" || other.gameObject.name == "PowerUp_1")
            {
                Destroy(other.gameObject);
                AcquireUpgrade(1);
            }
            else if(other.gameObject.name == "PowerUp_2(Clone)" || other.gameObject.name == "PowerUp_2")
            {
                Destroy(other.gameObject);
                AcquireUpgrade(2);
            }
            else if(other.gameObject.name == "PowerUp_3(Clone)" || other.gameObject.name == "PowerUp_3")
            {
                Destroy(other.gameObject);
                AcquireUpgrade(3);
            }
        }
    }

    //Función que se ejecuta cuando el jugador ha sido destruido
    //* Aqui se llama a la funcion Destruction del script PlayerAnimation
    private void Destroyed()
    {
        playerAnimation.Destruction();
        dead = true;
        //TODO: Falta colocar estadisticas
        this.gameObject.SetActive(false);
    }

    //Función que se llama al coger un poder
    private void AcquireUpgrade(int type)
    {
        if(type == 1)
        {
            if(!playerUpgrades.getIsImprovedRefrigerationActive() && !playerUpgrades.getIsImprovedRefrigerationFull())
            {
                playerUpgrades.setIsImprovedRefrigerationFull(true);
            }
        }
        else if(type == 2)
        {
            if(!playerUpgrades.getIsOverchargeActive() && !playerUpgrades.getIsOverchargeFull())
            {
                playerUpgrades.setIsImprovedRefrigerationFull(true);
            }
        }
        else if(type == 3)
        {
            if(shield < 3)
            {
                shield++;
            }
        }
    }

    //Getter de la variable dead
    //* Usado por: GameDirector
    public bool getDead()
    {
        return dead;
    }

    //Setter de la variable shield
    public void setShield(int shield) {
		this.shield = shield;
	}

    //Getter de la variable shield
    public int getShield() {
		return this.shield;
	}
}