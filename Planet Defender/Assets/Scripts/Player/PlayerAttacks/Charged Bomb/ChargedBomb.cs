using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedBomb : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    public Color[] color = new Color[] { new Color(1f, 0.9160186f, 0f), new Color(1f, 0.5962105f, 0f), new Color(0.8773585f, 0.2062787f, 0f) };
    public GameObject[] bombFragment;
    public GameObject bfSpawner; 
    public int state = 0;
    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb = this.GetComponent<Rigidbody2D>();
        this.gameObject.GetComponent<Renderer>().material.color = color[0];
        StartCoroutine("Preparing");
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0, 0.6f);

        if (transform.position.y > screenBounds.y * 1.1f)
        {
            Destroy(this.gameObject);
        }

        switch(state)
        {
            case 1:
                {
                    this.gameObject.GetComponent<Renderer>().material.color = color[1];
                }
                break;
            case 2:
                {
                    this.gameObject.GetComponent<Renderer>().material.color = color[2];
                }
                break;
        }
    }

    public void Detonation()
    {
        switch (state)
        {
            case 0:
                {
                    Destroy(this.gameObject);
                }
                break;
            case 1:
                {
                    Instantiate(bombFragment[0], transform.position, Quaternion.identity);
                    Instantiate(bombFragment[1], transform.position, Quaternion.identity);
                    Instantiate(bombFragment[2], transform.position, Quaternion.identity);
                    Instantiate(bombFragment[3], transform.position, Quaternion.identity);
                    Instantiate(bombFragment[4], transform.position, Quaternion.identity);
                    Instantiate(bombFragment[5], transform.position, Quaternion.identity);
                    Instantiate(bombFragment[6], transform.position, Quaternion.identity);
                    Instantiate(bombFragment[7], transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                }
                break;
            case 2:
                {
                    Instantiate(bfSpawner, transform.position, Quaternion.identity);
                    Destroy(this.gameObject);
                }
                break;
        }
    }

    private IEnumerator Preparing()
    {
        for(int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.7f);
            state++;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "PE_Attack":
                {
                    Detonation();
                }
                break;
            case "Enemy":
                {
                    Destroy(this.gameObject);
                }
                break;
        }
    }
}
