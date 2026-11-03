using UnityEngine;

public class EnergyFieldUI : MonoBehaviour
{
    //Variable para guardar la referencia del director
    private GameObject director;

    //Variable para guardar la referencia del script LevelProgressDirector
    private LevelProgressDirector levelProgressDirector;

    //Arreglo para guardar todas las versiones del UI del escudo de energia
    public Sprite[] sprites;

    //Variable que guarda una referencia a el spriteRenderer
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        //Se coloca la referencia al director
        director = GameObject.FindWithTag("LevelDirector");

        //Se verifica si el director del nivel ha sido encontrado
        if(director)
        {
            //Se coloca la referencia del script LevelProgressDirector
            levelProgressDirector = director.GetComponent<LevelProgressDirector>();
        }
        else
        {
            Debug.LogWarning("El director del nivel no ha sido encontrado");
        }

        //Se coloca la referencia del SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Se verifica que el script LevelProgressDirector exista
        if(levelProgressDirector)
        {
            //El sprite cambia con el poder de el campo de energia
            if(levelProgressDirector.getEnergyFieldPower() == 0)
            {
                spriteRenderer.sprite = sprites[0];
            }
            else if(levelProgressDirector.getEnergyFieldPower() == 1)
            {
                spriteRenderer.sprite = sprites[1];
            }
            else if(levelProgressDirector.getEnergyFieldPower() == 2)
            {
                spriteRenderer.sprite = sprites[2];
            }
            else if(levelProgressDirector.getEnergyFieldPower() == 3)
            {
                spriteRenderer.sprite = sprites[3];
            }
        }
        else
        {
            Debug.LogError("La referencia al script LevelProgressDirector esta vacio");
        }
    }
}