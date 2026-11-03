using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    //Variable que guarda la referencia al audioSource
    private AudioSource audioSource;

    //Variable que guarda la referencia al script PlayerUpgrades
    private PlayerEnergy playerEnergy;

    //Audio clip para la recarga de energia
    public AudioClip recharge;

    //Variable que el sonido de recarga solo se ejecute una vez por recarga
    private bool rechargeSound;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        if(!audioSource)
        {
            Debug.LogWarning("El AudioSource no se ha asignado correctamente");
        }
    }

    //Función que se llama por PlayerEnergy para hacer sonar el sonido de recarga
    public void RechargeSound()
    {
        //Se utilizan las dos variables por el siguiente motivo:
        //isRecharging para saber si la energia se esta recargando y rechargeSound para verificar que el sonido se ejecute solo una vez por recarga
        if(playerEnergy.getIsRecharging() && rechargeSound)
        {
            audioSource.PlayOneShot(recharge, 0.4f);
            rechargeSound = false;
        }
    }

    //Setter de rechargeSound
    //* Usado por PlayerEnergy
    public void setRechargeSound(bool rechargeSound) {
		this.rechargeSound = rechargeSound;
	}
}