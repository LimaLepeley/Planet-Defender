using UnityEngine;

//* Los valores por defecto son expansionPercentage: 1.1f | energyPointsDepleated: 1
public class VerticalEnemyOutOfBounds : MonoBehaviour
{
    //Referencia al script EnemyInformation
    private EnemyInformation enemyInformation;

    //Referencia al script LevelProgressDirector
    private LevelProgressDirector director;

    //Variable que guarda los limites de la camara
    private Vector2 screenBounds;

    //Variable que guarda cuantos puntos del campo de energia son extraidos
    //* Esto cambia para enemigos grandes, hasta ahora solo con el Bulldozer
    private int energyPointsDepleated = 1;

    //Variable para definir el procentaje de expansión del limite de desaparición
    private float expansionPercentage = 1.1f;

    void Start()
    {
        //Se asigna la referencia al script EnemyInformation
        enemyInformation = GetComponent<EnemyInformation>();

        //Se asigna la referencia al script LevelProgressDirector
        director = GetComponent<LevelProgressDirector>();

        //Limite de mapa
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        if (transform.position.y < screenBounds.y * expansionPercentage)
        {
            //TODO: Revisar si esto se puede colocar en otro script
            if(enemyInformation) //Se verifica que la referencia al script este asignada
            {
                if(enemyInformation.getDecoration() == false) //Si el enemigo es una decoración solo sera destruido
                {
                    if(director) //Se verifica que la referencia al script este asignada
                    {
                        director.setEnergyFieldPower(director.getEnergyFieldPower() - energyPointsDepleated);
                        if (enemyInformation.getSupport() == false) //Si el enemigo es un soporte no contara para la barra de progreso
                        {
                            //Se suman los progressPoints correspondientes
                            director.setProgress(director.getProgress() + enemyInformation.getProgressPoints());
                        }
                    }
                    else
                    {
                        Debug.LogError("La referencia al script LevelProgressDirector esta vacia");
                    }
                }
                Destroy(this.gameObject);
            }
            else
            {
                Debug.LogError("La referencia al script EnemyInformation esta vacia");
            }
                
        }
    }

    //Getters y Setters
    public int getEnergyPointsDepleated() {
		return this.energyPointsDepleated;
	}

	public void setEnergyPointsDepleated(int energyPointsDepleated) {
		this.energyPointsDepleated = energyPointsDepleated;
	}

    public float getExpansionPercentage() {
		return this.expansionPercentage;
	}

	public void setExpansionPercentage(float expansionPercentage) {
		this.expansionPercentage = expansionPercentage;
	}
}