using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Executor : MonoBehaviour
{
    UIDirector uiDirector;

    public GameObject levelDirector;

    private Rigidbody2D rb;
    public GameObject explosion;
    public GameObject[] pLasers;
    public GameObject[] lasers;
    private Vector2 screenBounds;
    private bool entrance;
    private bool paralyzed;
    public bool support;
    public bool decoration;

    // Start is called before the first frame update
    void Start()
    {
        levelDirector = GameObject.FindWithTag("LevelDirector");
        uiDirector = levelDirector.GetComponent<UIDirector>();
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb.velocity = new Vector2(0, -0.7f);
        StartCoroutine("Entrance");
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public void Shoot()
    {
        Instantiate(lasers[0], transform.position + new Vector3(0, -0.25f), Quaternion.identity);
        Instantiate(lasers[1], transform.position + new Vector3(0, -0.25f), Quaternion.identity);
        Instantiate(lasers[2], transform.position + new Vector3(0, -0.25f), Quaternion.identity);
    }

    public void OneShotAttack(Collider2D other)
    {
        Destroy(other.gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Stadistics.executor = Stadistics.executor++;
        if (support == false)
        {
            uiDirector.progress = uiDirector.progress + 1;
        }
        Destroy(this.gameObject);
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

    private IEnumerator Entrance()
    {
        rb.velocity = new Vector2(0, -0.7f);
        yield return new WaitForSeconds(2.5f);
        entrance = true;
        yield return StartCoroutine("Attack");
    }

    private IEnumerator Attack()
    {
        if(transform.position.y < screenBounds.y  - 0.3f)
        {
            rb.velocity = new Vector2(0, -0.7f);
            Shoot();
        }
        yield return new WaitForSeconds(1.2f);
        yield return StartCoroutine("Attack");
    }

    private IEnumerator Paralyzed()
    {
        if (entrance == true)
        {
            paralyzed = true;
            StopCoroutine("Attack");
            rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(3f);
            paralyzed = false;
            rb.velocity = new Vector2(0, -0.7f);
            yield return StartCoroutine("Attack");
        }
        else
        {
            paralyzed = true;
            StopCoroutine("Entrance");
            rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(3f);
            paralyzed = false;
            rb.velocity = new Vector2(0, -0.7f);
            StartCoroutine("Attack");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
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
                    Stadistics.executor= Stadistics.executor++;
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
                    Stadistics.executor = Stadistics.executor++;
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
                        Stadistics.executor = Stadistics.executor++;
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
                    Stadistics.executor = Stadistics.executor++;
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
