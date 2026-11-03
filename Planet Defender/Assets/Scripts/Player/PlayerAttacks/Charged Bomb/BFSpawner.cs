using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFSpawner : MonoBehaviour
{
    public GameObject[] bombFragment;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Explosion");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Explosion()
    {
        for(int i = 0; i < 5; i++)
        {
            Instantiate(bombFragment[0], transform.position, Quaternion.identity);
            Instantiate(bombFragment[1], transform.position, Quaternion.identity);
            Instantiate(bombFragment[2], transform.position, Quaternion.identity);
            Instantiate(bombFragment[3], transform.position, Quaternion.identity);
            Instantiate(bombFragment[4], transform.position, Quaternion.identity);
            Instantiate(bombFragment[5], transform.position, Quaternion.identity);
            Instantiate(bombFragment[6], transform.position, Quaternion.identity);
            Instantiate(bombFragment[7], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
        Destroy(this.gameObject);
    }
}
