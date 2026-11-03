using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDecoration : MonoBehaviour
{
    public GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        /*
        GameObject a = (GameObject)Instantiate(enemies[0], transform.position + new Vector3(0, 4f, 0), Quaternion.identity);
        a.GetComponent<Soldier>().decoration = true;
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
