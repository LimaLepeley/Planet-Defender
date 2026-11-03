using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XD2 : MonoBehaviour
{
    public GameObject lose;
    public GameObject win;
    private GameObject player;
    private GameObject commander;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        commander = GameObject.Find("Commander");
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if (player.activeSelf == false)
            {
                lose.SetActive(true);
            }
        }

        if(commander != null)
        {
            if (commander.activeSelf == false)
            {
                win.SetActive(true);
            }
        }
    }
}
