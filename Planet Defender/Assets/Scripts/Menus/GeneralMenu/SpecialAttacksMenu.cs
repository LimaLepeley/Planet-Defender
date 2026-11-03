using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Falta cambiar los nombres de script y funciones
public class SpecialAttacksMenu : MonoBehaviour
{
    //Variable que guarda la referencia al script ProgressDirector
    private ProgressDirector progressDirector;

    //Variable que guarda la referencia al progressDirector
    private GameObject _progressDirector;

    //Variable que guarda la referencia al script PowerUpSave
    private SpecialAttackSave specialAttackSave;

    //Variable que guarda la referencia al powerUpSave
    private GameObject _specialAttackSave;

    // Start is called before the first frame update
    void Start()
    {
        //Se guarda la referencia tanto al ProgressDirector como a su script
        _progressDirector = GameObject.FindWithTag("ProgressDirector");
        progressDirector = _progressDirector.GetComponent<ProgressDirector>();

        //Se guarda la referencia tanto al PowerUpSave como a su script
        _specialAttackSave = GameObject.FindWithTag("SpecialAttackSave");
        specialAttackSave = _specialAttackSave.GetComponent<SpecialAttackSave>();
    }

    //Función que se llama cada vez que se hace click en el boton de algun ataque especial
    public void Super(int type)
    {
        if(type == 1)
        {
            specialAttackSave.setSpecialAttack(1);
        }
        else if(type == 2)
        {
            specialAttackSave.setSpecialAttack(1);
        }
        else if(type == 3)
        {
            specialAttackSave.setSpecialAttack(1);
        }
    }
}
