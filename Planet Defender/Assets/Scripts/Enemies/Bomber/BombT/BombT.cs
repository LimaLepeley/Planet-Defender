using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombT : MonoBehaviour
{
    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;
    public Transform spawn4;
    public GameObject bombScrap1;
    public GameObject bombScrap2;
    public GameObject bombScrap3;
    public GameObject bombScrap4;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Bomb");
    }

    private IEnumerator Bomb()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Instantiate(bombScrap1, spawn1.transform.position, Quaternion.identity);
        bombScrap1.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, 0);
        Instantiate(bombScrap2, spawn2.transform.position, Quaternion.identity);
        bombScrap2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0.5f);
        Instantiate(bombScrap3, spawn3.transform.position, Quaternion.identity);
        bombScrap3.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -0.5f);
        Instantiate(bombScrap4, spawn4.transform.position, Quaternion.identity);
        bombScrap4.GetComponent<Rigidbody2D>().velocity = new Vector2(0.5f, 0);
        Destroy(this.gameObject);
    }
}
