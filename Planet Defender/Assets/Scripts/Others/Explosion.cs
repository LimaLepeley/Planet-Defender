using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Animation");
    }

    private IEnumerator Animation()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
