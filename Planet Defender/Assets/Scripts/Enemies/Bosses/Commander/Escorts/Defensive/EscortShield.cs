using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortShield : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Disabled(Collider2D other)
    {
        gameObject.SetActive(false);
        Destroy(other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "L_Attack":
                {
                    Disabled(other);
                }
                break;
            case "PE_Attack":
                {
                    Disabled(other);
                }
                break;
            case "R_Attack":
                {
                    Disabled(other);
                }
                break;
            case "BM_Attack": //Black Matter Missile
                {
                    Disabled(other);
                }
                break;
            case "RS_Attack": //Reforged Stringer
                {
                    Disabled(other);
                }
                break;
            case "PL_Attack": //Positronic Laser
                {
                    Disabled(other);
                }
                break;
            case "TLM_Attack": //Charged Bomb
                {
                    Disabled(other);
                }
                break;
            case "PS_Attack": //Proton Shield
                {
                    if (other.gameObject.name == "ProtonShieldGenerator(Clone)")
                    {
                        Disabled(other);
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                }
                break;
            case "IP_Attack": //Ion Pulse
                {
                    gameObject.SetActive(false);
                }
                break;
            case "Player":
                {
                    Disabled(other);
                }
                break;
        }
    }
}
