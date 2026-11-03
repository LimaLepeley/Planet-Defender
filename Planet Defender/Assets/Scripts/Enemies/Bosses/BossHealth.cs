using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth;
    public int health;

    // Update is called once per frame
    void Update()
    {
        //Se impide que la vida baje de cero
        if(health < 0)
        {
            health = 0;
        }
    }
}
