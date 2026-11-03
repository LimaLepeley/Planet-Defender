using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacksOutOfBounds : MonoBehaviour
{
    //Variable donde se asignaran los limites de la camara
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        //Se asignan los limites de la camara
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        //Si el objeto sale por arriba de la pantalla es destruido
        if (transform.position.y > screenBounds.y * 1.1f)
        {
            Destroy(this.gameObject);
        }
    }
}
