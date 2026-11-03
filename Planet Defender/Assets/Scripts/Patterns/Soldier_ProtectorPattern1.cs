using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier_ProtectorPattern1 : MonoBehaviour
{
    public GameObject[] enemies;  //Line Pattern
    public int numSoldiers = 3;
    public int numProtectors = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Pattern");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Pattern()
    {
        for(int i=0; i<numProtectors; i++)
        {
            Instantiate(enemies[1], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        for(int i=0; i<numSoldiers; i++)
        {
            Instantiate(enemies[0], transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }

        Destroy(this.gameObject);
    }
}
