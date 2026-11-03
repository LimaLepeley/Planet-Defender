using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProtectorShield : MonoBehaviour
{
    public GameObject protector;

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
                    Disabled(other);
                }
                break;
            case "PE_Attack":
                {
                    protector.SendMessage("Disable");
                    Destroy(other.gameObject);
                }
                break;
            case "R_Attack":
                {
                    Disabled(other);
                }
                break;
            case "VM_Attack": //Void Missile
                {
                    Disabled(other);
                }
                break;
            case "RS_Attack": //Reforged Stringer
                {
                    Disabled(other);
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
