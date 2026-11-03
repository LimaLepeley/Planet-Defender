using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigaLaser : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Laser");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Laser()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
