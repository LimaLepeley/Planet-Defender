using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMovement : MonoBehaviour
{
    //Referencia al script SoldierCollision
    private SoldierCollision soldierCollision;

    //Variable para guardar la referencia del componente RigidBody2D
    private Rigidbody2D rb;

    //Variable que define la velocidad del enemigo
    private float speed = -1f;
    
    void Start()
    {
        //Se asigna la referencia al componente Rigidbody2D y la velocidad
        rb = GetComponent<Rigidbody2D>();

        //Se asigna la referencia
        soldierCollision = GetComponent<SoldierCollision>();

        if(rb) //Se verifica que la referencia este asignada
        {
            rb.velocity = new Vector2(0, speed);
        }
        else
        {
            Debug.LogWarning("El componente Rigidbody no se asigno correctamente");
        }
    }

    void Update()
    {   
        //Se verifica que la referencia no este vacia
        if(soldierCollision)
        {
            //Si la variable decelertated es true el objeto se mueve más despacio
            if(soldierCollision.getDecelerated())
            {
                rb.velocity = new Vector2(0, speed / 2f);
            }
            else
            {
                rb.velocity = new Vector2(0, speed);
            }
        }
    }
}