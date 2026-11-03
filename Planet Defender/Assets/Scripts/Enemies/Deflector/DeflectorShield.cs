using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectorShield : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject deflectorLaser;
    public GameObject deflector;
    public Transform laserPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Laser(Collider2D other)
    {
        Destroy(other.gameObject);
        Instantiate(deflectorLaser, laserPosition.position, Quaternion.identity);
        Stadistics.shieldsBroken = Stadistics.shieldsBroken++;
        gameObject.SetActive(false);
    }

    public void Disable(Collider2D other)
    {
        Stadistics.shieldsBroken = Stadistics.shieldsBroken++;
        Destroy(other.gameObject);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "L_Attack":
                {              
                    Laser(other);                  
                }
                break;
            case "PE_Attack":
                {
                    deflector.SendMessage("Disable");
                    Destroy(other.gameObject);
                }
                break;
            case "R_Attack":
                {
                    Laser(other);                   
                }
                break;
            case "BM_Attack": //Black Matter Missile
                {
                    Disable(other);
                }
                break;
            case "RS_Attack": //Reforged Stringer
                {
                    Disable(other);
                }
                break;
            case "PL_Attack": //Positronic Laser
                {
                    Laser(other);
                }
                break;
            case "TLM_Attack": //Charged Bomb
                {
                    if(other.gameObject.name == "The Lil Matthew(Clone)")
                    {
                        Disable(other);
                    }
                    else
                    {
                        Laser(other);
                    }                   
                }
                break;
            case "PS_Attack": //Proton Shield
                {
                    if (other.gameObject.name == "ProtonShieldGenerator(Clone)")
                    {
                        Disable(other);
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
