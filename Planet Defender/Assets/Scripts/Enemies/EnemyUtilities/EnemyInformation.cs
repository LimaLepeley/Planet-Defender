using UnityEngine;

//* Esta clase en un punto de acceso en comun para todos los enemigos que puedan aparecer como soportes (en la partida) o como decoración (o en el menu)
public class EnemyInformation : MonoBehaviour
{
    //Variable que indica si el enemigo es un soporte (No aumentara la barra de progreso al ser destruido)
    private bool support = false;

    //Variable que indica si el enemigo es una decoración (Solo se movera el resto de las acciones se veran invalidadas)
    private bool decoration = false;

	//Variable que indica cuanto se progresa al ser destruido
	//* Por ahora solo cambiaria con el Bulldozer y el transporter
	private int progressPoints = 1;

    //Getters y Setters
    public bool getSupport() {
		return this.support;
	}

	public void setSupport(bool support) {
		this.support = support;
	}

    public bool getDecoration() {
		return this.decoration;
	}

	public void setDecoration(bool decoration) {
		this.decoration = decoration;
	}

	public int getProgressPoints() {
		return this.progressPoints;
	}

	public void setProgressPoints(int progressPoints) {
		this.progressPoints = progressPoints;
	}
}