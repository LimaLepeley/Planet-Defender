using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class XD : MonoBehaviour
{
    //Objetos UI
    public Animator s1;
    public Animator s2;
    public Animator s3;
    public Animator t1;
    public Image energyBar;

    //Prefabs
    public GameObject laser;
    public GameObject peLaser;
    public GameObject reinforcements;
    public GameObject blackMatterMissile;
    public GameObject reinforcedStinger;
    public GameObject positronicLaser;
    public GameObject chargedBomb;
    public GameObject protonShield;
    public GameObject ionicPulse;
    //-------------------------
    public GameObject explosion;

    //Sonidos
    AudioSource audioSource;
    public AudioClip recharge;
    

    //Otros
    private Rigidbody2D rb;
    public Transform attackSpawn;

    //Poderes
    private int energyMultiplier = 1;
    private int laserMultiplier = 1;
    private bool powerActive1;
    private bool powerActive2;
    private bool power_Up_1_Full = false;
    private bool power_Up_2_Full = false;

    //Super_Power_Up
    //Power_Up_Save power_Up_Save;

    //private GameObject _power_Up_Save;

    public int super;

    //Variables  
    private float energy = 100; 
    private float fTimer;
    private float peTimer;
    public bool dead;
    public bool cinematic;
    private bool usingEnergy;
    private bool usingEnergy2;   
    private bool recharging;
    private bool energySound;

    //Variables Life
    public int durability = 1;
    public int d = 1;

    //Variables Shield
    private int shield;

    //Variables Reinforcements
    private int reinNumLasers = 0;
    private bool r = true;
    private float reinforcementsTimer;

    // Start is called before the first frame update
    void Start()
    {
        //_power_Up_Save = GameObject.FindWithTag("Power_Up_Save");
        //power_Up_Save= _power_Up_Save.GetComponent<Power_Up_Save>();
        //super = power_Up_Save.superPowerUp;
        reinNumLasers = 0;
        audioSource = this.GetComponent<AudioSource>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        

        //ENERGIA------------------------------------------------------------------
        //Se impide que las energia baje de cero
        energy = Mathf.Clamp(energy, 0, 100);

        energyBar.fillAmount = energy / 100;
        //-------------------------------------------------------------------------

        //SHIELD-------------------------------------------------------------------
        shield = Mathf.Clamp(shield, 0, 3);

        durability = (d + shield);
        //-------------------------------------------------------------------------


        if (!(fTimer >= (0.3f / laserMultiplier)))
        {
            fTimer = fTimer + Time.deltaTime;
        }

        //Ataques del jugador
        //LASER//
        if (Input.GetKeyDown(KeyCode.Z) && fTimer >= (0.3f / laserMultiplier) && cinematic == false)
        {
            Instantiate(laser, attackSpawn.position, Quaternion.identity);
            fTimer = 0;
        }
        //PE_LASER//
        if (Input.GetKeyDown(KeyCode.X) && energy > 15 && cinematic == false)
        {
            Instantiate(peLaser, attackSpawn.position, Quaternion.identity);
            usingEnergy2 = true;
            energy = energy - (15 * energyMultiplier);
            StartCoroutine("PE_Laser");
        }
        //REINFORCEMENTS//
        if (Input.GetKeyDown(KeyCode.C) && r == true && reinNumLasers > 0 && cinematic == false)
        {
            GameObject h = (GameObject)Instantiate(reinforcements, transform.position + new Vector3(0, -1, 0), transform.rotation);

            Reinforcements rein = h.GetComponent<Reinforcements>();
            //rein.nLasers = reinNumLasers;

            StartCoroutine("Reinforcements", rein);
        }
        //SUPER//
        if (Input.GetKeyDown(KeyCode.V) && cinematic == false)
        {

        }
        //SUPER_POWERS//
        //IMPROVED REFRIGERATION//
        if (Input.GetKeyDown(KeyCode.A) && power_Up_1_Full == true && powerActive1 == false && cinematic == false)
        {
            StartCoroutine("Activation1");
        }

        //OVERCHARGE//
        if(Input.GetKeyDown(KeyCode.S) && power_Up_2_Full == true && powerActive2 == false && cinematic == false)
        {
            StartCoroutine("Activation2");
        }

        if (r == false)
        {
            reinforcementsTimer = reinforcementsTimer + Time.deltaTime;
        }

        if(reinforcementsTimer >= 15f)
        {
            r = true;
            t1.GetComponent<Animator>().SetBool("Full", true);
            reinforcementsTimer = 0;
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            //Instantiate(blackMatterMissile, attackSpawn.position, Quaternion.identity);
            //Instantiate(reinforcedStinger, attackSpawn.position, Quaternion.identity);
            //Instantiate(positronicLaser, attackSpawn.position, Quaternion.identity);
            Instantiate(chargedBomb, attackSpawn.position, Quaternion.identity);
            //Instantiate(protonShield, attackSpawn.position, Quaternion.identity);
            //Instantiate(ionicPulse, attackSpawn.position, Quaternion.identity);
        }

        //ENERGY-------------------------------------------------------------
        if (energy == 100 || usingEnergy == true || usingEnergy2 == true)
        {
            energySound = true;
            recharging = false;
        }

        if (powerActive2 == true)
        {
            energy = 100;
        }
        //ENERGY--------------------------------------------------------------

        if (durability <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            dead = true;
            Stadistics.deaths = Stadistics.deaths++;
            this.gameObject.SetActive(false);          
        }

    }

    void FixedUpdate()
    {
        //ENERGY_TOP-----------------------------------------------------------------------------
        if ((usingEnergy == true || usingEnergy2 == true) && powerActive2 == false)
        {
            peTimer = 0;
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("Blue", false);
            gameObject.GetComponent<Animator>().SetBool("Celeste", false);
            if (energy < 100f)
            {
                peTimer = peTimer + Time.deltaTime;
                if (peTimer >= 3.5f)
                {
                    recharging = true;
                    energy = energy + 0.5f;
                    RechargeSound();
                }
            }
        }
        //ENERGY_DOWN-------------------------------------------------------------------------------

        Movement();
    }

    public void Movement()
    {
        if (Input.GetKey(KeyCode.Space) && energy > 0 && cinematic == false)
        {
            if (Input.GetKey("left"))
            {
                rb.velocity = new Vector2(-6f, 0);
                Dash();

            }
            if (Input.GetKey("right"))
            {
                rb.velocity = new Vector2(6f, 0);
                Dash();
            }
            if (!Input.GetKey("left") && !Input.GetKey("right"))
            {
                rb.velocity = new Vector2(0, 0);
                usingEnergy = false;
            }
        }
        else
        {
            if (cinematic == false)
            {
                usingEnergy = false;

                if (Input.GetKey("left"))
                {
                    rb.velocity = new Vector2(-3f, 0);
                }
                if (Input.GetKey("right"))
                {
                    rb.velocity = new Vector2(3f, 0);
                }
                if (!Input.GetKey("left") && !Input.GetKey("right"))
                {
                    rb.velocity = new Vector2(0, 0);
                }
            }
        }
    }

    void RechargeSound()
    {
        if (recharging == true && energySound == true)
        {
            audioSource.PlayOneShot(recharge, 0.4f);
            energySound = false;
        }
    }

    public void Dash()
    {
        usingEnergy = true;
        energy = energy - (0.30f * energyMultiplier);
        gameObject.GetComponent<Animator>().SetBool("Celeste", true);
    }

    public void Damage (int damage)
    {
        if(shield >= 1)
        {
            if(damage >= durability)
            {
                shield = 0;
                s3.SetInteger("Shield", shield);
                d = 0;
            }
            else
            {
                shield = shield - damage;
                s3.SetInteger("Shield", shield);
            }
        }
        else
        {
            d = 0;
        }
    }

    public void Collision(Collision2D other)
    {
        Destroy(other.gameObject);
        if(shield >= 1)
        {
            shield--;
            s3.SetInteger("Shield", shield);
        }
        else
        {
            d = 0;
        }
    }
    //ATTACKS COROUTINES
    private IEnumerator PE_Laser()
    {
        gameObject.GetComponent<Animator>().SetBool("Blue", true);
        yield return new WaitForSeconds(0.3f);
        usingEnergy2 = false;
        gameObject.GetComponent<Animator>().SetBool("Blue", false);
    }

    private IEnumerator Reinforcements(Reinforcements rein)
    {
        /*t1.GetComponent<Animator>().SetBool("Full", false);
        t1.GetComponent<Animator>().SetBool("InUse", true);
        while(rein.finished == false)
        {
            yield return null;
        }
        t1.GetComponent<Animator>().SetBool("InUse", false);
        r = false;*/
        yield return null;
    }
    //POWERS COROUTINE
    private IEnumerator Activation1()
    {
        laserMultiplier = 2;
        powerActive1 = true;
        s1.SetBool("InUse", true);
        s1.SetBool("Full", false);
        yield return new WaitForSeconds(6);
        powerActive1 = false;
        laserMultiplier = 1;
        s1.SetBool("InUse", false);
        power_Up_1_Full = false;
    }

    private IEnumerator Activation2()
    {
        energyMultiplier = 0;
        powerActive2 = true;
        s2.SetBool("InUse", true);
        s2.SetBool("Full", false);
        yield return new WaitForSeconds(6);
        powerActive2 = false;
        energyMultiplier = 1;
        s2.SetBool("InUse", false);
        power_Up_2_Full = false;
    }

    public void Power(int p)
    {
        switch(p)
        {
            case 1:
                {
                    power_Up_1_Full = true;
                    s1.SetBool("Full", power_Up_1_Full);
                }
                break;
            case 2:
                {
                    power_Up_2_Full = true;
                    s2.SetBool("Full", power_Up_2_Full);
                }
                break;
            case 3:
                {
                    shield++;
                    s3.SetInteger("Shield", shield);
                }
                break;
            case 4:
                {
                    reinNumLasers++;
                    t1.SetInteger("Reach", reinNumLasers);
                }
                break;
        }
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        switch(other.gameObject.tag)
        {
            case "R_Attack":
                {
                    Collision(other); 
                }
                break;
            case "TLM_Attack":
                {
                    Destroy(other.gameObject);
                }
                break;
            case "Enemy":
                {
                    Collision(other);
                }
                break;
            case "PU":
                {
                    switch(other.gameObject.name)
                    {
                        case "PowerUp_1":
                            {
                                Destroy(other.gameObject);
                                Power(1);
                            }
                            break;
                        case "PowerUp_2":
                            {
                                Destroy(other.gameObject);
                                Power(2);
                            }
                            break;
                        case "PowerUp_3":
                            {
                                Destroy(other.gameObject);
                                Power(3);
                            }
                            break;
                        case "PowerUp_4":
                            {
                                Destroy(other.gameObject);
                                Power(4);
                            }
                            break;
                    }
                }
                break;
            case "SuperPU":
                {
                    Destroy(other.gameObject);
                    Power(5);
                }
                break;
            case "GigaLaser":
                {
                    durability = 0;
                }
                break;
            case "MiniLaser":
                {
                    Collision(other);
                }
                break;

        }
    }

}
