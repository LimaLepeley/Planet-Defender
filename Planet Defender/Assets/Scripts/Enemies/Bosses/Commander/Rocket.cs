using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    public Sprite markSprite;
    private Color color = new Color(1, 0.8913302f, 0);
    private SpriteRenderer markSpriteR;
    private SpriteRenderer spriteR;
    private GameObject player;
    private GameObject target;
    private GameObject mark;
    public GameObject explosion;
    public bool prosecute;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        spriteR.enabled = false;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        player = GameObject.FindWithTag("PlayerPosition");
        CreateTarget();
        Mark();
        StartCoroutine("Prosecute");
    }

    // Update is called once per frame
    void Update()
    {
        if(prosecute == true)
        {
            if (transform.position.y < screenBounds.y * -1.1f)
            {
                Destroy(mark);
                Destroy(target);
                Destroy(this.gameObject);
            }

            rb.velocity = new Vector2(0, -10f);
        }
    }

    public void CreateTarget()
    {
        target = new GameObject("Target");
        target.transform.position = player.transform.position;
    }

    public void Mark()
    {
        mark = new GameObject("Mark", typeof(SpriteRenderer));
        markSpriteR = mark.GetComponent<SpriteRenderer>();
        markSpriteR.sprite = markSprite;
        mark.gameObject.GetComponent<Renderer>().material.color = color;
        mark.transform.position = target.transform.position;
    }

    private IEnumerator Prosecute()
    {
        yield return new WaitForSeconds(1.5f);
        prosecute = true;
        spriteR.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(mark);
            Destroy(target);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
