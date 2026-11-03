using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_1Director : MonoBehaviour
{
    //Referencias a objetos necesarios
    Player player;

    private GameObject _player;

    UIDirector uiDirector;

    private GameObject ui_LevelDirector;

    ProgressDirector progressDirector;

    private GameObject _progressDirector;

    PauseMenu pauseMenu;

    private GameObject _pauseMenu;

    //Varaible para determinar hay algun enemigo cargado
    private GameObject enemy;

    //Generacion de enemigos
    public GameObject[] enemies;
    public GameObject[] patterns;

    //Objectos UI
    public Image progressBar;
    public GameObject win;
    public GameObject lose;

    //Indicadores de progreso
    public int energyField = 3;
    private float progressLevel = 50f;
    private bool completed = false;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        player = _player.GetComponent<Player>();
        player.level_Progression = 1; //Determina la disponibilidad de uso de los poderes
        _progressDirector = GameObject.FindWithTag("ProgressDirector");
        progressDirector = _progressDirector.GetComponent<ProgressDirector>();
        _pauseMenu = GameObject.FindWithTag("PauseMenu");
        pauseMenu = _pauseMenu.GetComponent<PauseMenu>();
        pauseMenu.setLevel(1); //Se coloca el numero correspondiente que tiene el nivel en la build del juego
        uiDirector = GetComponent<UIDirector>();
        StartCoroutine("Level_1");
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null)
        {
            enemy = GameObject.FindWithTag("Enemy");
        }

        progressBar.fillAmount = uiDirector.progress / progressLevel;

        if (uiDirector.progress == progressLevel)
        {
            completed = true;
        }

        if (completed == true && _player.activeSelf == true && uiDirector.energyFieldPower >= 1 && enemy == null)
        {
            win.SetActive(true);
            if (uiDirector.energyFieldPower == 3)
            {
                progressDirector.stateLevels[0] = 4;
            }
            else
            {
                progressDirector.stateLevels[0] = 3;
            }
            Stadistics.gamesPlayed++;
            StartCoroutine("Ending");
        }

        if (_player.activeSelf == false || uiDirector.energyFieldPower <= 0)
        {
            StopCoroutine("Level-4");
            lose.SetActive(true);
            Stadistics.gamesPlayed++;
            StartCoroutine("Ending");
        }
    }

    private IEnumerator Level_1()
    {
        Instantiate(enemies[0], transform.position + new Vector3(0, 4f, 0), Quaternion.identity);
        yield return null;
    }

    private IEnumerator Ending()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
