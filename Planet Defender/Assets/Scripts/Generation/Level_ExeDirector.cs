using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_ExeDirector : MonoBehaviour
{ //NIVEL DE PRUEBA DE JEFE
    //Variable para guardar el objeto jugador
    private GameObject player;

    //Variable para guardar la referencia al script PlayerCollsion //* El script es del objeto Player
    private PlayerCollision playerCollision;

    //Variable para guardar la referencia al script ProgressDirector //* El script es del objeto ProgressDirector 
    private ProgressDirector progressDirector;

    //Variable para guardar la referencia al script LevelProgressDirector 
    private LevelProgressDirector levelProgressDirector;

    private GameObject _progressDirector;

    private GameObject _bossMusicDirector;

    BossMusicDirector bossMusicDirector;

    //Power_Up_Save power_Up_Save;

    //Variables para determinar hay algun enemigo o patron cargado
    private GameObject enemy;
    private GameObject pattern;

    //Generacion de enemigos
    public GameObject[] enemies;

    //Objectos UI
    public GameObject win;
    public GameObject lose;

    //Indicadores de progreso
    private float progressLevel = 1f;
    private bool levelCompleted = false;
    private bool bossCompleted = false;
    private bool completed = false;

    //Checkpoint del nivel
    private int checkpoint;

    //Variables del jefe
    public GameObject boss;

    //Variables del jugador
    private int progression = 4;

    // Start is called before the first frame update
    void Start()
    {
        //Se guardan las referencia hacia el objeto Player y su script PlayerCollision
        //* Para detectar cuando el jugador es destruido
        player = GameObject.FindWithTag("Player");
        playerCollision = player.GetComponent<PlayerCollision>();

        //Se guardan las referencias hacia el objeto ProgressDirector y su script
        _progressDirector = GameObject.FindWithTag("ProgressDirector");
        progressDirector = _progressDirector.GetComponent<ProgressDirector>();
        checkpoint = progressDirector.getCheck_Exe();

        _bossMusicDirector = GameObject.FindWithTag("BossMusicDirector");
        bossMusicDirector = _bossMusicDirector.GetComponent<BossMusicDirector>();
        StartCoroutine("GetSuperPower", player);
        //StartCoroutine("LevelExe");
    }

    // Update is called once per frame
    void Update()
    {   
        if (enemy == null) //Verifica si hay algun enemigo en el nivel
        {
            enemy = GameObject.FindWithTag("Enemy");
        }

        if (pattern == null) //Verifica si hay algun patron en el nivel - LOS PATRONES SE DESTRUYEN EN ESTE SCRIPT
        {
            pattern = GameObject.FindWithTag("Pattern");
        }
        
        //Se comprueba si la parte del nivel no se ha completado o en su defecto si hay un checkpoint
        if (levelProgressDirector.getProgress() == levelProgressDirector.getGoalProgress() && levelCompleted == false) //Una vez que se llege al maximo se dara por completado el nivel
        {
            levelCompleted = true;
        }
        else
        {
            if(checkpoint == 1)
            {
                levelCompleted = true;
            }
        }

        //Se comprueba si el nivel se ha completado y el jefe ha sido destruido
        if(bossCompleted == true && levelCompleted == true)
        {
            completed = true;
        }

        if (completed == true && player.activeSelf == true && levelProgressDirector.getEnergyFieldPower() >= 1 && enemy == null && pattern == null) //Si el nivel fue completado, el jugador sigue vivo, aun le queda energia al escudo y no hay enemigos ni patrones en el nivel
        {
            win.SetActive(true);
            if (levelProgressDirector.getEnergyFieldPower() == 3) //Si la barrera no recibio da�o se marca al nivel como perfecto
            {
                progressDirector.setLevelEx(4);
            }
            else
            {
                progressDirector.setLevelEx(3); //Si la barrera fue da�ada se marca al nivel como completado
            }
            Stadistics.gamesPlayed++;
            StartCoroutine("Ending");
        }

        if (player.activeSelf == false || levelProgressDirector.getEnergyFieldPower() <= 0) //Si el jugador muere o la energia de la barrera llega a 0 el nivel se cuenta como perdido
        {
            StopCoroutine("LevelEx");
            lose.SetActive(true);
            Stadistics.gamesPlayed++;
            StartCoroutine("Ending");
        }
    }

    private IEnumerator LevelExe()
    {
        if(checkpoint == 0)
        {
            Instantiate(enemies[0], transform.position + new Vector3(0, 4f, 0), Quaternion.identity);
            yield return null;
        }
        while(levelCompleted == false)
        {
            yield return null;
        }
        if(checkpoint == 0)
        {
            checkpoint = 1;
            progressDirector.check_Exe = 1;
        }
        //player.cinematic = true;
        yield return new WaitForSeconds(0.7f);
        boss = (GameObject)Instantiate(enemies[3], transform.position + new Vector3(0, 4f, 0), Quaternion.identity);
        boss.GetComponent<EliteTransporterAttacks>().start = true;
        bossMusicDirector.start = true;
        StartCoroutine(bossMusicDirector.ChangePhase(0));
        while(boss.GetComponent<EliteTransporterAttacks>().entrance == true)
        {
            yield return null;
        }
        bossMusicDirector.start = false;
        //player.cinematic = false;
        while (boss.GetComponent<EliteTransporterAttacks>().changingPhase == false)
        {
            yield return null;
        }
        bossMusicDirector.start = true;
        StartCoroutine(bossMusicDirector.ChangePhase(1));
        while(boss.GetComponent<EliteTransporterAttacks>().changingPhase == true)
        {
            yield return null;
        }
        bossMusicDirector.start = false;
        while (boss.GetComponent<EliteTransporterAttacks>().changingPhase == false)
        {
            yield return null;
        }
        bossMusicDirector.start = true;
        StartCoroutine(bossMusicDirector.ChangePhase(2));
        while (boss.GetComponent<EliteTransporterAttacks>().changingPhase == true)
        {
            yield return null;
        }
        bossMusicDirector.start = false;
        while (boss != null)
        {
            yield return null;
        }
        bossCompleted = true;
        bossMusicDirector.start = true;
        StartCoroutine(bossMusicDirector.ChangePhase(3));
        progressDirector.check_Exe = 0;

    }

    private IEnumerator Ending()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
