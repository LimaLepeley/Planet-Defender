using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveEscort_R : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    public GameObject shield;
    public GameObject[] pLasers;
    public GameObject explosion;
    private GameObject target;
    private GameObject Escort_R;
    private float shieldTimer = 0;
    private bool disable;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        DefineTarget();
        StartCoroutine("Entrance");
    }

    // Update is called once per frame
    void Update()
    {
        if (shield.activeSelf == false)
        {
            disable = true;
        }

        if (disable == true)
        {
            shieldTimer = shieldTimer + Time.deltaTime;
        }

        if (shieldTimer >= 5f)
        {
            shield.SetActive(true);
            disable = false;
            shieldTimer = 0;
        }
    }

    public void Disable()
    {
        shield.SetActive(false);
    }

    public void DefineTarget()
    {
        Escort_R = GameObject.FindWithTag("Escort_R");

        if (Escort_R == null)
        {
            if (target == null)
            {
                target = new GameObject("Target");
                target.transform.position = new Vector2(3.5f, 0.5f);
                return;
            }
        }
        else
        {
            if (Escort_R.transform.position.x == 3.5f)
            {
                if (target == null)
                {
                    target = new GameObject("Target");
                    target.transform.position = new Vector2(2.5f, 0.5f);
                    return;
                }
            }
            else
            {
                if (target == null)
                {
                    target = new GameObject("Target");
                    target.transform.position = new Vector2(3.5f, 0.5f);
                    return;
                }
            }

        }
    }

    public void OneShotAttack(Collider2D other)
    {
        Destroy(other.gameObject);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Stadistics.escort = Stadistics.escort++;
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

    private IEnumerator Entrance()
    {
        while (Vector2.Distance(transform.position, target.transform.position) > 0.05f)
        {
            Vector2 direction = target.transform.position - transform.position;
            float xDirection = direction.x;

            transform.Translate(direction.normalized * 1f * Time.deltaTime);

            yield return null;
        }

        transform.position = new Vector2(target.transform.position.x, target.transform.position.y);

        yield return new WaitForSeconds(0.2f);

        shield.SetActive(true);
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
                    Stadistics.escort = Stadistics.escort++;
                    Destroy(this.gameObject);
                }
                break;
            case "PL_Attack": //Positronic Laser
                {
                    Destroy(other.gameObject);
                    Instantiate(explosion, transform.position, Quaternion.identity);
                    Stadistics.escort = Stadistics.escort++;
                    PositronicLasers();
                    Destroy(this.gameObject);
                }
                break;
            case "TLM_Attack": //Charged Bomb
                {
                    OneShotAttack(other);
                }
                break;
            case "IP_Attack": //Ion Pulse
                {
                    Disable();
                }
                break;
        }
    }
}
