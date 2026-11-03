using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonicPulse : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Pulse");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator Pulse()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
}
