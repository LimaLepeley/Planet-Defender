using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackSave : MonoBehaviour
{
    //Variable para guardar el script y confirmar que no sea destruido
    public static SpecialAttackSave specialAttackSave;

    //Guarda el ataque especial escogido
    //* 1. OverchargedStinger | 2. Megaton Projectile | 3. Split Missile
    private int specialAttack;

    // Start is called before the first frame update
    void Start()
    {
        //Se hace una comprobación si el script ya existe y sí si es asi el nuevo se borra
        if (specialAttackSave == null)
        {
            specialAttackSave = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    //Getters y Setters
    public int getSpecialAttack() {
		return this.specialAttack;
	}

	public void setSpecialAttack(int specialAttack) {
		this.specialAttack = specialAttack;
	}
}
