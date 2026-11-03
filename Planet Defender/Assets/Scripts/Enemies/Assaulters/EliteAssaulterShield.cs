using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteAssaulterShield : MonoBehaviour
{

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Disabled(Collider2D other)
    {
        Stadistics.shieldsBroken = Stadistics.shieldsBroken++;
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
                        Stadistics.shieldsBroken = Stadistics.shieldsBroken++;
                        gameObject.SetActive(false);
                    }
                }
                break;
            case "IP_Attack": //Ion Pulse
                {
                    Stadistics.shieldsBroken = Stadistics.shieldsBroken++;
                    gameObject.SetActive(false);
                }
                break;
            case "Player":
                {
                    other.SendMessage("Damage", 1);
                    Stadistics.shieldsBroken = Stadistics.shieldsBroken++;
                    gameObject.SetActive(false);
                }
                break;
        }
    }
}
