using UnityEngine;

public class SoldierAnimation : MonoBehaviour
{
    //Referencia del componente SpriteRenderer
    private SpriteRenderer spriteRenderer;

    //Referencia al script SoldierCollision
    private SoldierCollision soldierCollision;

    //Variable para guardar el prefab de las particulas del motor
    public GameObject particle;

    //Variable para guardar una referencia a las particulas del motor
    public GameObject[] motor_Particles;

    //Variable para guardar el lugar raiz de las particulas del motor
    public Transform[] particleSpawns;

    //Variable para guardar sus sprites
    //* 0 es para el sprite normal y el 1 es para el sprite desactivado
    public Sprite[] sprites;

    //Variable que guarda el prefab de la explosión
    public GameObject explosion;

    void Start()
    {
        //Se asigna la referencia del componente SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Se asigna la referencia del script SoldierCollision
        soldierCollision = GetComponent<SoldierCollision>();

        //Se instancian las particulas de los motores en el lugar correspondido
        motor_Particles[0] = (GameObject)Instantiate(particle, particleSpawns[0].position, new Quaternion(-1, 0, 0, 1), this.transform);
        motor_Particles[1] = (GameObject)Instantiate(particle, particleSpawns[1].position, new Quaternion(-1, 0, 0, 1), this.transform);
    }

    void Update()
    {
        //Se verifica que las referencias esten asignadas
        if(spriteRenderer && soldierCollision)
        {
            //Si el objeto esta desacelerado se cambia de sprite
            if(soldierCollision.getDecelerated())
            {
                spriteRenderer.sprite = sprites[1];
            }
            else
            {
                spriteRenderer.sprite = sprites[0];
            }
        }
        else
        {
            Debug.LogError("El componente SpriteRenderer o el script SoldierCollision estan asignados");
        }
    }

    //Función que se llama cuando el objeto es destruido
    public void Destroyed()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}