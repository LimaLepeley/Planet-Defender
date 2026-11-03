using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDirector : MonoBehaviour
{
    //Variable para guardar una referencia al componente AudioSource
    private AudioSource audioSource;

    //Variable para guardar una referencia al componente AudioDistorsionFilter
    private AudioDistortionFilter distortionFilter;

    //Variable que guarda una referencia al menu de pausa
    private Transform[] pauseMenuCanvasChildren;
    private GameObject pauseMenu;

    //Variable para saber si hay una cinematica en curso
    //* Cambiada desde el director de cada nivel
    private bool cinematic;

    //Variables de las canciones y efectos usados
    public AudioClip levelMusic;
    public AudioClip pauseMenuMusic;

    // Start is called before the first frame update
    void Start()
    {
        //Se coloca la referencia del AudioSource y el AudioDistorsionFilter
        audioSource = GetComponent<AudioSource>();
        distortionFilter = GetComponent<AudioDistortionFilter>();

        //Se obtiene la referencia al menu de pausa
        //* Primero se halla al canvas
        GameObject pauseCanvas = GameObject.Find("Canvas(Pause Menu)");

        //Comprobación de que el canvas existe
        if(pauseCanvas != null)
        {
            Transform pauseMenuTransform = pauseCanvas.transform.Find("Pause Menu");

            if (pauseMenuTransform != null)  // Verificamos que se encontró el menú de pausa
            {
                pauseMenu = pauseMenuTransform.gameObject;
            }
            else
            {
                Debug.LogWarning("No se encontró el objeto 'Pause Menu' dentro del Canvas.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró el Canvas llamado 'Canvas(Pause Menu)'.");
        }

        // Si el AudioSource existe y hay una canción asignada, la reproducimos
        if (audioSource != null && levelMusic != null)
        {
            audioSource.clip = levelMusic;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No se pudo reproducir la música. Verifica que el AudioSource y el AudioClip están asignados.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(pauseMenu)
        {
            //La musica cambia cuando se activa el menu de pausa
            //* A la musica del menu se le aplica distorsion y se le baja el sonido
            if (pauseMenu.activeSelf)
            {
                audioSource.clip = pauseMenuMusic;
                distortionFilter.distortionLevel = 0.6f;
                audioSource.volume = 0.05f;
            }
            else
            {
                //Si se despausa vuelve a la musica normal
                //Solo se cumple si no se esta en una cinemtica actualmente
                if(!cinematic)
                {
                    audioSource.clip = levelMusic;
                    distortionFilter.distortionLevel = 0f;
                    audioSource.volume = 0.2f;
                }
            }
        }
        else
        {
            Debug.LogWarning("No se tiene referencia al menu de pausa");
        }

        //Si el AudioSource existe y no se esta tocando la musica se empieza a tocar
        if(audioSource)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogWarning("La referencia al AudioSource se encuentra vacia");
        }
    }

    //Getters y Setters
    public bool isCinematic() {
		return this.cinematic;
	}

	public void setCinematic(bool cinematic) {
		this.cinematic = cinematic;
	}
}
