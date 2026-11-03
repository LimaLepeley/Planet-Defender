using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    //Variable para guardar una referencia hacia el componente RigidBody
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //Se asigna el componente a la referencia
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, 3.5f);
    }
}
