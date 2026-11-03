using UnityEngine.UI;
using UnityEngine;

public class BossBarUI : MonoBehaviour
{
    //Variable de referencia al SpriteRenderer
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    //Variable para guardar la referencia del jugador
    private GameObject director;

    //Variable para la referencia al script LevelProgressDirector
    private LevelProgressDirector levelProgressDirector;

    //Barra de vida del boos
    public Image bossBar;

    void Start()
    {
        //Se guarda una referencia al propio SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Se guarda una referencia hacia al director
        director = GameObject.FindWithTag("LevelDirector");

        //Se verifica si el director fue encontrado
        if(director)
        {
            //Se guarda una referencia al script LevelProgressDirector
            levelProgressDirector = director.GetComponent<LevelProgressDirector>();
        }
        else
        {
            Debug.LogWarning("El director del nivel no se ha encontrado");
        }     
    }

    void Update()
    {
        //Se verifica que se tiene una referencia al director de progreso 
        if(levelProgressDirector)
        {
            //La barra se vacia dependiendo de cuanta vida le queda al jefe
            bossBar.fillAmount = levelProgressDirector.getBossProgress()/levelProgressDirector.getBossLife();

            //Se muestra la barra cuando el jefe aparece
            if(levelProgressDirector.getBossAppeared() && !spriteRenderer.enabled)
            {
                spriteRenderer.enabled = true;
            }
        }
        else
        {
            Debug.LogError("La referencia al LevelProgressDirector esta vacia");
        }
    }
}