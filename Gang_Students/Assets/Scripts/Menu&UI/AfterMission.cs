using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Zarz�dza zachowaniami po zako�czeniu gry, takimi jak pauzowanie gry i wy�wietlanie interfejsu u�ytkownika.
/// </summary>
/// <remarks>
/// Klasa odpowiada za obs�ug� interfejsu po zako�czeniu gry. Umo�liwia pauzowanie gry i wy�wietlanie panelu
/// po pomy�lnie zako�czonej grze, jak r�wnie� umo�liwia powr�t do menu g��wnego.
/// </remarks>
public class AfterMission : MonoBehaviour
{
    public String mainMenuScene; ///< Nazwa sceny z menu g��wnym, do kt�rej mo�na powr�ci�.

    public static bool GameIsPaused = false; ///< Flaga okre�laj�ca, czy gra jest zapauzowana.

    public GameObject afterSuccessfulMissionUI; ///< Obiekt interfejsu u�ytkownika, kt�ry pojawia si� po wygranej grze.


    /// <summary>
    /// Pauzuje gr� i aktywuje panel z informacj� o zako�czeniu poziomu.
    /// </summary>
    public void FinishLevel()
    {
        afterSuccessfulMissionUI.SetActive(true);

        Time.timeScale = 0f;
        GameIsPaused = true;

        // W��cz kursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Realizuje powr�t do menu g��wnego gry.
    /// </summary>
    public void GoBackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}
