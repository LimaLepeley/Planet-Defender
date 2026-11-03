using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kinnector : MonoBehaviour
{
    UIDirector uiDirector;

    public GameObject levelDirector;

    public GameObject kinneticRemenant;

    private GameObject playerPosition;
    private GameObject _target;

    public Color[] color;

    public GameObject[] support;

    public GameObject explosion;
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    public int durability = 50;
    private int kinneticCharge;
    public int supportType = 0;
    private float supportTimer;
    private float supportTime;
    private bool entrance;
    private bool defenseMode;
    private bool paralyzed;

    // Start is called before the first frame update
    void Start()
    {
        levelDirector = GameObject.FindWithTag("LevelDirector");
        uiDirector = levelDirector.GetComponent<UIDirector>();
        rb = this.GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindWithTag("PlayerPosition");
        UpdateTarget();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb.velocity = new Vector2(0, -0.7f);
        StartCoroutine("Entrance");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < screenBounds.y * -1.1f)
        {
            uiDirector.energyFieldPower = uiDirector.energyFieldPower - 1;
            uiDirector.progress = uiDirector.progress + 1;
            Destroy(this.gameObject);
        }

        if (durability <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Stadistics.kinnector = Stadistics.kinnector++;
            uiDirector.progress = uiDirector.progress + 1;
            KinneticRelease();
            Destroy(this.gameObject);
        }

        if(supportTimer == supportTime)
        {
            Instantiate(support[supportType], transform.position + new Vector3(0, 2f, 0), Quaternion.identity);
        }

        Aspect();
    }

    public void Aspect()
    {
        if(defenseMode == true)
        {
            //this.GetComponent<Renderer>.material.color = color[1];
        }
        else
        {
            //this.GetComponent<Renderer>.material.color = color[0];
        }  
    }

    public void Phases() //Fases del Commander que cambian segun su vida
    {
        if (durability < 35)
        {
            supportTime = supportTime - 2;
        }

        if (durability < 20)
        {
            supportTime = supportTime - 2;
        }
    }

    private void UpdateTarget()
    {
        // If first time, create target in the left
        if (_target == null)
        {
            _target = new GameObject("Target");
            _target.transform.position = new Vector2(-3.5f, 1.5f);
            return;
        }

        // If we are in the left, change target to the right
        if (_target.transform.position.x == -3.5f)
        {
            _target.transform.position = new Vector2(3.5f, 1.5f);
        }

        // If we are in the right, change target to the left
        else if (_target.transform.position.x == 3.5f)
        {
            _target.transform.position = new Vector2(-3.5f, 1.5f);
        }
    }

    public void KinneticRelease()
    {

    }

    public void Hit(Collider2D other)
    {
        Destroy(other.gameObject);
        kinneticCharge++;
        durability--;
    }

    public void ShieldCollision(int damage)
    {
        durability = durability - damage;
    }

    private IEnumerator Entrance() //Entrada del Commander
    {
        while (transform.position.y > (screenBounds.y - 1.2f))
        {
            yield return new WaitForSeconds(0.5f);
        }
        rb.velocity = new Vector2(0, 0);
        entrance = true;
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("Movement");
    }

    private IEnumerator Paralyzed()
    {
        paralyzed = true;
        kinneticCharge = 0;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(3f);
        rb.velocity = new Vector2(0, -0.7f);
        paralyzed = false;
    }

    private IEnumerator Movement()
    {
        yield return new WaitForSeconds(3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "L_Attack":
                {
                    if(defenseMode == true)
                    {
                        Hit(other);
                    }
                    else
                    {

                    }    
                }
                break;
            case "PE_Attack":
                {
                    Destroy(other.gameObject);
                }
                break;
            case "R_Attack":
                {
                    if (defenseMode == true)
                    {
                        Hit(other);
                    }
                    else
                    {

                    }
                }
                break;
            case "BM_Attack": //Black Matter Missile
                {
                    if (defenseMode == true)
                    {
                        durability = 0;
                        Destroy(other.gameObject);
                    }
                    else
                    {

                    }            
                }
                break;
            case "RS_Attack": //Reforged Stringer
                {
                    if (defenseMode == true)
                    {
                        if (durability <= 1)
                        {
                            Instantiate(explosion, transform.position, Quaternion.identity);
                            Stadistics.kinnector = Stadistics.kinnector++;
                            uiDirector.progress = uiDirector.progress + 1;
                            Destroy(this.gameObject);
                        }
                        else
                        {
                            Hit(other);
                        }
                    }
                    else
                    {

                    }
                }
                break;
            case "PL_Attack": //Positronic Laser
                {
                    if (durability <= 1)
                    {
                        Destroy(other.gameObject);
                        Instantiate(explosion, transform.position, Quaternion.identity);
                        Stadistics.kinnector = Stadistics.kinnector++;
                        uiDirector.progress = uiDirector.progress + 1;
                        Destroy(this.gameObject);
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
