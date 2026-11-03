using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteTransporter : MonoBehaviour
{
    BossHealth bossHealth;
    EliteTransporterAttacks bossAttacks;

    //Colores de colisión
    private Color[] colors = { new Color(1, 1, 1, 1), new Color(0.9f, 0.9f, 0.9f) };

    //Variables de las particulas y sus colores
    public GameObject hitParticles;
    private GameObject hParticles;
    private Color[] hitParticlesColors = { new Color(1, 0, 0), new Color(0, 0.15f, 1), new Color(1, 0.87f, 0), new Color(0.41f, 0.41f, 0.41f), new Color(0.36f, 1, 0), new Color(1, 0.5f, 0), new Color(1, 0, 0.73f) };
    private ParticleSystem ps;

    //Variables de audio
    public AudioClip hit;
    AudioSource audioSource;

    //Variable para el Positronic Laser
    public GameObject pl_Spawner;

    //Variable health
    private int health = 120;

    // Start is called before the first frame update
    void Start()
    {
        bossAttacks = this.GetComponent<EliteTransporterAttacks>();
        bossHealth = this.GetComponent<BossHealth>();
        bossHealth.health = health;
        bossHealth.maxHealth = health;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(bossHealth.health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Hit(Collider2D other)
    {
        audioSource.PlayOneShot(hit, 0.6f);
        bossHealth.health--;
        Destroy(other.gameObject);
    }

    private IEnumerator HitAnimation()
    {
        gameObject.GetComponent<Renderer>().material.color = colors[0];
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Renderer>().material.color = colors[1];
    }

    public void HitParticles(int type, Transform other)
    {
        hParticles = (GameObject)Instantiate(hitParticles, other.position, hitParticles.transform.rotation, this.transform);
        ps = hParticles.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = hitParticlesColors[type];
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
                    
                }
                break;
            case "R_Attack":
                {
                    Hit(other);
                }
                break;
            case "BM_Attack": //Black Matter Missile
                {
                    
                }
                break;
            case "RS_Attack": //Reforged Stringer
                {
                    if (bossHealth.health <= 2)
                    {

                    }
                    else
                    {
                        Hit(other);
                    }
                }
                break;
            case "PL_Attack": //Positronic Laser
                {
                    if (bossHealth.health <= 2)
                    {
                        Instantiate(pl_Spawner, transform.position, Quaternion.identity);
                        Stadistics.commander = Stadistics.commander++;
                    }
                    else
                    {
                        Hit(other);
                    }
                }
                break;
            case "TLM_Attack": //Charged Bomb
                {
                    Hit(other);
                }
                break;
            case "IP_Attack": //Ion Pulse
                {
                    if (bossAttacks.paralyzed == true)
                    {
                        bossAttacks.paralyzedTimer = 0;
                    }
                    else
                    {
                        bossAttacks.paralyzed = true;
                    }
                }
                break;
        }
    }
}
