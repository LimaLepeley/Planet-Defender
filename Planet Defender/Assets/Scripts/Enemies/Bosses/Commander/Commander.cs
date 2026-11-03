using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Commander : MonoBehaviour
{
    CommanderShield commanderShield;

    private Rigidbody2D rb;
    private Vector2 screenBounds;
    public GameObject shield;
    public GameObject[] defensiveEscorts;
    public GameObject[] offensiveEscorts;
    public GameObject miniLaser;
    public GameObject rocket;
    public GameObject[] gLasers; // FALTA OPTIMIZAR - ESTABLECER VECTOR DE VELOCIDAD DESDE EL COMMANDER
    public GameObject commanderLaser;
    public GameObject gigaLaser;
    public GameObject pl_Spawner;
    private GameObject playerPosition;
    private GameObject _target;
    private int durability = 100;
    private int attackType = 2;
    private int x = 0;
    private float sTimer = 0; //Timer para regenerar el escudo cuando es desactivado //S
    private float timeForShield = 8f; //S
    private float attackTimer = 0; //Tiempo entre ataques //!
    private float timeForAttack = 15f; //!
    private float escortTimer = 0; //Tiempo entre spawn de escoltas
    private float basicAttackTimer = 0;
    private float paralyzedTimer = 0;
    private float verticalVelocity = -3f; //Velocidad para los miniLasers
    private bool entrance;
    private bool attacking;
    private bool search = false;
    private bool basicAttack = false;
    private bool secondPhase = true;
    private bool paralyzed = false;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        playerPosition = GameObject.FindWithTag("PlayerPosition");
        commanderShield = shield.GetComponent<CommanderShield>();
        UpdateTarget();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb.velocity = new Vector2(0, -1f);
        StartCoroutine("Entrance");
    }

    // Update is called once per frame
    void Update()
    {
        if(shield.activeSelf == false) //Escudo
        {
            sTimer = sTimer + Time.deltaTime;
        }

        if(sTimer >= timeForShield) //Escudo
        {
            commanderShield.durability = 15;
            shield.SetActive(true);
            sTimer = 0;
        }

        if(entrance == true && attacking == false && paralyzed == false) //Ataques
        {
            attackTimer = attackTimer + Time.deltaTime;
        }

        if(attackTimer >= timeForAttack) //Ataques
        {
            rb.velocity = new Vector2((playerPosition.transform.position.x - transform.position.x) * 0.1f, 0);
            StopCoroutine("PatrolToTarget");
            x = Random.Range(0, attackType);
            StartCoroutine("Fight", x);
        }

        if(search == true) //Busca al jugador
        {
            if (Vector2.Distance(transform.position, playerPosition.transform.position) < 0.05f)
            {
                transform.position = new Vector2(playerPosition.transform.position.x, transform.position.y);
                rb.velocity = new Vector2(0, 0);
            }
            else
            {
                if (playerPosition.transform.position.x - transform.position.x < 0)
                {
                    rb.velocity = new Vector2(-4f, 0);
                }
                if (playerPosition.transform.position.x - transform.position.x > 0)
                {
                    rb.velocity = new Vector2(4f, 0);
                }
            }
        }

        if(basicAttack == true) // Lasers
        {
            basicAttackTimer = basicAttackTimer + Time.deltaTime;
        }

        if(basicAttackTimer >= 1f) // Lasers
        {
            Instantiate(commanderLaser, transform.position + new Vector3(-0.5f, -1.5f, 0), Quaternion.identity);
            Instantiate(commanderLaser, transform.position + new Vector3(0.5f, -1.5f, 0), Quaternion.identity);
            basicAttackTimer = 0;
        }

        if(attacking == false && entrance == true && paralyzed == false) //Escoltas
        {
            escortTimer = escortTimer + Time.deltaTime;
        }

        if(escortTimer >= 15f) //Escoltas
        {
            SummonEscort();
            escortTimer = 0;
        }

        Phases();

        if(paralyzed == true)
        {
            paralyzedTimer = paralyzedTimer + Time.deltaTime;
        }

        if(paralyzedTimer >= 5f)
        {
            paralyzed = false;
        }
    }

    public void Phases() //Fases del Commander que cambian segun su vida
    {
        if(durability < 75)
        {
            attackType = 3;
        }

        if(durability < 50)
        {
            attackType = 4;
        }

        if(durability < 25)
        {
            secondPhase = true;
            timeForAttack = 7f;
            timeForShield = 4f;
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

    public void SummonEscort() //Metodo para invocar escoltas
    {
        int x = 0, y = 0;
        GameObject[] escorts;
        x = Random.Range(0, 2);

        switch(x)
        {
            case 0: //Izquierda
                {
                    escorts = GameObject.FindGameObjectsWithTag("Escort_L");
                    if(escorts.Length >= 2)
                    {
                        escorts = GameObject.FindGameObjectsWithTag("Escort_R");
                        if (escorts.Length >= 2)
                        {
                            return;
                        }
                        else
                        {
                            y = Random.Range(0, 2);
                            switch (y)
                            {
                                case 0:
                                    {
                                        Instantiate(defensiveEscorts[1], transform.position + new Vector3(0.5f, 0, 0), Quaternion.identity);
                                    }
                                    break;
                                case 1:
                                    {
                                        Instantiate(offensiveEscorts[1], transform.position + new Vector3(0.5f, 0, 0), Quaternion.identity);
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        y = Random.Range(0, 2);
                        switch(y)
                        {
                            case 0:
                                {
                                    Instantiate(defensiveEscorts[0], transform.position + new Vector3(-0.5f, 0, 0), Quaternion.identity);
                                }
                                break;
                            case 1:
                                {
                                    Instantiate(offensiveEscorts[0], transform.position + new Vector3(-0.5f, 0, 0), Quaternion.identity);
                                }
                                break;
                        }   
                    }
                }
                break;
            case 1: //Derecha
                {
                    escorts = GameObject.FindGameObjectsWithTag("Escort_R");
                    if (escorts.Length >= 2)
                    {
                        escorts = GameObject.FindGameObjectsWithTag("Escort_L");
                        if (escorts.Length >= 2)
                        {
                            return;
                        }
                        else
                        {
                            y = Random.Range(0, 2);
                            switch (y)
                            {
                                case 0:
                                    {
                                        Instantiate(defensiveEscorts[0], transform.position + new Vector3(-0.5f, 0, 0), Quaternion.identity);
                                    }
                                    break;
                                case 1:
                                    {
                                        Instantiate(offensiveEscorts[0], transform.position + new Vector3(-0.5f, 0, 0), Quaternion.identity);
                                    }
                                    break;
                            }
                        }
                    }
                    else
                    {
                        y = Random.Range(0, 2);
                        switch (y)
                        {
                            case 0:
                                {
                                    Instantiate(defensiveEscorts[1], transform.position + new Vector3(0.5f, 0, 0), Quaternion.identity);
                                }
                                break;
                            case 1:
                                {
                                    Instantiate(offensiveEscorts[1], transform.position + new Vector3(0.5f, 0, 0), Quaternion.identity);
                                }
                                break;
                        }
                    }
                }
                break;
        }  
    }

    public void Pattern_1() //a , b, c, d, e, f, g, PATRON DE LOS MINILASERS 
    {
        GameObject a = (GameObject)Instantiate(miniLaser, transform.position, transform.rotation);

        a.GetComponent<Rigidbody2D>().velocity = new Vector3(-3f, verticalVelocity, 0);

        GameObject b = (GameObject)Instantiate(miniLaser, transform.position, transform.rotation);

        b.GetComponent<Rigidbody2D>().velocity = new Vector3(-2f, verticalVelocity, 0);

        GameObject c = (GameObject)Instantiate(miniLaser, transform.position, transform.rotation);

        c.GetComponent<Rigidbody2D>().velocity = new Vector3(-1f, verticalVelocity, 0);

        GameObject d = (GameObject)Instantiate(miniLaser, transform.position, transform.rotation);

        d.GetComponent<Rigidbody2D>().velocity = new Vector3(0, verticalVelocity, 0);

        GameObject e = (GameObject)Instantiate(miniLaser, transform.position, transform.rotation);

        e.GetComponent<Rigidbody2D>().velocity = new Vector3(1f, verticalVelocity, 0);

        GameObject f = (GameObject)Instantiate(miniLaser, transform.position, transform.rotation);

        f.GetComponent<Rigidbody2D>().velocity = new Vector3(2f, verticalVelocity, 0);
            
        GameObject g = (GameObject)Instantiate(miniLaser, transform.position, transform.rotation);

        g.GetComponent<Rigidbody2D>().velocity = new Vector3(3f, verticalVelocity, 0);
    }

    public void Pattern_2()// h, i, j, k, l, m, PATRON DE LOS MINILASERS
    {
        GameObject h = (GameObject)Instantiate(miniLaser, transform.position, transform.rotation);

        h.GetComponent<Rigidbody2D>().velocity = new Vector3(-2.5f, verticalVelocity, 0);

        GameObject i = (GameObject)Instantiate(miniLaser, transform.position, transform.rotation);

        i.GetComponent<Rigidbody2D>().velocity = new Vector3(-1.5f, verticalVelocity, 0);

        GameObject j = (GameObject)Instantiate(miniLaser, transform.position, transform.rotation);

        j.GetComponent<Rigidbody2D>().velocity = new Vector3(-0.5f, verticalVelocity, 0);

        GameObject k = (GameObject)Instantiate(miniLaser, transform.position, transform.rotation);

        k.GetComponent<Rigidbody2D>().velocity = new Vector3(0.5f, verticalVelocity, 0);

        GameObject l = (GameObject)Instantiate(miniLaser, transform.position, transform.rotation);

        l.GetComponent<Rigidbody2D>().velocity = new Vector3(1.5f, verticalVelocity, 0);

        GameObject m = (GameObject)Instantiate(miniLaser, transform.position, transform.rotation);

        m.GetComponent<Rigidbody2D>().velocity = new Vector3(2.5f, verticalVelocity, 0);
    }

    public void GLasers() //Laser adicionales para el GigaLaser
    {
        Instantiate(gLasers[0], transform.position, Quaternion.identity);
        Instantiate(gLasers[1], transform.position, Quaternion.identity);
        Instantiate(gLasers[2], transform.position, Quaternion.identity);
        Instantiate(gLasers[3], transform.position, Quaternion.identity);
    }

    public void Hit(Collider2D other)
    {
        Destroy(other.gameObject);
        durability--;
    }

    private IEnumerator Fight(int typeAttack) //Selector aleatorio de ataques
    {
        attacking = true;
        attackTimer = 0;
        switch (typeAttack)
        {
            case 0:
                {
                    basicAttack = true;
                    for (int i = 0; i < 2; i++)
                    {              
                        while (Vector2.Distance(transform.position, _target.transform.position) > 0.05f)
                        {
                            if(secondPhase == true)
                            {
                                if (_target.transform.position.x < 0)
                                {
                                    rb.velocity = new Vector2(-1f, 0);
                                }

                                if (_target.transform.position.x > 0)
                                {
                                    rb.velocity = new Vector2(1f, 0);
                                }
                            }
                            else
                            {
                                if (_target.transform.position.x < 0)
                                {
                                    rb.velocity = new Vector2(-0.5f, 0);
                                }

                                if (_target.transform.position.x > 0)
                                {
                                    rb.velocity = new Vector2(0.5f, 0);
                                }
                            }
                            // IMPORTANT
                            yield return null;
                        }

                        transform.position = new Vector2(_target.transform.position.x, transform.position.y);

                        yield return new WaitForSeconds(0.2f); // IMPORTANT

                        UpdateTarget();
                    }
                    basicAttack = false;
                }
                break;
            case 1:
                {
                    rb.velocity = new Vector2(0, 0);
                    if(secondPhase == true)
                    {
                        for(int i = 0; i < 16; i++)
                        {
                            Instantiate(rocket, playerPosition.transform.position + new Vector3(0, 7f, 0), Quaternion.identity);
                            yield return new WaitForSeconds(0.5f);
                        }
                    }
                    else
                    {
                        for(int i = 0; i < 8; i++)
                        {
                            Instantiate(rocket, playerPosition.transform.position + new Vector3(0, 7f, 0), Quaternion.identity);
                            yield return new WaitForSeconds(1f);
                        }
                    }
                    yield return new WaitForSeconds(3f);
                }
                break;
            case 2:
                {
                    search = true;
                    yield return new WaitForSeconds(3f);
                    search = false;
                    rb.velocity = new Vector2(0, 0);
                    yield return new WaitForSeconds(0.6f);
                    if(secondPhase == true)
                    {
                        Instantiate(gigaLaser, transform.position + new Vector3(0, -3f, 0), Quaternion.identity);
                        for(int i = 0; i < 3; i++)
                        {
                            GLasers();
                            yield return new WaitForSeconds(1f);
                        }
                    }
                    else
                    {
                        Instantiate(gigaLaser, transform.position + new Vector3(0, -3f, 0), Quaternion.identity);
                        yield return new WaitForSeconds(3f);
                    }
                }
                break;
            case 3:
                {
                    if (secondPhase == true)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            Pattern_1();
                            yield return new WaitForSeconds(0.5f);
                            Pattern_2();
                            yield return new WaitForSeconds(0.5f);
                        }

                        yield return new WaitForSeconds(0.2f);
                        verticalVelocity = -4f;

                        for (int i = 0; i < 6; i++)
                        {
                            Pattern_1();
                            yield return new WaitForSeconds(0.5f);
                            Pattern_2();
                            yield return new WaitForSeconds(0.5f);
                        }
                        verticalVelocity = -3f;
                    }
                    else
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            Pattern_1();
                            yield return new WaitForSeconds(0.5f);
                            Pattern_2();
                            yield return new WaitForSeconds(0.5f);
                        }
                    }
                }
                break;
        }
        while(transform.position.x > 0.1f || transform.position.x < -0.1f)
        {
            if (secondPhase == true)
            {
                if (_target.transform.position.x < 0)
                {
                    rb.velocity = new Vector2(-1f, 0);
                    yield return null;
                }

                if (_target.transform.position.x > 0)
                {
                    rb.velocity = new Vector2(1f, 0);
                    yield return null;
                }
            }
            else
            {
                if (transform.position.x < 0)
                {
                    rb.velocity = new Vector2(-0.5f, 0);
                    yield return null;
                }
                if (transform.position.x > 0)
                {
                    rb.velocity = new Vector2(0.5f, 0);
                    yield return null;
                }
            }
        }
        yield return new WaitForSeconds(1.5f);
        attacking = false;
        StartCoroutine("PatrolToTarget");
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
        StartCoroutine("PatrolToTarget");
    }

    private IEnumerator PatrolToTarget() //Movimiento principal del commander
    {
        // Coroutine to move the enemy
        while (Vector2.Distance(transform.position, _target.transform.position) > 0.05f)
        {
            if(paralyzed == true)
            {
                if (_target.transform.position.x < 0)
                {
                    rb.velocity = new Vector2(-0.2f, 0);
                }

                if (_target.transform.position.x > 0)
                {
                    rb.velocity = new Vector2(0.2f, 0);
                }
            }
            else
            {
                if(secondPhase == true)
                {
                    if (_target.transform.position.x < 0)
                    {
                        rb.velocity = new Vector2(-1f, 0);
                    }

                    if (_target.transform.position.x > 0)
                    {
                        rb.velocity = new Vector2(1f, 0);
                    }
                }
                else
                {
                    if (_target.transform.position.x < 0)
                    {
                        rb.velocity = new Vector2(-0.5f, 0);
                    }

                    if (_target.transform.position.x > 0)
                    {
                        rb.velocity = new Vector2(0.5f, 0);
                    }
                }
            }
            // IMPORTANT
            yield return null;
        }

        // At this point, i've reached the target, let's set our position to the target's one
        transform.position = new Vector2(_target.transform.position.x, transform.position.y);

        yield return new WaitForSeconds(0.2f); // IMPORTANT

        UpdateTarget();
        StartCoroutine("PatrolToTarget");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "L_Attack":
                {
                    Hit(other);
                }
                break;
            case "PE_Attack":
                {
                    Destroy(other.gameObject);
                }
                break;
            case "R_Attack":
                {
                    Hit(other);
                }
                break;
            case "BM_Attack": //Black Matter Missile
                {
                    Destroy(other.gameObject);
                    durability = durability - 10;
                }
                break;
            case "RS_Attack": //Reforged Stringer
                {
                    if (durability <= 2)
                    {
                        durability = 0;
                    }
                    else
                    {
                        Hit(other);
                    }
                }
                break;
            case "PL_Attack": //Positronic Laser
                {
                    if (durability <= 2)
                    {
                        Destroy(other.gameObject);
                        Instantiate(pl_Spawner, transform.position, Quaternion.identity);
                        Stadistics.commander = Stadistics.commander++;
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
                    if(paralyzed == true)
                    {
                        paralyzedTimer = 0;
                    }
                    else
                    {
                        paralyzed = true;
                    }
                }
                break;
        }
    }
}
