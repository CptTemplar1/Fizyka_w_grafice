using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Zarz¹dza menu pauzy w grze, umo¿liwiaj¹c pauzowanie i wznawianie rozgrywki oraz powrót do menu g³ównego.
/// </summary>
/// <remarks>
/// Klasa PauseMenu umo¿liwia graczowi pauzowanie i wznawianie gry, a tak¿e powrót do menu g³ównego 
/// lub zakoñczenie gry.
/// </remarks>
public class PauseMenu : MonoBehaviour
{
    public String mainMenuScene; ///< Nazwa sceny z menu g³ównym do ³adowania.

    public static bool GameIsPaused = false; ///< Flaga okreœlaj¹ca, czy gra jest zapauzowana.

    public GameObject pauseMenuUI; ///< Obiekt interfejsu u¿ytkownika dla menu pauzy.

    public GameObject afterSuccessfulMission; ///< Obiekt ekranu po wygranej rundzie.

    /// <summary>
    /// Sprawdza naciœniêcia klawisza ESC i zarz¹dza stanem menu pauzy.
    /// </summary>
    private void Update()
    {
        // przycisk ESC w³¹cza PauseMenu
        //dodatkowo sprawdzamy czy nie jest aktywny ekran wygranej lub przegranej
        if (Input.GetKeyUp(KeyCode.Escape) && !afterSuccessfulMission.activeSelf)
        {
            if (GameIsPaused) 
                ResumeGame();
            else
                PauseGame();
        }
    }

    /// <summary>
    /// Pauzuje grê i aktywuje interfejs menu pauzy.
    /// </summary>
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true; // Zapauzuj wszystkie dŸwiêki
        GameIsPaused = true;

        // W³¹cz kursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Wznawia grê i dezaktywuje interfejs menu pauzy.
    /// </summary>
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false; // Odpauzuj wszystkie dŸwiêki
        GameIsPaused = false;

        // Wy³¹cz kursor
        Cursor.visible = false;

        // Zablokuj kursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// £aduje menu g³ówne gry.
    /// </summary>
    public void GoBackToMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false; // Odpauzuj wszystkie dŸwiêki
        SceneManager.LoadScene(mainMenuScene);
    }

    /// <summary>
    /// Wykonuje zakoñczenie dzia³ania gry.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Wyjœcie z gry. Nie dzia³a w edytorze Unity");
        Application.Quit();
    }
}
