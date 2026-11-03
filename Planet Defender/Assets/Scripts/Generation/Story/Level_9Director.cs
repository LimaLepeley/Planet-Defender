using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_9Director : MonoBehaviour
{
    /*
    UIDirector uiDirector;

    public GameObject levelDirector;

    public GameObject[] entities; 
    public GameObject energyField;
    public GameObject gameOver;
    public Transform spawnR;
    public Transform spawnL;
    public static int difficulty;
    public static int energyFieldPower = 3;
    public static bool end = false;
    public bool failure;
    public string scene = "Level-9";

    // Start is called before the first frame update
    void Start()
    {
        levelDirector = GameObject.FindWithTag("LevelDirector");
        uiDirector = levelDirector.GetComponent<UIDirector>();
        Restart();
        energyField.GetComponent<Animator>().SetInteger("State", 0);
        StartCoroutine("Level_9");
    }

    void Update()
    {
        energyFieldPower = uiDirector.energyFieldPower;
        //Se acaba el juego si el jugador es destruido
        failure = Player.failure;

        if (failure == true || energyFieldPower == 0)
        {
            StartCoroutine("Fail");
        }

        //Controla la visualización de los attempts
        A();

        Player.end = end;
    }

    public void A()
    {
        switch (energyFieldPower)
        {
            case 2:
                {
                    energyField.GetComponent<Animator>().SetInteger("State", 1);
                }
                break;
            case 1:
                {
                    energyField.GetComponent<Animator>().SetInteger("State", 2);
                }
                break;
            case 0:
                {
                    energyField.GetComponent<Animator>().SetInteger("State", 3);
                }
                break;
            default:
                {
                    energyField.GetComponent<Animator>().SetInteger("State", 0);
                }
                break;
        }
    }

    public void Restart()
    {
        uiDirector.energyFieldPower = 3;
        energyFieldPower = 3;
        end = false;
        failure = false;
        Player.failure = false;
        Player.game = true;
        gameOver.SetActive(false);
    }

    private IEnumerator Level_9()
    {
        while(failure == false)
        {
            yield return new WaitForSeconds(3);
        }
    }

    private IEnumerator Fail()
    {
        yield return null;
        end = true;
        Player.game = false;
        gameOver.SetActive(true);
        yield return new WaitForSeconds(3);
        if (Input.GetKey(KeyCode.Z))
        {
            Restart();
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
    }
    */
}
