using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


//* La variable cinematica se establece aca
public class PlayerMovement : MonoBehaviour
{
    //Variable que guarda al Rigidbody del objeto
    private Rigidbody2D rb;

    //Variable que guarda la referencia al script PlayerAnimation
    private PlayerAnimation playerAnimation;

    //Variable que guarda la referencia al script PlayerEnergy
    private PlayerEnergy playerEnergy;

    //Variable para guardar el prefab de FollowPlayer
    public GameObject followPlayer;

    //Velocidades del jugador
    private float speed = 3f;
    private float boostedSpeed = 6f;

    void Start()
    {
        //Se establece la referencia al script PlayerAnimation
        playerAnimation = GetComponent<PlayerAnimation>();

        //Se establece la referencia al script PlayerEnergy
        playerEnergy = GetComponent<PlayerEnergy>();

        //Se instancia al objeto que sigue al jugador
        Instantiate(followPlayer, transform.position + new Vector3(0, -3f, 0), Quaternion.identity);
    }

    void Update()
    {
        //RigidBody2D
        rb = this.GetComponent<Rigidbody2D>();

        Movement();

        //Mientras haya cinematica el jugador no se puede mover
        if (GetComponent<PlayerAnimation>().getCinematic())
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
    
    //Función de moviento principal que se utiliza en cada frame
    //* Se llama al script PlayerEnergy para utilizar la función Dash(), el setter de usingEnergyMovement y energy
    //* Se llama al script PlayerAnimation para utilizar el getter de cinematic
    private void Movement()
    {
        //La veclocidad de movimiento cambia si el jugador presiona la tecla (Space), esta acción consumira energia
        //El jugador solo se puede mover hacia la izquierda o hacia la derecha
        if(Input.GetKey(KeyCode.Space) && playerEnergy.getEnergy() > 0 && !playerAnimation.getCinematic())
        {
            if(Input.GetKey("left"))
            {
                rb.velocity = new Vector2(boostedSpeed * -1f, 0);
                //Se llama a la función Dash en PlayerEnergy para realizar todos los cambios por el consumo de energia
                playerEnergy.Dash();
                //Se cambia la animación cada vez que se usa energia para moverse
                playerAnimation.UseOwnAnimator("Celeste", true);
            }
            else if(Input.GetKey("right"))
            {
                rb.velocity = new Vector2(boostedSpeed, 0);
                //Se llama a la función Dash en PlayerEnergy para realizar todos los cambios por el consumo de energia
                playerEnergy.Dash();
                //Se cambia la animación cada vez que se usa energia para moverse
                playerAnimation.UseOwnAnimator("Celeste", true);
            }
            else 
            {
                //El jugador deja de moverse si ninguna de las teclas se presionan
                rb.velocity = Vector2.zero;
                playerAnimation.UseOwnAnimator("Celeste", false);
                playerEnergy.setUsingEnergyMovement(false);
            }
        }
        else //Movimiento normal sin uso de energia
        {
            playerAnimation.UseOwnAnimator("Celeste", false);
            playerEnergy.setUsingEnergyMovement(false);

            if(!playerAnimation.getCinematic())
            {
                if(Input.GetKey("left"))
                {
                    rb.velocity = new Vector2(speed * -1f, 0);
                }
                else if(Input.GetKey("right"))
                {
                    rb.velocity = new Vector2(speed, 0);
                }
                else
                {
                    rb.velocity = Vector2.zero;
                }
            }
        }
    }
}