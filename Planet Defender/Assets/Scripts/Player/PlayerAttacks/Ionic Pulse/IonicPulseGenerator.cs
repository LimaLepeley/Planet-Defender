using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonicPulseGenerator : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    public GameObject ionicPulse;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, 2.5f);

        if (transform.position.y > screenBounds.y * 1.1f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Instantiate(ionicPulse, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
