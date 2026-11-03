using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StoryMenu : MonoBehaviour
{
    ProgressDirector progressDirector;

    private GameObject _progressDirector;

    public GameObject storyMenu;

    public TMP_ColorGradient[] gradients;
    public Button[] levels;
    public TMP_Text[] levelText;

    void Start()
    {
        _progressDirector = GameObject.FindWithTag("ProgressDirector");
        progressDirector = _progressDirector.GetComponent<ProgressDirector>();
        LevelEx();
        LevelExe();
        RefreshAspects();
    }

    void Update()
    {
        if (progressDirector.level_11 == true && storyMenu == true)
        {
            levels[10].gameObject.SetActive(true);
        }
        else
        {
            levels[10].gameObject.SetActive(false);
        }

        if (progressDirector.level_12 == true && storyMenu == true)
        {
            levels[11].gameObject.SetActive(true);
        }
        else
        {
            levels[11].gameObject.SetActive(false);
        }
    }

    public void RefreshAspects()
    {
        for(int i = 0; i < 12; i++)
        {
            Aspect(i);
        }
    }

    public void Aspect(int i)
    {
        switch(progressDirector.stateLevels[i])
        {
            case 1:
                {
                    levelText[i].colorGradientPreset = gradients[0];
                }
                break;
            case 2:
                {
                    levelText[i].colorGradientPreset = gradients[1];
                }
                break;
            case 3:
                {
                    levelText[i].colorGradientPreset = gradients[2];
                }
                break;
            case 4:
                {
                    levelText[i].colorGradientPreset = gradients[3];
                }
                break;
        }
    }

    public void LevelEx()
    {
        switch(progressDirector.levelEx)
        {
            case 1:
                {
                    levelText[12].colorGradientPreset = gradients[0];
                }
                break;
            case 2:
                {
                    levelText[12].colorGradientPreset = gradients[1];
                }
                break;
            case 3:
                {
                    levelText[12].colorGradientPreset = gradients[2];
                }
                break;
            case 4:
                {
                    levelText[12].colorGradientPreset = gradients[3];
                }
                break;
        }
    }

    public void LevelExe()
    {
        switch(progressDirector.levelExe)
        {
            case 1:
                {
                    levelText[13].colorGradientPreset = gradients[0];
                }
                break;
            case 2:
                {
                    levelText[13].colorGradientPreset = gradients[1];
                }
                break;
            case 3:
                {
                    levelText[13].colorGradientPreset = gradients[2];
                }
                break;
            case 4:
                {
                    levelText[13].colorGradientPreset = gradients[3];
                }
                break;
        }
    }

    public void Level(int numLevel)
    {
        switch (numLevel)
        {
            case 1:
                {
                    if(progressDirector.stateLevels[0] >= 2)
                    {
                        SceneManager.LoadScene("Level-1", LoadSceneMode.Single);
                    }
                }
                break;
            case 2:
                {
                    if (progressDirector.stateLevels[1] >= 2)
                    {
                        //SceneManager.LoadScene("Level-2", LoadSceneMode.Single);
                        Debug.Log("2");
                    }
                }
                break;
            case 3:
                {
                    if (progressDirector.stateLevels[2] >= 2)
                    {
                        //SceneManager.LoadScene("Level-3", LoadSceneMode.Single);
                        Debug.Log("3");
                    }
                }
                break;
            case 4:
                {
                    if (progressDirector.stateLevels[3] >= 2)
                    {
                        //SceneManager.LoadScene("Level-4", LoadSceneMode.Single);
                        Debug.Log("4");
                    }
                }
                break;
            case 5:
                {
                    if (progressDirector.stateLevels[4] >= 2)
                    {
                        //SceneManager.LoadScene("Level-5", LoadSceneMode.Single);
                        Debug.Log("5");
                    }                      
                }
                break;
            case 6:
                {
                    if (progressDirector.stateLevels[5] >= 2)
                    {
                        //SceneManager.LoadScene("Level-6", LoadSceneMode.Single);
                        Debug.Log("6");
                    }
                }
                break;
            case 7:
                {
                    if (progressDirector.stateLevels[6] >= 2)
                    {
                        //SceneManager.LoadScene("Level-7", LoadSceneMode.Single);
                        Debug.Log("7");
                    }            
                }
                break;
            case 8:
                {
                    if (progressDirector.stateLevels[7] >= 2)
                    {
                        //SceneManager.LoadScene("Level-8", LoadSceneMode.Single);
                        Debug.Log("8");
                    }
                }
                break;
            case 9:
                {
                    if (progressDirector.stateLevels[8] >= 2)
                    {
                        //SceneManager.LoadScene("Level-9", LoadSceneMode.Single);
                        Debug.Log("9");
                    }
                }
                break;
            case 10:
                {
                    if (progressDirector.stateLevels[9] >= 2)
                    {
                        //SceneManager.LoadScene("Level-10", LoadSceneMode.Single);
                        Debug.Log("10");
                    }
                }
                break;
            case 11:
                {
                    if (progressDirector.stateLevels[10] >= 2)
                    {
                        SceneManager.LoadScene("Commander", LoadSceneMode.Single);
                        Debug.Log("11");
                    }
                }
                break;
            case 12:
                {
                    if(progressDirector.stateLevels[11] >= 2)
                    {
                        //SceneManager.LoadScene("Level-12", LoadSceneMode.Single);
                        Debug.Log("12");
                    }
                }
                break;
            case 13:
                {
                    SceneManager.LoadScene("LevelExe", LoadSceneMode.Single);
                }
                break;
        }
    }
}
