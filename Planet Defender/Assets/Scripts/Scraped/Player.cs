using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Objetos UI
    public Animator s1;
    public GameObject s1_Object;
    public Animator s2;
    public GameObject s2_Object;
    public Animator s3;
    public GameObject s3_Object;    
    public Animator t1;
    public GameObject t1_Object;
    public Image energyBar;

    //Prefabs
    public GameObject laser;
    public GameObject peLaser;
    public GameObject reinforcements;
    public GameObject blackMatterMissile;
    public GameObject reinforcedStinger;
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

    public int superType;
    private int superCapacity;
    private int superMaxCapacity;

    public GameObject superHUD;
    public Color[] color;

    //Variables  
    private float energy = 100;
    private float fTimer;
    public float peTimer;
    public bool dead;
    public bool cinematic;
    private bool pe;
    private bool usingEnergy;
    private bool usingEnergy2;
    private bool recharging;
    private bool energySound;

    public int level_Progression; //Controla cuales poderes pueden usarse en el nivel

    //Variables Life
    public int durability = 1;
    public int d = 1;

    //Variables Shield
    private int shield;

    //Variables Reinforcements
    private int reinNumLasers = 1;
    private bool reinCharge = true;
    private bool r = true;
    private float reinforcementsTimer;

    // Start is called before the first frame update
    void Start()
    {
        //_power_Up_Save = GameObject.FindWithTag("Power_Up_Save");
        //power_Up_Save = _power_Up_Save.GetComponent<Power_Up_Save>();
        //superType = power_Up_Save.superPowerUp;
        SuperCapacity();
        reinNumLasers = 0;
        audioSource = this.GetComponent<AudioSource>();
        rb = this.GetComponent<Rigidbody2D>();
        PowerUpsDisponibility();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            for(int i = 1; i < 6; i++)
            {
                Power(i);
            }
        }

        superCapacity = Mathf.Clamp(superCapacity, 0, superMaxCapacity);

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
        if (Input.GetKeyDown(KeyCode.Z) && fTimer >= (0.3f / laserMultiplier) && usingEnergy == false && cinematic == false)
        {
            Instantiate(laser, attackSpawn.position, Quaternion.identity);
            fTimer = 0;
            StartCoroutine("Laser");
        }
        //PE_LASER//
        if (Input.GetKeyDown(KeyCode.X) && energy > 15 && peTimer >= 0.3f && cinematic == false)
        {
            Instantiate(peLaser, attackSpawn.position, Quaternion.identity);
            usingEnergy2 = true;
            peTimer = 0;
            energy = energy - (15 * energyMultiplier);
            StartCoroutine("PE_Laser");
        }
        //REINFORCEMENTS//
        if (Input.GetKeyDown(KeyCode.C) && reinCharge == true && usingEnergy == false && reinNumLasers > 0 && level_Progression > 1 && cinematic == false)
        {
            GameObject h = (GameObject)Instantiate(reinforcements, transform.position + new Vector3(0, -1, 0), transform.rotation);

            Reinforcements rein = h.GetComponent<Reinforcements>();
            //rein.nLasers = reinNumLasers;

            StartCoroutine("Reinforcements", rein);
        }
        //SUPER//
        if (Input.GetKeyDown(KeyCode.V) && superCapacity >= 1 && level_Progression > 3 && cinematic == false)
        {
            switch (superType)
            {
                case 1:
                    {
                        superCapacity--;
                        Instantiate(reinforcedStinger, attackSpawn.position, Quaternion.identity);
                    }
                    break;
                case 2:
                    {
                        superCapacity--;
                        Instantiate(blackMatterMissile, attackSpawn.position, Quaternion.identity);
                    }
                    break;
                case 3:
                    {
                        superCapacity--;
                        Instantiate(ionicPulse, attackSpawn.position, Quaternion.identity);
                    }
                    break;
            }

        }

        //SUPER_POWERS//
        //IMPROVED REFRIGERATION//
        if (Input.GetKeyDown(KeyCode.A) && power_Up_1_Full == true && powerActive1 == false && level_Progression > 1 && cinematic == false)
        {
            StartCoroutine("Activation1");
        }

        //OVERCHARGE//
        if (Input.GetKeyDown(KeyCode.S) && power_Up_2_Full == true && powerActive2 == false && level_Progression > 1 && cinematic == false)
        {
            StartCoroutine("Activation2");
        }

        //REINFORCEMENTS---------------------------------------------------
        if (r == false)
        {
            reinforcementsTimer = reinforcementsTimer + Time.deltaTime;
        }

        if (reinforcementsTimer >= 15f)
        {
            r = true;
            reinCharge = true;
            t1.GetComponent<Animator>().SetBool("Full", true);
            reinforcementsTimer = 0;
        }
        //------------------------------------------------------------------


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

        if(!(peTimer >= 0.3f))
        {
            peTimer = peTimer + Time.deltaTime;
        }
        //ENERGY--------------------------------------------------------------

        if (durability <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            dead = true;
            Stadistics.deaths = Stadistics.deaths++;
            this.gameObject.SetActive(false);
        }

        if (cinematic == true)
        {
            rb.velocity = new Vector2(0, 0);
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
                    energy = energy + 1f;
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

    public void PowerUpsDisponibility()
    {
        if(level_Progression == 1)
        {
            s1_Object.SetActive(false);
            s2_Object.SetActive(false);
            s3_Object.SetActive(false);
            t1_Object.SetActive(false);
            superHUD.SetActive(false);
        }

        if(level_Progression == 2)
        {
            s3_Object.SetActive(false);
            t1_Object.SetActive(false);
            superHUD.SetActive(false);
        }

        if(level_Progression == 3)
        {
            superHUD.SetActive(false);
        }
    }

    public void SuperCapacity()
    {
        switch(superType)
        {
            case 1:
                {
                    superMaxCapacity = 6;
                    superHUD.gameObject.GetComponent<Renderer>().material.color = color[0];
                }
                break;
            case 2:
                {
                    superMaxCapacity = 3;
                    superHUD.gameObject.GetComponent<Renderer>().material.color = color[1];
                }
                break;
            case 3:
                {
                    superMaxCapacity = 3;
                    superHUD.gameObject.GetComponent<Renderer>().material.color = color[2];
                }
                break;
            default:
                {
                    superMaxCapacity = 6;
                    superHUD.gameObject.GetComponent<Renderer>().material.color = color[0];
                }
                break;
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

    //FUNCI�N DE DA�O QUE SE LLAMA EXTERNAMENTE POR OTROS OBJETOS
    public void Damage(int damage)
    {
        if (shield >= 1)
        {
            if (damage >= durability)
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

    //FUNCI�N DE DA�O DEL JUGADOR
    public void Collision(Collision2D other)
    {
        Destroy(other.gameObject);
        if (shield >= 1)
        {
            shield--;
            s3.SetInteger("Shield", shield);
        }
        else
        {
            d = 0;
        }
    }
    //ATTACKS VISIUALS COROUTINES

    private IEnumerator Laser()
    {
        gameObject.GetComponent<Animator>().SetBool("Red", true);
        yield return new WaitForSeconds(0.15f);
        gameObject.GetComponent<Animator>().SetBool("Red", false);
    }

    private IEnumerator PE_Laser()
    {
        gameObject.GetComponent<Animator>().SetBool("Blue", true);
        yield return new WaitForSeconds(0.15f);
        usingEnergy2 = false;
        gameObject.GetComponent<Animator>().SetBool("Blue", false);
    }

    private IEnumerator Reinforcements(Reinforcements rein)
    {
        /*reinCharge = false;
        t1.GetComponent<Animator>().SetBool("Full", false);
        t1.GetComponent<Animator>().SetBool("InUse", true);
        gameObject.GetComponent<Animator>().SetBool("Yellow", true);
        yield return new WaitForSeconds(0.15f);
        gameObject.GetComponent<Animator>().SetBool("Yellow", false);
        while (rein.finished == false)
        {
            yield return null;
        }
        t1.GetComponent<Animator>().SetBool("InUse", false);
        r = false;
        */
        yield return null;
    }
    //POWERS COROUTINES
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

    //INTERACCI�N DEL JUGADOR CON LOS PODERES QUE SUELTAN EL SUPLIER Y EL FREIGHTER || 1. REFRIGERATION 2. OVERCHARGE 3. SHIELDS 4. REINFORCEMENTS 5. SUPER
    public void Power(int p)
    {
        switch (p)
        {
            case 1:
                {
                    if(power_Up_1_Full == false && powerActive1 == false)
                    {
                        power_Up_1_Full = true;
                        s1.SetBool("Full", power_Up_1_Full);
                    }
                }
                break;
            case 2:
                {
                    if(power_Up_2_Full == false && powerActive2 == false)
                    {
                        power_Up_2_Full = true;
                        s2.SetBool("Full", power_Up_2_Full);
                    }
                }
                break;
            case 3:
                {
                    shield++;
                    s3.SetInteger("Shield", shield);
                }
                break;
            case 5:
                {              
                    if(superType == 1)
                    {
                        superCapacity = superCapacity + 2;
                    }
                    else
                    {
                        superCapacity++;
                    }
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
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
                    switch (other.gameObject.name)
                    {
                        case "PowerUp_1(Clone)":
                            {
                                Destroy(other.gameObject);
                                Power(1);
                            }
                            break;
                        case "PowerUp_2(Clone)":
                            {
                                Destroy(other.gameObject);
                                Power(2);
                            }
                            break;
                        case "PowerUp_3(Clone)":
                            {
                                Destroy(other.gameObject);
                                if (level_Progression > 2)
                                {  
                                    Power(3);
                                }
                            }
                            break;
                        case "PowerUp_5(Clone)":
                            {
                                Destroy(other.gameObject);
                                Power(5);
                            }
                            break;
                        default:
                            {
                                Destroy(other.gameObject);
                                Power(1);
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
