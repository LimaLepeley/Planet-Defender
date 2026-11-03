using UnityEngine.UI;
using UnityEngine;

public class LevelProgressBarUI : MonoBehaviour
{
    //Variable de referencia al SpriteRenderer
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    //Variable para guardar la referencia del jugador
    private GameObject director;

    //Variable para la referencia al script LevelProgressDirector
    private LevelProgressDirector levelProgressDirector;

    //Barra de vida del boos
    public Image levelProgressBar;

    void Start()
    {
        //Se guarda una referencia al propio SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Se guarda una referencia hacia al director
        director = GameObject.FindWithTag("LevelDirector");

        //Se verifica si el objeto del director fue encontrado
        if(director)
        {
            //Se guarda una referencia al script LevelProgressDirector
            levelProgressDirector = director.GetComponent<LevelProgressDirector>();
        }
        else
        {
            Debug.LogWarning("El objeto del director del nivel no ha sido encontrado");
        }
    }

    void Update()
    {
        //Se verifica que el script exista
        if(levelProgressDirector)
        {
            //La barra se vacia dependiendo de cuanta vida le queda al jefe
            levelProgressBar.fillAmount = levelProgressDirector.getProgress()/levelProgressDirector.getGoalProgress();

            //Se muestra la barra cuando el jefe aparece
            if(levelProgressDirector.getBossAppeared() && spriteRenderer.enabled)
            {
                spriteRenderer.enabled = false;
            }
        }
        else
        {
            Debug.LogError("La referencia al script LevelProgressDirector esta vacia");
        }
    }
}