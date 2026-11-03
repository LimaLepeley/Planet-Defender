using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //Variable que guarda la referencia al animator
    private Animator animator;

    //Variable que guarda el prefab de la explosion que se reproduce cuando muere el jugador
    public GameObject explosion;

    //Variable que indica al objeto que hay una cinematica y por lo tanto debe impedirse el movimiento
    private bool cinematic;

    void Start()
    {
        //Se asigna el componente Animator a la variable
        animator = GetComponent<Animator>();
    }

    //Corutina que se lleva a cabo para hacer la animación de disparo principal
    public IEnumerator Laser()
    {
        animator.SetBool("Red", true);
        yield return new WaitForSeconds(0.15f);
        animator.SetBool("Red", false);
    }

    //Corutina que se lleva a cabo para hacer la animación de disparo de energia
    public IEnumerator EnergyLaser()
    {
        animator.SetBool("Blue", true);
        yield return new WaitForSeconds(0.15f);
        animator.SetBool("Blue", false);
    }

    //Corutina que se lleva a cabo para hacer la animación de llamado a los refuerzos
    public IEnumerator Reinforcements()
    {
        animator.SetBool("Yellow", true);
        yield return new WaitForSeconds(0.15f);
        animator.SetBool("Yellow", false);
    }

    //Corutina que se lleva a cabo para hacer la animación de disparo de la aguja reforzada
    public IEnumerator OverchargedStinger()
    {
        animator.SetBool("Orange", true);
        yield return new WaitForSeconds(0.15f);
        animator.SetBool("Orange", false);
    }

    //Corutina que se lleva a cabo para hacer la animación de disparo del projectil de Megatones
    public IEnumerator MegatonProjectile()
    {
        animator.SetBool("Purple", true);
        yield return new WaitForSeconds(0.15f);
        animator.SetBool("Purple", false);
    }

    //Corutina que se lleva a cabo para hacer la animación de disparo del misil dividido
    public IEnumerator SplitMissile()
    {
        animator.SetBool("Green", true);
        yield return new WaitForSeconds(0.15f);
        animator.SetBool("Green", false);
    }

    //Función el cual llama a su propio animador y lo cambia con los parametros dados
    public void UseOwnAnimator(string name, bool state)
    {
        animator.SetBool(name, state);
    }

    //Función que se llama cuando se quiere que se instancie la explosion al jugador ser destruido
    //* Usado por: PlayerCollision
    public void Destruction()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
    }

    //Setter de la variable cinematic
    //* Usado por: GameDirector
    public void setCinematic(bool isCinematic)
    {
        this.cinematic = isCinematic;
    }

    //Getter de la variable cinematic
    public bool getCinematic()
    {
        return cinematic;
    }
}