using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    BossHealth bossHealth;

    private GameObject _bossBar;
    private Image bossBar;

    //Colores de la barra
    public Color[] colors;

    // Start is called before the first frame update
    void Start()
    {
        _bossBar = GameObject.Find("BossHealthFull");
        bossBar = _bossBar.GetComponent<Image>();
        bossHealth = this.GetComponent<BossHealth>();
    }

    // Update is called once per frame
    void Update()
    {      
        bossBar.fillAmount = (float)bossHealth.health / (float)bossHealth.maxHealth;

        if(bossBar.fillAmount > 0.66f)
        {
            bossBar.color = colors[0]; //Color verde
        }
        if(bossBar.fillAmount <= 0.66f && bossBar.fillAmount >= 0.33f)
        {
            bossBar.color = colors[1]; //Color amarillo
        }
        if(bossBar.fillAmount < 0.33f)
        {
            bossBar.color = colors[2]; //Color rojo
        }
    }
}
