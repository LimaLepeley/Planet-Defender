using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    ProgressDirector progressDirector;

    private GameObject _progressDirector;

    public GameObject mainMenu;
    public GameObject bossRush;
    public GameObject survival;


    // Start is called before the first frame update
    void Start()
    {
        _progressDirector = GameObject.FindWithTag("ProgressDirector");
        progressDirector = _progressDirector.GetComponent<ProgressDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (progressDirector.bossRush == true)
        {
            bossRush.SetActive(true);
        }
        else
        {
            bossRush.SetActive(false);
        }

        if (progressDirector.survival == true)
        {
            survival.SetActive(true);
        }
        else
        {
            survival.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Survival()
    {
        //SceneManager.LoadScene("Survival", LoadSceneMode.Single);
        Debug.Log("Survival");
    }

    public void UltraSurvival()
    {
        //SceneManager.LoadScene("Survival", LoadSceneMode.Single);
        Debug.Log("UltraSurvival");
    }

    public void Stadistics()
    {
        //SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }
}
