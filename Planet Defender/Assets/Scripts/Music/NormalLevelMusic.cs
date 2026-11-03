using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Esto hay que llevarlo a scraped
public class NormalLevelMusic : MonoBehaviour
{
    //Variable para guardar una referencia al AudioSource
    private AudioSource audioSource;

    //Variable para guardar un referencia al menu de pausa
    public GameObject pauseMenu;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //La musica es más baja cuando se esta en el menu de pausa
        if (pauseMenu.activeSelf == true)
        {
            audioSource.volume = 0.15f;
        }
        else
        {
            audioSource.volume = 0.6f;
        }

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
