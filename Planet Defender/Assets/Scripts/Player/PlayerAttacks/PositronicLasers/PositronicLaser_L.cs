using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositronicLaser_L : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        transform.Rotate(new Vector3(0, 0, 45f));
        rb.velocity = new Vector2(-2.52f, 2.52f);
    }

    // Update is called once per frame
    void Update()
    {        
        if (transform.position.y > screenBounds.y * 1.1f)
        {
            Destroy(this.gameObject);
        }
    }
}
