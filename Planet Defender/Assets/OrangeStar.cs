using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeStar : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    public Color[] colors = new Color[] { new Color(1f, 1f, 1f, 1f), new Color(1f, 1f, 1f, 0.8f), new Color(1f, 1f, 1f, 0.6f), new Color(1f, 1f, 1f, 0.4f)};

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        rb.velocity = new Vector2(0, -0.5f);
        StartCoroutine("Animation");
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

    private IEnumerator Animation()
    {
        GetComponent<Renderer>().material.color = colors[0];
        yield return new WaitForSeconds(0.5f);
        GetComponent<Renderer>().material.color = colors[1];
        yield return new WaitForSeconds(0.5f);
        GetComponent<Renderer>().material.color = colors[2];
        yield return new WaitForSeconds(0.5f);
        GetComponent<Renderer>().material.color = colors[3];
        yield return new WaitForSeconds(0.5f);
        GetComponent<Renderer>().material.color = colors[2];
        yield return new WaitForSeconds(0.5f);
        GetComponent<Renderer>().material.color = colors[1];
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("Animation");
    }

}
