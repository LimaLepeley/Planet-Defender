using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulldozer : MonoBehaviour
{
    UIDirector uiDirector;

    public GameObject levelDirector;

    public GameObject[] particles;

    public GameObject[] motor_Particles;

    public Transform[] particleSpawns;

    private Color[] colors = { new Color(1, 1, 1, 1), new Color(0.9f, 0.9f, 0.9f) };


    public GameObject hitParticles;
    private GameObject hParticles;
    private Color[] hitParticlesColors = { new Color(1, 0, 0), new Color(0, 0.15f, 1), new Color(1, 0.87f, 0), new Color(0.41f, 0.41f, 0.41f), new Color(0.36f, 1, 0), new Color(1, 0.5f, 0), new Color(1, 0, 0.73f)};
    private ParticleSystem ps;

    public Sprite[] sprites;
    public Sprite[] paralyzedSprites;
    private SpriteRenderer spriteRenderer;

    public GameObject debris;

    public AudioClip hit;
    private Rigidbody2D rb;
    AudioSource audioSource;
    public GameObject explosion;
    public GameObject pl_Spawner;
    private Vector2 screenBounds;
    public int durability = 10;
    private bool paralyzed;
    public bool support;
    public bool decoration;

    // Start is called before the first frame update
    void Start()
    {
        motor_Particles[0] = (GameObject)Instantiate(particles[1], particleSpawns[0].position, particles[1].transform.rotation, this.transform);
        motor_Particles[1] = (GameObject)Instantiate(particles[0], particleSpawns[1].position, particles[0].transform.rotation, this.transform);
        motor_Particles[2] = (GameObject)Instantiate(particles[1], particleSpawns[2].position, particles[1].transform.rotation, this.transform);
        levelDirector = GameObject.FindWithTag("LevelDirector");
        uiDirector = levelDirector.GetComponent<UIDirector>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb.velocity = new Vector2(0, -0.5f);
    }


    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < screenBounds.y * -1.3f)
        {
            if(decoration == false)
            {
                uiDirector.energyFieldPower = uiDirector.energyFieldPower - 3;
                if (support == false)
                {
                    uiDirector.progress = uiDirector.progress + 10;//El bulldozer cuenta por diez
                }
            }
            Destroy(this.gameObject);
        }

        if(durability <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Stadistics.bulldozer = Stadistics.bulldozer++;
            if (support == false)
            {
                uiDirector.progress = uiDirector.progress + 10;//El bulldozer cuenta por diez
            }
            Debris();
            Destroy(this.gameObject);
        }

        Aspect();
    }

    public void Aspect()
    {
        if(paralyzed == true)
        {
            ChangeParticles(false);
            if (durability >= 9)
            {
                spriteRenderer.sprite = paralyzedSprites[0];
            }
            if (durability <= 8 && durability >= 7)
            {
                spriteRenderer.sprite = paralyzedSprites[1];
            }
            if (durability <= 6 && durability >= 5)
            {
                spriteRenderer.sprite = paralyzedSprites[2];
            }
            if (durability <= 4 && durability >= 3)
            {
                spriteRenderer.sprite = paralyzedSprites[3];
            }
            if (durability <= 2)
            {
                spriteRenderer.sprite = paralyzedSprites[4];
            }
        }
        else
        {
            if(durability >= 9)
            {
                ChangeParticles(true);
                spriteRenderer.sprite = sprites[0];
            }
            if(durability <= 8 && durability >= 7)
            {
                ChangeParticles(true);
                spriteRenderer.sprite = sprites[1];
            }
            if(durability <= 6 && durability >= 5)
            {
                ChangeParticles(true);
                spriteRenderer.sprite = sprites[2];
            }
            if(durability <= 4 && durability >= 3)
            {
                ChangeParticles(true);
                spriteRenderer.sprite = sprites[3];
            }
            if (durability <= 2)
            {
                motor_Particles[0].SetActive(false);
                motor_Particles[1].SetActive(true);
                motor_Particles[2].SetActive(true);
                spriteRenderer.sprite = sprites[4];
            }
        }
    }

    public void ChangeParticles(bool state)
    {
        for(int i = 0; i < 3; i++)
        {
            motor_Particles[i].SetActive(state);
        }
    }

    public void Debris()
    {
        GameObject a = (GameObject)Instantiate(debris, transform.position + new Vector3(-0.5f, 0, 0), transform.rotation);
        a.GetComponent<Rigidbody2D>().velocity = new Vector3(-1f, 0.5f, 0);
        GameObject b = (GameObject)Instantiate(debris, transform.position + new Vector3(0.5f, 0.2f, 0), transform.rotation);
        b.GetComponent<Rigidbody2D>().velocity = new Vector3(1.5f, 1f, 0);
        GameObject c = (GameObject)Instantiate(debris, transform.position + new Vector3(0.3f, -0.3f, 0), transform.rotation);
        c.GetComponent<Rigidbody2D>().velocity = new Vector3(0.3f, -1f, 0);
    }

    public void Hit(Collider2D other)
    {
        Destroy(other.gameObject);
        audioSource.PlayOneShot(hit, 0.4f);
        durability--;
    }

    public void HitParticles(int type, Transform other)
    {
        hParticles = (GameObject)Instantiate(hitParticles, other.position, hitParticles.transform.rotation, this.transform);
        ps = hParticles.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = hitParticlesColors[type];
    }

    public void ShieldCollision(int damage)
    {
        durability = durability - damage;
    }

    public void Support()
    {
        support = true;
    }

    private IEnumerator Paralyzed()
    {
        paralyzed = true;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(3f);
        rb.velocity = new Vector2(0, -0.5f);
        paralyzed = false;
    }

    private IEnumerator HitAnimation()
    {
        gameObject.GetComponent<Renderer>().material.color = colors[0];
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Renderer>().material.color = colors[1];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "L_Attack":
                {
                    Hit(other);
                    HitParticles(0, other.gameObject.transform);
                    StartCoroutine("HitAnimation");
                }
                break;
            case "PE_Attack":
                {
                    Destroy(other.gameObject);
                    HitParticles(1, other.gameObject.transform);
                }
                break;
            case "R_Attack":
                {
                    Hit(other);
                    HitParticles(2, other.gameObject.transform);
                    StartCoroutine("HitAnimation");
                }
                break;
            case "BM_Attack": //Black Matter Missile
                {
                    Destroy(other.gameObject);
                    audioSource.PlayOneShot(hit, 0.4f);
                    durability = 0;
                }
                break;
            case "RS_Attack": //Reforged Stringer
                {
                    if (durability <= 2)
                    {
                        audioSource.PlayOneShot(hit, 0.4f);
                        durability = 0;
                    }
                    else
                    {
                        Hit(other);
                        HitParticles(3, other.gameObject.transform);
                        StartCoroutine("HitAnimation");
                    }
                }
                break;
            case "PL_Attack": //Positronic Laser
                {
                    if(durability <= 2)
                    {
                        Destroy(other.gameObject);
                        audioSource.PlayOneShot(hit, 0.4f);
                        Instantiate(explosion, transform.position, Quaternion.identity);
                        Instantiate(pl_Spawner, transform.position, Quaternion.identity);
                        Stadistics.bulldozer = Stadistics.bulldozer++;
                        if (support == false)
                        {
                            uiDirector.progress = uiDirector.progress + 10;//El bulldozer cuenta por diez
                        }
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        Hit(other);
                        HitParticles(4, other.gameObject.transform);
                        StartCoroutine("HitAnimation");
                    }
                }
                break;
            case "TLM_Attack": //Charged Bomb
                {
                    Hit(other);
                    HitParticles(5, other.gameObject.transform);
                    StartCoroutine("HitAnimation");
                }
                break;
            case "PS_Attack": //Proton Shield
                {
                    if (other.gameObject.name == "ProtonShieldGenerator(Clone)")
                    {
                        Hit(other);
                        HitParticles(3, other.gameObject.transform);
                        StartCoroutine("HitAnimation");
                    }
                }
                break;
            case "IP_Attack": //Ion Pulse
                {
                    HitParticles(6, other.gameObject.transform);
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
                    int d = other.GetComponent<Player>().durability;

                    if(d > durability)
                    {
                        other.SendMessage("Damage", durability);
                        durability = 0;
                    }

                    if(d <= durability)
                    {
                        other.SendMessage("Damage", durability);
                        ShieldCollision(d);
                        StartCoroutine("HitAnimation");
                        HitParticles(3, other.gameObject.transform);
                    }

                    audioSource.PlayOneShot(hit, 0.4f);
                }
                break;
        }
    }
}
