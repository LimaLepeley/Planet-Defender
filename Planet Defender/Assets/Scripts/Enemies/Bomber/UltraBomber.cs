using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltraBomber : MonoBehaviour
{
    UIDirector uiDirector;

    public GameObject levelDirector;

    private Rigidbody2D rb;
    public Transform spawnB1;
    public Transform spawnB2;
    public Transform spawnB3;
    public GameObject explosion;
    public GameObject bombT;
    public GameObject bombX;
    private int tX;
    private int durability = 5;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        levelDirector = GameObject.FindWithTag("LevelDirector");
        uiDirector = levelDirector.GetComponent<UIDirector>();
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        durability = 5;
        rb.velocity = new Vector2(0, -0.3f);
        //Se inicia corutina de movimiento
    }

    // Update is called once per frame
    void Update()
    {
        //Función que controla la animación del objeto
        Animation();
        // Al ser destruido el objeto coloca una cantidad de bombas dependiendo de la dififcultad
        if (durability <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Stadistics.ultraBomber = Stadistics.ultraBomber++;
            tX = Random.Range(0, 2);
            if (tX == 0)
            {
                Instantiate(bombT, spawnB1.position, Quaternion.identity);
            }
            if (tX == 1)
            {
                Instantiate(bombX, spawnB1.position, Quaternion.identity);
            }
            tX = Random.Range(0, 2);
            if (tX == 0)
            {
                Instantiate(bombT, spawnB2.position, Quaternion.identity);
            }
            if (tX == 1)
            {
                Instantiate(bombX, spawnB2.position, Quaternion.identity);
            }
            tX = Random.Range(0, 2);
            if (tX == 0)
            {
                Instantiate(bombT, spawnB2.position, Quaternion.identity);
            }
            if (tX == 1)
            {
                Instantiate(bombX, spawnB2.position, Quaternion.identity);
            }
            tX = Random.Range(0, 2);
            if (tX == 0)
            {
                Instantiate(bombT, spawnB2.position, Quaternion.identity);
            }
            if (tX == 1)
            {
                Instantiate(bombX, spawnB2.position, Quaternion.identity);
            }
        }

        if (transform.position.y < screenBounds.y * -1.1f)
        {
            uiDirector.energyFieldPower = uiDirector.energyFieldPower - 1;
            Destroy(this.gameObject);
        }

    }

    public void Animation()
    {
        //El objeto pierde 1 de durabilidad al ser impactado necesitando 2 para cambiar de fase
        if (durability == 1)
        {
            gameObject.GetComponent<Animator>().SetInteger("Durability", 1);
        }
        if (durability == 2)
        {
            gameObject.GetComponent<Animator>().SetInteger("Durability", 2);
        }
        if (durability == 3)
        {
            gameObject.GetComponent<Animator>().SetInteger("Durability", 3);
        }
        if (durability == 4)
        {
            gameObject.GetComponent<Animator>().SetInteger("Durability", 4);
        }
        if (durability == 5)
        {
            gameObject.GetComponent<Animator>().SetInteger("Durability", 5);
        }
    }

    public void Attack()
    {
        if (durability > 1)
        {
            durability = 1;
        }
        else
        {
            durability = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "L_Attack":
                {
                    Destroy(other.gameObject);
                    durability--;
                }
                break;
            case "PE_Attack":
                {
                    if (durability >= 1)
                    {
                        durability = 1;
                        Destroy(other.gameObject);
                    }
                    else
                    {
                        Destroy(other.gameObject);
                    }
                }
                break;
            case "R_Attack":
                {
                    Destroy(other.gameObject);
                    durability--;
                }
                break;
            case "IB_Attack":
                {
                    Attack();
                }
                break;
            case "BM_Attack":
                {
                    Destroy(other.gameObject);
                    Attack();
                }
                break;
            case "CB_Attack":
                {
                    Attack();
                }
                break;
        }
    }
}
