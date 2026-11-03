using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    private Color color;

    public Sprite[] sprites;

    private SpriteRenderer spriteRenderer;

    private float time;
    private int rotation;
    private int type;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        type = Random.Range(0, 6);
        spriteRenderer.sprite = sprites[type];
        time = Random.Range(1, 4) * 0.1f;
        rotation = Random.Range(-1, 2) * Random.Range(1, 2) * 10;
        if(rotation == 0)
        {
            rotation = 10;
        }
        StartCoroutine("Rotation");
        StartCoroutine("OpacityChange");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OpacityChange()
    {
        for(int i = 0; i < 10; i++)
        {
            color = new Color(1, 1, 1, (1 - (i * 0.1f)));
            gameObject.GetComponent<Renderer>().material.color = color;
            yield return new WaitForSeconds(time);
        }
        Destroy(this.gameObject);
    }

    private IEnumerator Rotation()
    {
        while(this.gameObject.activeSelf == true)
        {
            transform.Rotate(0, 0, rotation);
            yield return null;
        }
        
    }
}
