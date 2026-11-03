using System.Collections;
using UnityEngine;

//* Esta clase se encarga de ser solo un punto de conexión en donde multiples scripts encuentran información que necesitan
// TODO: El que se encarga de hacer la logica del nivel y acabarlo es el director de cada nivel
public class LevelProgressDirector : MonoBehaviour
{
    //Variable que lleva el progreso del nivel (cuantos enemigos ha muerto y cuantos faltan)
    private float progress = 0;

    //Variable que guarda cual es el progreso que debe alcanzar el jugador para pasar el nivel
    //* El minimo es 1 para evitar que halla problemas con el 
    private float goalProgress = 1;

    //Variable que lleva el estado del escudo de energia
    private int energyFieldPower = 3;

    //Variable que indica que el jefe ha aparecido y por lo tando de buscarse
    //*Esta variable es cambiada por el director de generación respectivo de cada nivel
    private bool bossAppeared = false;

    //Variable para determinar cuando ha aparecido un jefe
    private GameObject boss;

    //Variable que guarda el progreso con el jefe (vida restante) 
    private int bossProgress = 0;

    //Variable que guarda la vida total del jefe
    private int bossLife = 1;

    void Update()
    {
        energyFieldPower = Mathf.Clamp(energyFieldPower, 0, 3);
        
        //Cuando el jefe aparece el LevelProgressDirector lo busca
        if(bossAppeared && !boss)
        {
            boss = GameObject.FindWithTag("Boss");
            if(boss)
            {
                //TODO: Copiar la vida total del boss en bossLife
            }
        }

        if(boss)
        {
            //TODO: Copiar la vida actual del boss en bossProgress
        }
    }

    //*Los getter son usados generalmente por UI y los setters por enemigos
    public float getProgress() {
		return this.progress;
	}

	public void setProgress(float progress) {
		this.progress = progress;
	}

    public float getGoalProgress() {
		return this.goalProgress;
	}

	public void setGoalProgress(float goalProgress) {
		this.goalProgress = goalProgress;
	}

    public int getEnergyFieldPower() {
		return this.energyFieldPower;
	}

	public void setEnergyFieldPower(int energyFieldPower) {
		this.energyFieldPower = energyFieldPower;
	}

    public bool getBossAppeared() {
		return this.bossAppeared;
	}

	public void setBossAppeared(bool bossAppeared) {
		this.bossAppeared = bossAppeared;
	}

    public int getBossProgress() {
		return this.bossProgress;
	}

	public void setBossProgress(int bossProgress) {
		this.bossProgress = bossProgress;
	}

    public int getBossLife() {
		return this.bossLife;
	}

	public void setBossLife(int bossLife) {
		this.bossLife = bossLife;
	}
}