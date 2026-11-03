using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtonShield : MonoBehaviour
{
    public Color[] color = new Color[] { new Color(0f, 0.4913632f, 0.5660378f), new Color(0f, 0.6797041f, 0.7830189f), new Color(0f, 0.8650002f, 1f)};
    public GameObject protonShieldGenerator;
    private int durability = 5;
    private int damage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        durability = Mathf.Clamp(durability, 0, 15);

        if (durability <= 0)
        {
            protonShieldGenerator.SendMessage("Deactivate");
        }

        Animation();
    }

    public void Animation()
    {
        if (durability <= 5)
        {
            this.gameObject.GetComponent<Renderer>().material.color = color[0];
            this.transform.localScale = new Vector3(6f, 0.5f, 0);
        }
        if (durability > 5 && durability <= 10)
        {
            this.gameObject.GetComponent<Renderer>().material.color = color[1];
            this.transform.localScale = new Vector3(10f, 0.5f, 0);
        }
        if (durability > 10)
        {
            this.gameObject.GetComponent<Renderer>().material.color = color[2];
            this.transform.localScale = new Vector3(13f, 0.5f, 0);
        }
    }

    public void Upgrade()
    {
        durability = durability + 5;
    }

    public void BigEnemyCollision(int damage)
    {
        durability = durability - damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch(other.gameObject.tag)
        {
            case "Enemy":
                {
                    if(other.gameObject.name == "Bulldozer(Clone)" || other.gameObject.name == "Bulldozer" || other.gameObject.name == "Kinnector" || other.gameObject.name == "Kinnector(Clone)")
                    {
                        damage = other.gameObject.GetComponent<Bulldozer>().durability;
                        other.SendMessage("ShieldCollision", durability);
                        BigEnemyCollision(damage);
                    }
                    else
                    {
                        durability--;
                    }                   
                }
                break;
        }
    }

}
