using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool pause;

    //Variable que sirve para saber que escena cargar cuando se reinicia el nivel
    private int level;

    //Variable para saber si hay actualmente una cinemtica en curso
    //* Esta variable es alterada por el director de cada nivel
    private bool cinematic;

    // Update is called once per frame
    void Update()
    {
        //El menu se activa cuando se presiona la tecla escape
        if (Input.GetKeyDown(KeyCode.Escape) && !cinematic)
        {
            if (pause)
            {
                ResumeGame();
            }
            else
            {
                pause = true;
                pauseMenu.SetActive(true);
                //El juego se para mientras se esta en pausa
                Time.timeScale = 0f;
            }
        }
    }

    //Función que se llama para seguir con el juego al salir del menu de pausa
    private void ResumeGame()
    {
        pause = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    //Función que se llama cuando se presiona el boton de reiniciar
    private void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    //Función que se llama cuando se sale al menu principal
    private void ReturnToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    //Getters y Setters
    public void setLevel(int newLevel)
    {
        this.level = newLevel;
    }

    public int getLevel()
    {
        return level;
    }

    public bool isCinematic() {
		return this.cinematic;
	}

	public void setCinematic(bool cinematic) {
		this.cinematic = cinematic;
	}
}
