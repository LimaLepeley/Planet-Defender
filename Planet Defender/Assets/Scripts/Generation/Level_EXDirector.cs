using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_EXDirector : MonoBehaviour
{//NIVEL DE PRUEBA NORMAL
    //Referencias a objetos necesarios
    Player player;

    private GameObject _player;

    UIDirector uiDirector;

    private GameObject ui_LevelDirector;

    ProgressDirector progressDirector;

    private GameObject _progressDirector;

    PauseMenu pauseMenu;

    private GameObject _pauseMenu;

    //Power_Up_Save power_Up_Save;

    //Variables para determinar hay algun enemigo o patron cargado
    private GameObject enemy;
    private GameObject pattern;

    //Generacion de enemigos
    public GameObject[] enemies;
    public GameObject[] patterns;
    public GameObject[] supportPatterns;

    //Objectos UI
    public Image progressBar;
    public GameObject win;
    public GameObject lose;

    //Indicadores de progreso
    //public int energyField = 3;
    private float progressLevel = 128f;
    private bool completed = false;

    //Variables del jugador
    public int superType;
    private int progression = 4;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        player = _player.GetComponent<Player>();
        player.level_Progression = progression; //Determina la disponibilidad de uso de los poderes   1 para solo PE y disparo normal, 2 para habilitar IMPROVED REFRIGERATION y OVERCHARGE, 3 para habilitar SHIELD y REINFORCEMENTS y 4 para habilitar todo
        _progressDirector = GameObject.FindWithTag("ProgressDirector");
        progressDirector = _progressDirector.GetComponent<ProgressDirector>();
        _pauseMenu = GameObject.FindWithTag("PauseMenu"); //Se halla al pause menu para saber que nivel tiene que reiniciar
        pauseMenu = _pauseMenu.GetComponent<PauseMenu>();
        pauseMenu.setLevel(2); //Se coloca la escena correspondiente que tiene el nivel en la build del juego
        StartCoroutine("GetSuperPower", player);
        uiDirector = GetComponent<UIDirector>();
        StartCoroutine("LevelEx");
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy == null) //Verifica si hay algun enemigo en el nivel
        {
            enemy = GameObject.FindWithTag("Enemy");
        }

        if(pattern == null) //Verifica si hay algun patron en el nivel - LOS PATRONES SE DESTRUYEN EN ESTE SCRIPT
        {
            pattern = GameObject.FindWithTag("Pattern");
        }

        progressBar.fillAmount = uiDirector.progress/progressLevel; //Llena la barra dependiendo del progreso del nivel

        if (uiDirector.progress == progressLevel) //Una vez que se llege al maximo se dara por completado el nivel
        {
            completed = true;
        }

        if(completed == true && _player.activeSelf == true && uiDirector.energyFieldPower >= 1 && enemy == null && pattern == null) //Si el nivel fue completado, el jugador sigue vivo, aun le queda energia al escudo y no hay enemigos ni patrones en el nivel
        {
            win.SetActive(true);
            if(uiDirector.energyFieldPower == 3) //Si la barrera no recibio da�o se marca al nivel como perfecto
            {
                progressDirector.levelEx = 4;
            }
            else
            {
                progressDirector.levelEx = 3; //Si la barrera fue da�ada se marca al nivel como completado
            }
            Stadistics.gamesPlayed++;
            StartCoroutine("Ending");
        }

        if(_player.activeSelf == false || uiDirector.energyFieldPower <= 0) //Si el jugador muere o la energia de la barrera llega a 0 el nivel se cuenta como perdido
        {
            StopCoroutine("LevelEx");
            lose.SetActive(true);
            Stadistics.gamesPlayed++;
            StartCoroutine("Ending");
        }
    }

    public void SpawnFreighter_R(int type, float speed)
    {
        GameObject a = (GameObject)Instantiate(enemies[6], transform.position + new Vector3(-5.5f, 1f, 0), Quaternion.identity);
        a.GetComponent<Freighter_R>().type = type;
        a.GetComponent<Freighter_R>().speed = speed;
        a.GetComponent<Rigidbody2D>().velocity = new Vector3(speed, 0, 0);
    }

    public void SpawnFreighter_L(int type, float speed)
    {
        GameObject a = (GameObject)Instantiate(enemies[7], transform.position + new Vector3(5.5f, 1f, 0), Quaternion.identity);
        a.GetComponent<Freighter_L>().type = type;
        a.GetComponent<Freighter_L>().speed = speed;
        a.GetComponent<Rigidbody2D>().velocity = new Vector3(speed, 0, 0);
    }

    public void SpawnSuplier_R(int type)
    {
        GameObject a = (GameObject)Instantiate(enemies[13], transform.position + new Vector3(-5.5f, 1f, 0), Quaternion.identity);
        a.GetComponent<Suplier_R>().type = type;
    }

    public void SpawnSuplier_L(int type)
    {
        GameObject a = (GameObject)Instantiate(enemies[14], transform.position + new Vector3(5.5f, 1f, 0), Quaternion.identity);
        a.GetComponent<Suplier_L>().type = type;
    }

    public void SoldierPattern1()
    {
        Instantiate(enemies[0], transform.position + new Vector3(-3f, 4f, 0), Quaternion.identity);      
        Instantiate(enemies[0], transform.position + new Vector3(3f, 4f, 0), Quaternion.identity);
    }

    public void SoldierPattern2()
    {
        Instantiate(enemies[0], transform.position + new Vector3(-2f, 4f, 0), Quaternion.identity);
        Instantiate(enemies[0], transform.position + new Vector3(2f, 4f, 0), Quaternion.identity);
    }

    public void Soldier_ProtectorPattern1Spawner(int s, int p, float x, float y) //0.5f Por cada enemigo 
    {
        GameObject a = (GameObject)Instantiate(patterns[0], transform.position + new Vector3(x, y, 0), Quaternion.identity);
        a.GetComponent<Soldier_ProtectorPattern1>().numSoldiers = s;
        a.GetComponent<Soldier_ProtectorPattern1>().numProtectors = p;
    }

    public void DiagonalSupportPattern(GameObject a, int type, int direction, int range)
    {
        a.GetComponent<DiagonalSupportSpawn_1>().type = type;
        a.GetComponent<DiagonalSupportSpawn_1>().direction = direction;
        a.GetComponent<DiagonalSupportSpawn_1>().range = range;
    }

    private IEnumerator LevelEx()
    {
        yield return new WaitForSeconds(2f);
        SpawnSuplier_L(5);
        yield return new WaitForSeconds(2f);
        SpawnSuplier_R(3);
        yield return new WaitForSeconds(0.5f);
        SpawnSuplier_R(4);
        yield return new WaitForSeconds(1f);
        for(int i=0; i<4; i++) //Weight = 16
        {
            SoldierPattern1();
            yield return new WaitForSeconds(2f);
            SoldierPattern2();
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(2.5f);
        Instantiate(enemies[1], transform.position + new Vector3(-1f, 4f, 0), Quaternion.identity);
        Instantiate(enemies[1], transform.position + new Vector3(1f, 4f, 0), Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Instantiate(enemies[1], transform.position + new Vector3(-1f, 4f, 0), Quaternion.identity);
        Instantiate(enemies[1], transform.position + new Vector3(1f, 4f, 0), Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Instantiate(enemies[1], transform.position + new Vector3(0, 4f, 0), Quaternion.identity);
        yield return new WaitForSeconds(1.5f);//Cuenta weight = 21
        Soldier_ProtectorPattern1Spawner(5, 1, -2, 4); //TIEMPO = 3F;
        yield return new WaitForSeconds(1.5f);
        Soldier_ProtectorPattern1Spawner(5, 1, 2, 4); //TIEMPOR // = 3F;
        yield return new WaitForSeconds(6.5f);//Cuenta weight = 33
        GameObject a = (GameObject)Instantiate(enemies[4], transform.position + new Vector3(5.5f, 2f, 0), Quaternion.identity); //TRANSPORTER_R spawnea a la derecha //Weight = 10
        while (a != null)
        {
            yield return null;
        }
        a = (GameObject)Instantiate(enemies[5], transform.position + new Vector3(-5.5f, 2f, 0), Quaternion.identity); //TRANSPORTER_L spawnea a la izquierda //Weight = 10
        while(a != null)
        {
            yield return null;
        }
        Instantiate(enemies[10], transform.position + new Vector3(4.5f, 4f, 0), Quaternion.identity);
        Instantiate(enemies[10], transform.position + new Vector3(-4.5f, 4f, 0), Quaternion.identity);
        yield return new WaitForSeconds(2f);
        Instantiate(enemies[10], transform.position + new Vector3(-2.5f, 4f, 0), Quaternion.identity);
        Instantiate(enemies[10], transform.position + new Vector3(2.5f, 4f, 0), Quaternion.identity);
        yield return new WaitForSeconds(4f);
        a = (GameObject)Instantiate(enemies[10], transform.position + new Vector3(0, 4f, 0), Quaternion.identity);//Cuenta weight = 58
        while (a != null)
        {
            yield return null;
        }
        a = (GameObject)Instantiate(enemies[8], transform.position + new Vector3(3, 4f, 0), Quaternion.identity);
        while (a != null)
        {
            yield return null;
        }
        Instantiate(enemies[8], transform.position + new Vector3(-3, 4f, 0), Quaternion.identity);
        yield return new WaitForSeconds(2f);
        a = (GameObject)Instantiate(enemies[12], transform.position + new Vector3(3, 4f, 0), Quaternion.identity);
        GameObject b = (GameObject)Instantiate(supportPatterns[0], transform.position + new Vector3(-3, 4f, 0), Quaternion.identity);
        DiagonalSupportPattern(b, 0, 0, 0);
        while (a != null)// Cuenta weight = 70
        {
            yield return null;
        }
        Destroy(b.gameObject);
        yield return new WaitForSeconds(2f);
        SpawnSuplier_L(4);
        yield return new WaitForSeconds(0.5f);
        SpawnSuplier_L(4);
        yield return new WaitForSeconds(2f);
        SpawnSuplier_R(3);
        yield return new WaitForSeconds(2f);
        for (int i=0; i<3; i++)
        {
            Instantiate(enemies[1], transform.position + new Vector3(3f, 4f, 0), Quaternion.identity);
            Instantiate(enemies[1], transform.position + new Vector3(-3f, 4f, 0), Quaternion.identity);
            yield return new WaitForSeconds(2f);
            Instantiate(enemies[3], transform.position + new Vector3(0, 4f, 0), Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(4f);//Cuenta weight = 79
        for (int i = 0; i < 3; i++)
        {
            Instantiate(enemies[2], transform.position + new Vector3(3f, 4f, 0), Quaternion.identity);
            Instantiate(enemies[2], transform.position + new Vector3(-3f, 4f, 0), Quaternion.identity);
            yield return new WaitForSeconds(2f);
            Instantiate(enemies[9], transform.position + new Vector3(0, 4f, 0), Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
        yield return new WaitForSeconds(0.5f);//Cuenta weight = 88
        Instantiate(enemies[12], transform.position + new Vector3(3.5f, 4f, 0), Quaternion.identity);
        Instantiate(enemies[12], transform.position + new Vector3(-3.5f, 4f, 0), Quaternion.identity);
        yield return new WaitForSeconds(7f);//Cuenta weight = 108
        SpawnFreighter_L(3, -3f);
        yield return new WaitForSeconds(2f);
        SpawnFreighter_L(2, -3f);
        yield return new WaitForSeconds(2f);
        SpawnFreighter_L(5, -4f);
        yield return new WaitForSeconds(5f);
        Soldier_ProtectorPattern1Spawner(1, 1, -3.5f, 4);
        Soldier_ProtectorPattern1Spawner(1, 1, 3.5f, 4);
        yield return new WaitForSeconds(5f);
        Soldier_ProtectorPattern1Spawner(1, 2, -2.5f, 4);
        Soldier_ProtectorPattern1Spawner(1, 2, 2.5f, 4);
        yield return new WaitForSeconds(7f);
        Soldier_ProtectorPattern1Spawner(1, 3, -1.5f, 4);
        Soldier_ProtectorPattern1Spawner(1, 3, 1.5f, 4);
        yield return new WaitForSeconds(4f);
        Instantiate(enemies[2], transform.position + new Vector3(0, 4f, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(enemies[2], transform.position + new Vector3(0, 4f, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.5f);//Cuenta weight = 128
        Instantiate(enemies[2], transform.position + new Vector3(0, 4f, 0), Quaternion.identity);
    }

    private IEnumerator GetSuperPower(Player player)
    {
        yield return new WaitForSeconds(0.2f);
        superType = player.superType;
    }

    private IEnumerator Ending()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
