using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusicDirector : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioDistortionFilter distortionFilter;

    public GameObject pauseMenu;

    //Variables de las canciones y efectos usados
    //mainTheme; 0
    //entrance; 1
    //phase_1; 2
    //phase_2; 3
    //phase_3; 4
    //interlude; 5
    //Succes; 6
    //pauseMenu; 7
    public AudioClip[] music;
    private int auxiliarMusic = 0;

    //Variables para coordinar la musica
    public bool start = false;
    private float newPitch = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        distortionFilter = GetComponent<AudioDistortionFilter>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu.activeSelf == true)
        {
            audioSource.clip = music[7];
            distortionFilter.distortionLevel = 0.6f;
            audioSource.volume = 0.05f;
        }
        else
        {
            if(start != true)
            {
                audioSource.clip = music[auxiliarMusic];
                distortionFilter.distortionLevel = 0f;
            }
            audioSource.volume = 0.2f;
        }

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public IEnumerator ChangePhase(int phase)
    {
        switch(phase)
        {
            case 0:
                {
                    audioSource.Stop();
                    audioSource.PlayOneShot(music[1], 0.4f);
                    while(start == true)
                    {
                        yield return null;
                    }
                    audioSource.Stop();
                    audioSource.clip = music[2];
                    audioSource.loop = true;
                    audioSource.Play();
                    auxiliarMusic = 2;
                }
                break;
            case 1:
                {
                    audioSource.Stop();
                    audioSource.clip = music[5];
                    audioSource.volume = 0.45f;
                    audioSource.pitch = newPitch;
                    audioSource.loop = false;
                    audioSource.Play();
                    while(start == true)
                    {
                        yield return null;
                    }
                    audioSource.Stop();
                    audioSource.clip = music[3];
                    audioSource.volume = 0.6f;
                    audioSource.pitch = 1f;
                    audioSource.loop = true;
                    audioSource.Play();
                    auxiliarMusic = 3;
                }
                break;
            case 2:
                {
                    audioSource.Stop();
                    audioSource.clip = music[5];
                    audioSource.volume = 0.45f;
                    audioSource.pitch = newPitch;
                    audioSource.loop = false;
                    audioSource.Play();
                    while (start == true)
                    {
                        yield return null;
                    }
                    audioSource.Stop();
                    audioSource.clip = music[4];
                    audioSource.volume = 0.6f;
                    audioSource.pitch = 1f;
                    audioSource.loop = true;
                    audioSource.Play();
                    auxiliarMusic = 4;
                }
                break;
            case 3:
                {
                    audioSource.Stop();
                    audioSource.clip = music[6];
                    audioSource.loop = false;
                    audioSource.Play();
                    yield return null;
                }
                break;
            default:
                {
                    Debug.Log("Algo ocurrio con el BossMusicDirector F");
                    yield return null;
                }
                break;
        }
    }
}
