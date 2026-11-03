using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositronicLaser_HL : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb.velocity = new Vector2(-3.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > screenBounds.x * 1.1f)
        {
            Destroy(this.gameObject);
        }
    }
}
