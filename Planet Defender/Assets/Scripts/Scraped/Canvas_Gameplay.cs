using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Gameplay : MonoBehaviour
{
    UIDirector uiDirector;

    public GameObject levelDirector;

    // Start is called before the first frame update
    void Start()
    {
        levelDirector = GameObject.FindWithTag("LevelDirector");
        uiDirector = levelDirector.GetComponent<UIDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (uiDirector.energyFieldPower)
        {
            case 0:
                {

                }
                break;
            case 1:
                {

                }
                break;
            case 2:
                {

                }
                break;
            case 3:
                {

                }
                break;
        }
    }
}
