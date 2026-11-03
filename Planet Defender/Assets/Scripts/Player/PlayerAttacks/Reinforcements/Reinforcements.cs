using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reinforcements : MonoBehaviour
{
    //Variable para guardar el prefab de los refuerzos
    public GameObject reinforcementsLaser;

    //Variable para determinar el número de refuerzos que se lanzan
    private int numberOfLasers = 10;

    //Variable para saber si todos los refuerzos han sido lanzados
    private bool finished = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("HelpIsOnTheWay");
    }

    //Corutina para spawnear todos los laseres de los refuerzos
    private IEnumerator HelpIsOnTheWay()
    {
        int i = 0;
        float x = 0;
        yield return new WaitForSeconds(0.5f);
        while (i < (numberOfLasers))
        {
            x = Random.Range(-10, 10) * 0.01f;
            Instantiate(reinforcementsLaser, transform.position + new Vector3(x, 0, 0), Quaternion.identity) ;
            yield return new WaitForSeconds(0.2f);
            i++;
        }
        yield return new WaitForSeconds(0.5f);
        finished = true;
        Destroy(this.gameObject);
    }

    //Getters y Setters
    public bool getFinished() {
		return this.finished;
	}

	public void setFinished(bool finished) {
		this.finished = finished;
	}
}
