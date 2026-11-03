using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueStar : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    public Color[] colors = new Color[] { new Color(1f, 1f, 1f), new Color(0.2146671f, 0.3141136f, 0.6792453f), new Color(0.4899995f, 0.2236116f, 0.6320754f) };

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb.velocity = new Vector2(0, -1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //Limite donde el objeto se destruye
        if (transform.position.y < (screenBounds.y * -1f) - 10f)
        {
            transform.Translate(0, 16f, 0, Camera.main.transform);
        }
    }
}
