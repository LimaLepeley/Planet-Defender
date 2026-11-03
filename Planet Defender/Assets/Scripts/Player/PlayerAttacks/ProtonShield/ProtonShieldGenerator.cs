using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtonShieldGenerator : MonoBehaviour
{
    private Rigidbody2D rb;
    public Color[] color = new Color[] { new Color(1f, 1f, 1f), new Color(0.240566f, 0.4845211f, 1f) };
    public GameObject protonShield;
    public bool ready;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine("Deploy");
        this.gameObject.GetComponent<Renderer>().material.color = color[0];
        rb.velocity = new Vector3(0, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (ready == true)
        {
            this.gameObject.GetComponent<Renderer>().material.color = color[1];
            rb.velocity = new Vector3(0, 0);
        }
    }

    public void Deactivate()
    {
        protonShield.SetActive(false);
    }

    private IEnumerator Deploy()
    {
        yield return new WaitForSeconds(1.5f);
        ready = true;
        protonShield.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Enemy":
                {
                    Destroy(this.gameObject);
                }
                break;
            case "PS_Attack":
                {
                    if(other.gameObject.name == "ProtonShieldGenerator(Clone)")
                    {  
                        if(ready == true)
                        {
                            if(other.gameObject.GetComponent<ProtonShieldGenerator>().ready == false)
                            {
                                protonShield.SetActive(true);
                                protonShield.SendMessage("Upgrade");
                                Destroy(other.gameObject);
                            }
                        }
                        else
                        {
                            if (other.gameObject.GetComponent<ProtonShieldGenerator>().ready == true)
                            {
                                Destroy(this.gameObject);
                            }
                        }
                    }
                }
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "PS_Attack" && other.gameObject.name == "ProtonShieldGenerator(Clone)")
        {
            if (other.gameObject.name == "ProtonShieldGenerator(Clone)")
            {
                if (ready == true)
                {
                    if (other.gameObject.GetComponent<ProtonShieldGenerator>().ready == false)
                    {
                        protonShield.SetActive(true);
                        protonShield.SendMessage("Upgrade");
                        Destroy(other.gameObject);
                    }
                }
                else
                {
                    if (other.gameObject.GetComponent<ProtonShieldGenerator>().ready == true)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
