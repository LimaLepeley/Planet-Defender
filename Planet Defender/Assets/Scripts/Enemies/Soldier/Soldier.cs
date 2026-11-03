using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    UIDirector uiDirector;

    public GameObject levelDirector;

    public GameObject particle;

    //Particulas
    public GameObject[] motor_Particles;
    public Transform[] particleSpawns;

    public Sprite[] paralyzedSprite;

    private Rigidbody2D rb;
    public GameObject explosion;
    private Vector2 screenBounds;
    private bool paralyzed;
    private bool decelerated;
    public bool support;
    public bool decoration;

    //Velocidad
    private float speed = -1f;

    // Start is called before the first frame update
    void Start()
    {
        //Particulas
        motor_Particles[0] = (GameObject)Instantiate(particle, particleSpawns[0].position, new Quaternion(-1, 0, 0, 1), this.transform);
        motor_Particles[1] = (GameObject)Instantiate(particle, particleSpawns[1].position, new Quaternion(-1, 0, 0, 1), this.transform);

        //Game Director
        levelDirector = GameObject.FindWithTag("LevelDirector");
        uiDirector = levelDirector.GetComponent<UIDirector>();

        //Rigidbody
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, speed);

        //Limite de mapa
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < screenBounds.y * -1.1f)
        {
            if(decoration == false)
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
        Stadistics.soldier = Stadistics.soldier++;
        if (support == false)
        {
            uiDirector.progress = uiDirector.progress + 1;
        }
        Destroy(this.gameObject);
    }

    public void Support()
    {
        support = true;
    }

    private IEnumerator Decelerated()
    {
        decelerated = true;
        rb.velocity = new Vector2(0, speed / 2f);
        yield return new WaitForSeconds(1.5f);
        decelerated = false;
        rb.velocity = new Vector2(0, speed);
    }

    private IEnumerator Paralyzed()
    {
        paralyzed = true;
        rb.velocity = new Vector2(0, speed * 0);
        motor_Particles[0].SetActive(false);
        motor_Particles[1].SetActive(false);
        yield return new WaitForSeconds(3f);
        paralyzed = false;
        rb.velocity = new Vector2(0, speed);
        motor_Particles[0].SetActive(true);
        motor_Particles[1].SetActive(true);
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
                    if(paralyzed == false)
                    {
                        if (decelerated == true)
                        {
                            StopCoroutine("Decelerated");
                            StartCoroutine("Decelerated");
                        }
                        else
                        {
                            StartCoroutine("Decelerated");
                        }
                    } 
                }
                break;
            case "R_Attack":
                {
                    OneShotAttack(other);
                }
                break;
            case "VM_Attack": //Void Missile
                {
                    OneShotAttack(other);
                }
                break;
            case "RS_Attack": //Reforged Stringer
                {
                    Instantiate(explosion, transform.position, Quaternion.identity);
                    Stadistics.soldier = Stadistics.soldier++;
                    if (support == false)
                    {
                        uiDirector.progress = uiDirector.progress + 1;
                    }
                    Destroy(this.gameObject);
                }
                break;
            case "IP_Attack": //Ion Pulse
                {
                    if(decelerated == true)
                    {
                        StopCoroutine("Decelerated");
                        decelerated = false;
                    }
                    if(paralyzed == true)
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
                    Stadistics.soldier = Stadistics.soldier++;
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
