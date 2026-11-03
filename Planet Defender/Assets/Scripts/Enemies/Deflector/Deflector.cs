using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflector : MonoBehaviour
{
    UIDirector uiDirector;

    public GameObject levelDirector;

    public GameObject particle;

    public GameObject motor_Particles;

    public Transform particleSpawn;

    public Sprite[] paralyzedSprite;

    private Rigidbody2D rb;
    public GameObject explosion;
    public GameObject deflectorShield_M;
    public GameObject deflectorShield_L;
    public GameObject[] pLasers;
    private Vector2 screenBounds;
    private bool paralyzed;
    public bool support;
    public bool decoration;

    // Start is called before the first frame update
    void Start()
    {
        motor_Particles = (GameObject)Instantiate(particle, particleSpawn.position, new Quaternion(-1, 0, 0, 1), this.transform);
        levelDirector = GameObject.FindWithTag("LevelDirector");
        uiDirector = levelDirector.GetComponent<UIDirector>();
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb.velocity = new Vector2(0, -1f);
    }

    // Update is called once per frame
    void Update()
    {
        //Limite donde el objeto se destruye
        if (transform.position.y < screenBounds.y * -1.1f)
        {
            if (decoration == false)
            {
                uiDirector.energyFieldPower = uiDirector.energyFieldPower - 1;
                if (support == false)
                {
                    uiDirector.progress = uiDirector.progress + 1;
                }
            }
            Destroy(this.gameObject);
        }

        if (paralyzed == true)
        {
            GetComponent<SpriteRenderer>().sprite = paralyzedSprite[1];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = paralyzedSprite[0];
        }
    }

    public void OneShotAttack(Collider2D other)
    {
        Destroy(other.gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Stadistics.deflector = Stadistics.deflector++;
        if (support == false)
        {
            uiDirector.progress = uiDirector.progress + 1;
        }
        Disable();
        Destroy(this.gameObject);
    }

    public void Disable()
    {
        if(deflectorShield_M.activeSelf == true)
        {
            Stadistics.shieldsBroken = Stadistics.shieldsBroken++;
            deflectorShield_M.SetActive(false);
        }
        
        if(deflectorShield_L.activeSelf == true)
        {
            Stadistics.shieldsBroken = Stadistics.shieldsBroken++;
            deflectorShield_L.SetActive(false);
        }
        
    }

    public void PositronicLasers()
    {
        Instantiate(pLasers[0], transform.position, Quaternion.identity);
        Instantiate(pLasers[1], transform.position, Quaternion.identity);
        Instantiate(pLasers[2], transform.position, Quaternion.identity);
        Instantiate(pLasers[3], transform.position, Quaternion.identity);
        Instantiate(pLasers[4], transform.position, Quaternion.identity);
    }

    public void Support()
    {
        support = true;
    }

    private IEnumerator Paralyzed()
    {
        paralyzed = true;
        rb.velocity = new Vector2(0, 0);
        motor_Particles.SetActive(false);
        yield return new WaitForSeconds(3f);
        paralyzed = false;
        rb.velocity = new Vector2(0, -1f);
        motor_Particles.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "L_Attack":
                {                  
                    OneShotAttack(other);                   
                }
                break;
            case "PE_Attack":
                {
                    Destroy(other.gameObject);
                }
                break;
            case "R_Attack":
                {
                    OneShotAttack(other);
                }
                break;
            case "BM_Attack": //Black Matter Missile
                {
                    OneShotAttack(other);
                }
                break;
            case "RS_Attack": //Reforged Stringer
                {
                    Instantiate(explosion, transform.position, Quaternion.identity);
                    Stadistics.deflector = Stadistics.deflector++;
                    if (support == false)
                    {
                        uiDirector.progress = uiDirector.progress + 1;
                    }
                    Destroy(this.gameObject);
                }
                break;
            case "PL_Attack": //Positronic Laser
                {
                    Destroy(other.gameObject);
                    Instantiate(explosion, transform.position, Quaternion.identity);
                    Stadistics.deflector = Stadistics.deflector++;
                    if (support == false)
                    {
                        uiDirector.progress = uiDirector.progress + 1;
                    }
                    PositronicLasers();
                    Destroy(this.gameObject);
                }
                break;
            case "TLM_Attack": //Charged Bomb
                {
                    OneShotAttack(other);
                }
                break;
            case "PS_Attack": //Proton Shield
                {
                    if (other.gameObject.name == "ProtonShieldGenerator(Clone)")
                    {
                        OneShotAttack(other);
                    }
                    else
                    {
                        Instantiate(explosion, transform.position, Quaternion.identity);
                        Stadistics.deflector = Stadistics.deflector++;
                        if (support == false)
                        {
                            uiDirector.progress = uiDirector.progress + 1;
                        }
                        Destroy(this.gameObject);
                    }
                }
                break;
            case "IP_Attack": //Ion Pulse
                {
                    if (paralyzed == true)
                    {
                        StopCoroutine("Paralyzed");
                        StartCoroutine("Paralyzed");
                    }
                    else
                    {
                        StartCoroutine("Paralyzed");
                    }
                }
                break;
            case "Player":
                {
                    other.SendMessage("Damage", 1);
                    Instantiate(explosion, transform.position, Quaternion.identity);
                    if (support == false)
                    {
                        uiDirector.progress = uiDirector.progress + 1;
                    }
                    Destroy(this.gameObject);
                }
                break;
        }
    }
}
