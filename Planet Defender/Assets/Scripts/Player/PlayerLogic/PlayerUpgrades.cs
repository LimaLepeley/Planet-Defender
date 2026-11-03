using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
  	//Variable que guarda la referencia al script PlayerAnimator
    private PlayerAnimation playerAnimation;

    //Variable que guarda la referencia al script PlayerEnergy
    private PlayerEnergy playerEnergy;

    //Variables para saber si las mejoras estan llenas (pueden ser usadas)
    //ImprovedRefrigeration
    private bool isImprovedRefrigerationFull;
    //Overcharge
    private bool isOverchargeFull;

    //Variable para saber si se estan utilizando las mejoras
    //Improved Refrigeration
    private bool isImprovedRefrigerationActive;
    //Overcharge
    private bool isOverchargeActive;

    //Variables para determinar la duración de las mejoras
    //ImprovedRefrigeration
    private float improvedRefrigerationDuration = 6f;
    //Overcharge
    private float overchargeDuration = 6f;

    //Variable para reducir el tiempo de disparo principal
    //* Normalmente es 1 y cuando se activa la mejora pasa a ser 2
    //* JAMAS cambiar esta variable a 0
    private int laserTimerReduction = 1;

    //Variable que controla cuales poderes pueden usarse en el nivel
    //* El dato es establecido por el director de cada nivel
    private int levelProgression;

    void Start()
    {
        //Se guarda una referencia al script PlayerAnimation
        playerAnimation = GetComponent<PlayerAnimation>();

        //Se guarda una referencia al script PlayerEnergy
        playerEnergy = GetComponent<PlayerEnergy>();
    }

    void Update()
    {
        //MEJORAS
        //ImprovedRefrigeration
        if(Input.GetKeyDown(KeyCode.A) && isImprovedRefrigerationFull && !isImprovedRefrigerationActive && levelProgression > 1 && !playerAnimation.getCinematic())
        {
            StartCoroutine("ImprovedRefrigerationActivation");
        }
        //Overharge
        if(Input.GetKeyDown(KeyCode.S) && isOverchargeFull && !isOverchargeActive && levelProgression > 1 && !playerAnimation.getCinematic())
        {
            StartCoroutine("OverchargeActivation");
        }
    }

    //Corutina que se llama al activar la mejora ImprovedRefrigeration
    private IEnumerator ImprovedRefrigerationActivation()
    {
        isImprovedRefrigerationActive = true;
        laserTimerReduction = 2;
        yield return new WaitForSeconds(improvedRefrigerationDuration);
        laserTimerReduction = 1;
        isImprovedRefrigerationActive = false;
    }

    //Corutina que se llama al activar la mejora Overcharge
    private IEnumerator OverchargeActivation()
    {
        isOverchargeActive = true;
        //Se llama a la función OverchargeActivation para colocar la energia en 100
        playerEnergy.OverchargeActivation();
        yield return new WaitForSeconds(overchargeDuration);
        isOverchargeActive = false;
    }

    //Getter de isImprovedRefrigerationFull
    public bool getIsImprovedRefrigerationFull() {
      return this.isImprovedRefrigerationFull;
    }

    //Setter de isImprovedRefrigerationFull
    public void setIsImprovedRefrigerationFull(bool isImprovedRefrigerationFull) {
      this.isImprovedRefrigerationFull = isImprovedRefrigerationFull;
    }

    //Getter de isOverchargeFull
    public bool getIsOverchargeFull() {
      return this.isOverchargeFull;
    }

    //Setter de isOverchargeFull
    public void setIsOverchargeFull(bool isOverchargeFull) {
      this.isOverchargeFull = isOverchargeFull;
    }

    //Getter de isImprovedRefrigerationActive
    public void setIsImprovedRefrigerationActive(bool isActive) {
      this.isImprovedRefrigerationActive = isActive;
    }

    //Setter de isImprovedRefrigerationActive
    public bool getIsImprovedRefrigerationActive() {
      return this.isImprovedRefrigerationActive;
    }

    //Setter de isOverchargeFull
    public void setIsOverchargeActive(bool isActive) {
      this.isOverchargeActive = isActive;
    }

    //Getter de isOverchargeFull
    public bool getIsOverchargeActive() {
      return this.isOverchargeActive;
    }

    //Getter de laserTimerReduction
    //* Usado por: PlayerAttacks
    public int getLaserTimerReduction() {
      return this.laserTimerReduction;
    }

    //Getters y Setters
    public int getLevelProgression() {
		  return this.levelProgression;
	  }

	  public void setLevelProgression(int levelProgression) {
		  this.levelProgression = levelProgression;
	  }
}