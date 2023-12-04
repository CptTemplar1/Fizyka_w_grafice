using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Zarz�dza menu pauzy w grze, umo�liwiaj�c pauzowanie i wznawianie rozgrywki oraz powr�t do menu g��wnego.
/// </summary>
/// <remarks>
/// Klasa PauseMenu umo�liwia graczowi pauzowanie i wznawianie gry, a tak�e powr�t do menu g��wnego 
/// lub zako�czenie gry.
/// </remarks>
public class PauseMenu : MonoBehaviour
{
    public String mainMenuScene; ///< Nazwa sceny z menu g��wnym do �adowania.

    public static bool GameIsPaused = false; ///< Flaga okre�laj�ca, czy gra jest zapauzowana.

    public GameObject pauseMenuUI; ///< Obiekt interfejsu u�ytkownika dla menu pauzy.

    public GameObject afterSuccessfulMission; ///< Obiekt ekranu po wygranej rundzie.

    /// <summary>
    /// Sprawdza naci�ni�cia klawisza ESC i zarz�dza stanem menu pauzy.
    /// </summary>
    private void Update()
    {
        // przycisk ESC w��cza PauseMenu
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
    /// Pauzuje gr� i aktywuje interfejs menu pauzy.
    /// </summary>
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true; // Zapauzuj wszystkie d�wi�ki
        GameIsPaused = true;

        // W��cz kursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Wznawia gr� i dezaktywuje interfejs menu pauzy.
    /// </summary>
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false; // Odpauzuj wszystkie d�wi�ki
        GameIsPaused = false;

        // Wy��cz kursor
        Cursor.visible = false;

        // Zablokuj kursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// �aduje menu g��wne gry.
    /// </summary>
    public void GoBackToMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false; // Odpauzuj wszystkie d�wi�ki
        SceneManager.LoadScene(mainMenuScene);
    }

    /// <summary>
    /// Wykonuje zako�czenie dzia�ania gry.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Wyj�cie z gry. Nie dzia�a w edytorze Unity");
        Application.Quit();
    }
}
