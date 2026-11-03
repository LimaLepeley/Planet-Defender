using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderShield : MonoBehaviour
{
    public int durability;

    // Start is called before the first frame update
    void Start()
    {
        durability = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if(durability <= 0)
        {
            Disable();
        }
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "L_Attack":
                {
                    durability--;
                    Destroy(other.gameObject);
                }
                break;
            case "PE_Attack":
                {
                    durability = durability - 5;
                    Destroy(other.gameObject);
                }
                break;
            case "R_Attack":
                {
                    durability--;
                    Destroy(other.gameObject);
                }
                break;
            case "BM_Attack": //Black Matter Missile
                {
                    durability = durability - 15;
                    Destroy(other.gameObject);
                }
                break;
            case "RS_Attack": //Reforged Stringer
                {
                    durability--;
                    Destroy(other.gameObject);
                }
                break;
            case "PL_Attack": //Positronic Laser
                {
                    durability--;
                    Destroy(other.gameObject);
                }
                break;
            case "TLM_Attack": //Charged Bomb
                {
                    durability--;
                    Destroy(other.gameObject);
                }
                break;
            case "IP_Attack": //Ion Pulse
                {
                    durability = durability - 5;
                }
                break;
        }
    }
}
