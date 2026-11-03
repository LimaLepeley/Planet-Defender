using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    AudioSource audioSource;

    //Si se agregan más menus de super se tiene que agregar aqui

    public GameObject superMenu_MainMenu;
    public GameObject superMenu_LevelMenu;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (superMenu_MainMenu.activeSelf == true || superMenu_LevelMenu.activeSelf == true)
        {
            audioSource.pitch = 0.95f;
        }
        else
        {
            audioSource.pitch = 1;
        }

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
