using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScrapT4 : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0.5f, 0);
        StartCoroutine("D");
    }

    private IEnumerator D()
    {
        yield return new WaitForSeconds(4);
        Destroy(this.gameObject);
    }
}
