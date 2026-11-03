using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalSupportSpawn_1 : MonoBehaviour
{
    public GameObject[] enemies;
    public int type;
    public int direction; //0 = Izquierda, 1 = Derecha
    public float range;
    private bool run;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy_R()
    {
        GameObject a = (GameObject)Instantiate(enemies[type], transform.position + new Vector3((1 + range), 0, 0), Quaternion.identity);
        a.SendMessage("Support");
    }

    public void SpawnEnemy_L()
    {
        GameObject a = (GameObject)Instantiate(enemies[type], transform.position + new Vector3(-(1 + range), 0, 0), Quaternion.identity);
        a.SendMessage("Support");
    }

    private IEnumerator Spawn()
    {
        while(run == false)
        {
            switch(direction)
            {
                case 0:
                    {
                        SpawnEnemy_L();
                        yield return new WaitForSeconds(3f);
                        SpawnEnemy_R();
                        yield return new WaitForSeconds(3f);
                    }
                    break;
                case 1:
                    {
                        SpawnEnemy_R();
                        yield return new WaitForSeconds(3f);
                        SpawnEnemy_L();
                        yield return new WaitForSeconds(3f);
                    }
                    break;
            }
        }
    }
}
