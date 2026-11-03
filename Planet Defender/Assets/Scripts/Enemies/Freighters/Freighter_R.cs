using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freighter_R : MonoBehaviour
{
    public AudioClip hit;
    AudioSource audioSource;

    public Sprite[] spriteArray;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public GameObject[] powerUp;
    public GameObject explosion;
    public GameObject pl_Spawner;
    private Vector2 screenBounds;
    public int type;
    private int durability = 4;
    private float s;
    public float speed;
    private bool empty;
    private bool paralyzed;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        durability = 4;
        Aspect();
    }

    // Update is called once per frame
    void Update()
    {
        if (durability <= 0 && empty == false)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            PowerInst();
            empty = true;
            Destroy(this.gameObject);
        }
        else
        {
            if (durability <= 0)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Stadistics.freighter = Stadistics.freighter++;
                Destroy(this.gameObject);
            }
        }

        if (transform.position.x > screenBounds.x * 1.5f)
        {
            Destroy(this.gameObject);
        }

        if (empty == true)
        {
            spriteRenderer.sprite = spriteArray[0];
        }
    }

    public void Aspect()
    {
        switch (type)
        {
            case 1: //Refrigeration
                {
                    spriteRenderer.sprite = spriteArray[1];
                }
                break;
            case 2: //Overcharge
                {
                    spriteRenderer.sprite = spriteArray[2];
                }
                break;
            case 3: //Shield
                {
                    spriteRenderer.sprite = spriteArray[3];
                }
                break;
            case 4: //Reinforcements
                {
                    spriteRenderer.sprite = spriteArray[4];
                }
                break;
            case 5: //Super
                {
                    spriteRenderer.sprite = spriteArray[5];
                }
                break;
        }
    }

    public void Hit(Collider2D other)
    {
        audioSource.PlayOneShot(hit, 0.4f);
        Destroy(other.gameObject);
        durability = durability - 1;
    }

    public void PowerInst()
    {
        switch (type)
        {
            case 1: //Refrigeration
                {
                    Instantiate(powerUp[0], transform.position, Quaternion.identity);
                }
                break;
            case 2: //Overcharge
                {
                    Instantiate(powerUp[1], transform.position, Quaternion.identity);
                }
                break;
            case 3: //Shield
                {
                    Instantiate(powerUp[2], transform.position, Quaternion.identity);
                }
                break;
            case 4: //Reinforcements
                {
                    Instantiate(powerUp[3], transform.position, Quaternion.identity);
                }
                break;
            case 5: //Super
                {
                    Instantiate(powerUp[4], transform.position, Quaternion.identity);
                }
                break;
        }
    }

    private IEnumerator Paralyzed()
    {
        if (empty == false)
        {
            PowerInst();
            empty = true;
            paralyzed = true;
            rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(3f);
            paralyzed = false;
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            paralyzed = true;
            rb.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(3f);
            paralyzed = false;
            rb.velocity = new Vector2(speed, 0);
        }

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
                    if (empty == false)
                    {
                        Destroy(other.gameObject);
                        PowerInst();
                        empty = true;
                    }
                    else
                    {
                        Destroy(other.gameObject);
                    }
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
                        Stadistics.freighter = Stadistics.freighter++;
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
