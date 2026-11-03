using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliteTransporterAttacks : MonoBehaviour
{
    BossHealth bossHealth;
    EliteTransporter eliteTransporter;

    public bool start; //Variable cambiada por el levelDirector para indicar cuando un boss comienza a actuar después de que halla sido spawneado
    public bool survivalMode = false; //Variable para cambiar el comportamiento del UltraTransporter cambiado por el levelDirector

    //Limites de la pantalla
    private Vector2 screenBounds;

    private Rigidbody2D rb;

    //Variables de enemigos spawneables
    public Transform spawn;
    public GameObject[] enemies;
    //Soldier
    //Protector
    //Assaulter
    //Elite Asaulter
    //Deflector
    //Executor
    //Bulldozer
    private float spawnTimer = 0;
    private float timeForSpawn = 4.5f;
    private float spawnableEnemies = 3;
    private int bulldozerTime = 15;
    private int spawnCounter;

    //Variables de ataques
    public Transform laserSpawn_L;
    public Transform laserSpawn_R;
    public GameObject laser;
    private float attackTimer = 0;
    private float timeForAttack = 10f;
    private float attackInterval = 1f;
    private int numAttacks = 10;
    private bool attacking = false;

    //Variables de movimiento
    private GameObject _target;
    private float speed = 0.5f;

    //Varaibles cuando esta paralizado
    public float paralyzedTimer;
    public bool paralyzed;

    //Variable de fases
    public bool entrance = true;
    private float phase = 1;
    public bool changingPhase = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        bossHealth = this.GetComponent<BossHealth>();
        eliteTransporter = this.GetComponent<EliteTransporter>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if(survivalMode == true)
        {
            spawnableEnemies = 7;
        }
        StartCoroutine("Starting");
    }

    // Update is called once per frame
    void Update()
    {
        Health();

        if (start == true && changingPhase == false && entrance == false)
        {
            if(spawnTimer < timeForSpawn)
            {
                spawnTimer = spawnTimer + Time.deltaTime;
            }
            else
            {
                SpawnEnemy();
                spawnTimer = 0;
            }

            if(attackTimer < timeForAttack && attacking == false)
            {
                attackTimer = attackTimer + Time.deltaTime;
            }
            else
            {
                if(attacking == false)
                {
                    StartCoroutine("Attack_1");
                    attackTimer = 0;
                }    
            }
        }
    }

    public void Health()
    {
        if(bossHealth.health < (float)(bossHealth.maxHealth*0.75f) && phase == 1)
        {
            phase = 2;
            StartCoroutine("ChangePhase", phase);
        }

        if (bossHealth.health < (float)(bossHealth.maxHealth*0.3f) && phase == 2)
        {
            phase = 3;
            StartCoroutine("ChangePhase", phase);
        }
    }

    private void SpawnEnemy()
    {
        int x, y;
        if (survivalMode == true)
        { 
            spawnCounter++;

            if (spawnCounter >= bulldozerTime)
            {
                Instantiate(enemies[6], spawn.position, Quaternion.identity);
                spawnCounter = 0;
            }
            else
            {
                y = (int)spawnableEnemies - 1;
                x = Random.Range(0, y);
                Instantiate(enemies[x], spawn.position, Quaternion.identity);
            }
        }
        else
        {
            y = (int)spawnableEnemies;
            x = Random.Range(0, y);
            Instantiate(enemies[x], spawn.position, Quaternion.identity);
        }
    }

    private void UpdateTarget()
    {
        // If first time, create target in the left
        if (_target == null)
        {
            _target = new GameObject("Target");
            _target.transform.position = new Vector2(-3.5f, this.transform.position.y);
            return;
        }

        // If we are in the left, change target to the right
        if (_target.transform.position.x == -3.5f)
        {
            _target.transform.position = new Vector2(3.5f, this.transform.position.y);
        }

        // If we are in the right, change target to the left
        else if (_target.transform.position.x == 3.5f)
        {
            _target.transform.position = new Vector2(-3.5f, this.transform.position.y);
        }
    }

    private IEnumerator Starting()
    {
        yield return new WaitForSeconds(0.1f);
        rb.velocity = new Vector2(0, -0.25f);
        while (transform.position.y > (screenBounds.y - 1.0f))
        {
            yield return null;
        }
        rb.velocity = new Vector2(0, 0);
        while (start == false) //Se cambia la variable en LevelDirector para iniciar el comportamiento
        {
            yield return null;
        }
        UpdateTarget();
        entrance = false;
        StartCoroutine("PatrolToTarget");
    }

    private IEnumerator PatrolToTarget() //Movimiento principal del elite transporter
    {
        // Coroutine to move the enemy
        while (Vector2.Distance(transform.position, _target.transform.position) > 0.05f)
        {
            if (paralyzed == true)
            {
                if (_target.transform.position.x < 0)
                {
                    rb.velocity = new Vector2(-1f * 0.5f * speed, 0);
                }

                if (_target.transform.position.x > 0)
                {
                    rb.velocity = new Vector2(0.5f * speed, 0);
                }
            }
            else
            {
                if (_target.transform.position.x < 0)
                {
                    rb.velocity = new Vector2(-1f * speed, 0);
                }

                if (_target.transform.position.x > 0)
                {
                    rb.velocity = new Vector2(speed, 0);
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

    private IEnumerator ChangePhase(int phase)
    {
        StopCoroutine("PatrolToTarget");
        StopCoroutine("Attack_1");
        changingPhase = true;
        spawnTimer = 0;

        if(attackTimer > timeForAttack/2)
        {
            attackTimer = timeForAttack/2;
        }

        switch(phase)
        {
            case 2:
                {
                    timeForSpawn = 3.5f;
                    timeForAttack = 9f;
                    attackInterval = 0.7f;
                    numAttacks = 15;
                    speed = 0.75f;
                    if (survivalMode == false)
                    {
                        spawnableEnemies = 4;
                    }
                    else
                    {
                        bulldozerTime = 25;
                    }
                }
                break;
            case 3:
                {
                    timeForSpawn = 2.5f;
                    timeForAttack = 7.5f;
                    attackInterval = 0.5f;
                    numAttacks = 20;
                    speed = 1f;
                    if (survivalMode == false)
                    {
                        spawnableEnemies = 2;
                    }
                    else
                    {
                        bulldozerTime = 35;
                    }
                }
                break;
            default:
                {
                    Debug.Log("Al paso que no deberia pasar :(");
                }
                break;
        }

        rb.velocity = new Vector2(0, -0.5f);
        if(survivalMode == false)
        {
            while (transform.position.y > (screenBounds.y - (1f + (0.75f * phase))))
            {
                yield return null;
            }
            UpdateTarget();
        }
        changingPhase = false;
        StartCoroutine("PatrolToTarget");
        StopCoroutine("Attack_1");
        yield return null;
    }

    private IEnumerator Attack_1()
    {
        attacking = true;
        int i = 0;
        while(i < numAttacks)
        {
            Instantiate(laser, laserSpawn_L.position, Quaternion.identity);
            Instantiate(laser, laserSpawn_R.position, Quaternion.identity);
            yield return new WaitForSeconds(attackInterval);
            i++;
        }  
        attacking = false;
        yield return null;
    }
}
