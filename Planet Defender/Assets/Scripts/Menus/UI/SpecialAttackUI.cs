using UnityEngine;

public class SpecialAttackUI : MonoBehaviour
{
    //Variable que guarda la referencia al componente SpriteRenderer
    private SpriteRenderer spriteRenderer;

    //Variable que guarda los distintos sprites que puede usar el UI 3 para cada ataque especial
    public Sprite[] sprites;

    //Variables para obtener cual es el poder escogido
    private SpecialAttackSave specialAttackSave;
    private GameObject _specialSave;

    //Variable para guardar cual es el poder escogido
    private int specialType;

    void Start()
    {
        //Se asigna el componente SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Guardado del super poder
        _specialSave = GameObject.FindWithTag("SpecialAttackSave");
        
        //Se verifica que el objeto halla sido encontrado
        if(_specialSave) 
        {
            specialAttackSave = _specialSave.GetComponent<SpecialAttackSave>();
            specialType = specialAttackSave.getSpecialAttack();
        }
        else
        {
            Debug.LogWarning("El objeto SpecialAttackSave no ha sido encontrado");
        }
    }

    void Update()
    {
        //Se cambia el sprite dependiendo el ataque especial guardado en el script SpecialAttackSave
        if(specialType == 0) //* La opción 0 esta por si la información no se carga bien
        {
            spriteRenderer.sprite = sprites[0];
        }
        else if(specialType == 1)
        {
            spriteRenderer.sprite = sprites[1];
        }
        else if(specialType == 2)
        {
            spriteRenderer.sprite = sprites[2];
        }
        else if(specialType ==3)
        {
            spriteRenderer.sprite = sprites[3];
        }
    }
}