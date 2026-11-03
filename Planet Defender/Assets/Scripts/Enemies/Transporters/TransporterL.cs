using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterL : MonoBehaviour
{
    UIDirector uiDirector;

    public GameObject levelDirector;

    //Particulas
    public GameObject particle;
    public GameObject motor_Particles;
    public Transform particleSpawn;

    private Color[] colors = { new Color(1, 1, 1, 1), new Color(0.9f, 0.9f, 0.9f) };

    public GameObject hitParticles;
    private GameObject hParticles;
    private Color[] hitParticlesColors = { new Color(1, 0, 0), new Color(0, 0.15f, 1), new Color(1, 0.87f, 0), new Color(0.41f, 0.41f, 0.41f), new Color(0.36f, 1, 0), new Color(1, 0.5f, 0), new Color(1, 0, 0.73f) };
    private ParticleSystem ps;


    public AudioClip hit;
    AudioSource audioSource;
    private Rigidbody2D rb;
    public Transform spawnA;
    public GameObject assaulter;
    public GameObject explosion;
    public GameObject pl_Spawner;
    private Vector2 screenBounds;
    private int assault = 8;
    private int durability = 5;
    private bool entrance;
    private bool paralyzed;
    public bool support;

    // Start is called before the first frame update
    void Start()
    {
        motor_Particles = (GameObject)Instantiate(particle, particleSpawn.position, particleSpawn.rotation, this.transform);
        levelDirector = GameObject.FindWithTag("LevelDirector");
        uiDirector = levelDirector.GetComponent<UIDirector>();
        audioSource = GetComponent<AudioSource>();
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine("Entrance");
    }

    // Update is called once per frame
    void Update()
    {
        if (durability <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Stadistics.transporter = Stadistics.transporter++;
            if (support == false)
            {
                uiDirector.progress = uiDirector.progress + (assault + 2);
            }
            Destroy(this.gameObject);
        }

        if (transform.position.x > screenBounds.x * 1.4f)
        {
            if (support == false)
            {
                uiDirector.progress = uiDirector.progress + (assault + 2);
            }
            Destroy(this.gameObject);
        }

    }

    public void Hit(Collider2D other)
    {
        audioSource.PlayOneShot(hit, 0.4f);
        durability = durability - 1;
        Destroy(other.gameObject);
        StartCoroutine("HitAnimation");
    }

    public void HitParticles(int type, Transform other)
    {
        hParticles = (GameObject)Instantiate(hitParticles, other.position, hitParticles.transform.rotation, this.transform);
        ps = hParticles.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = hitParticlesColors[type];
    }

    public void Support()
    {
        support = true;
    }

    private IEnumerator Entrance()
    {
        rb.velocity = new Vector2(0.7f, 0);
        while (transform.position.x < ((screenBounds.x * -1) + 1.2f))
        {
            yield return null;
        }
        entrance = true;
        yield return StartCoroutine("Deploy");
    }

    private IEnumerator HitAnimation()
    {
        gameObject.GetComponent<Renderer>().material.color = colors[0];
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Renderer>().material.color = colors[1];
    }

    private IEnumerator Deploy()
    {
        rb.velocity = new Vector2(0.7f, 0);
        while (assault > 0)
        {
            if (transform.position.x < screenBounds.x - 0.5f)
            {
                gameObject.GetComponent<Animator>().SetBool("Deploy", true);              
                assault--;
                yield return new WaitForSeconds(0.3f);
                Instantiate(assaulter, spawnA.position, Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
                gameObject.GetComponent<Animator>().SetBool("Deploy", false);
                yield return new WaitForSeconds(0.8f);
            }
        }
        rb.velocity = new Vector2(1.5f, 0);
    }

    private IEnumerator Paralyzed()
    {
        if (entrance == true)
        {
            paralyzed = true;
            motor_Particles.SetActive(false);
            StopCoroutine("Deploy");
            rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(3f);
            paralyzed = false;
            motor_Particles.SetActive(true);
            yield return StartCoroutine("Deploy");
        }
        else
        {
            paralyzed = true;
            motor_Particles.SetActive(false);
            StopCoroutine("Entrance");
            rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(3f);
            paralyzed = false;
            motor_Particles.SetActive(true);
            yield return StartCoroutine("Deploy");
        }
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
                    audioSource.PlayOneShot(hit, 0.4f);
                    Destroy(other.gameObject);
                    durability = 0;
                }
                break;
            case "RS_Attack": //Reforged Stringer
                {
                    if (durability == 1)
                    {
                        audioSource.PlayOneShot(hit, 0.4f);
                        durability = durability - 1;
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
                    if (durability <= 1)
                    {
                        Destroy(other.gameObject);
                        audioSource.PlayOneShot(hit, 0.4f);
                        Instantiate(explosion, transform.position, Quaternion.identity);
                        Instantiate(pl_Spawner, transform.position, Quaternion.identity);
                        Stadistics.transporter = Stadistics.transporter++;
                        if (support == false)
                        {
                            uiDirector.progress = uiDirector.progress + (assault + 2);
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
        }
    }
}
