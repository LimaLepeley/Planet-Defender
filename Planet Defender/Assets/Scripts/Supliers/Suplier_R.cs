using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suplier_R : MonoBehaviour
{
    private GameObject playerPosition;

    public Sprite[] spriteArray;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public GameObject[] powerUp;
    private Vector2 screenBounds;
    public int type;
    private bool empty;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.FindWithTag("PlayerPosition");
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb.velocity = new Vector3(3f, 0, 0);
        Aspect();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(transform.position.x - playerPosition.transform.position.x) <= 0.1f && empty == false)
        {
            PowerInst();
            empty = true;
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
}
