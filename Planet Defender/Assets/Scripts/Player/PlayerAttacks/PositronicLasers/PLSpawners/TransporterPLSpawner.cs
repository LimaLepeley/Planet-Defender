using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterPLSpawner : MonoBehaviour
{
    public GameObject[] pLasers;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LaserStorm");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PositronicLasers()
    {
        Instantiate(pLasers[0], transform.position, Quaternion.identity);
        Instantiate(pLasers[1], transform.position, Quaternion.identity);
        Instantiate(pLasers[2], transform.position, Quaternion.identity);
        Instantiate(pLasers[3], transform.position, Quaternion.identity);
        Instantiate(pLasers[4], transform.position, Quaternion.identity);
    }

    private IEnumerator LaserStorm()
    {
        int i = 0;

        while (i < 3)
        {
            PositronicLasers();
            yield return new WaitForSeconds(0.2f);
            i++;
        }
        Destroy(this.gameObject);
    }
}
