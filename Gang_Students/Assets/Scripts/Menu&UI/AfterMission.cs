using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Zarz¹dza zachowaniami po zakoñczeniu gry, takimi jak pauzowanie gry i wyœwietlanie interfejsu u¿ytkownika.
/// </summary>
/// <remarks>
/// Klasa odpowiada za obs³ugê interfejsu po zakoñczeniu gry. Umo¿liwia pauzowanie gry i wyœwietlanie panelu
/// po pomyœlnie zakoñczonej grze, jak równie¿ umo¿liwia powrót do menu g³ównego.
/// </remarks>
public class AfterMission : MonoBehaviour
{
    public String mainMenuScene; ///< Nazwa sceny z menu g³ównym, do której mo¿na powróciæ.

    public static bool GameIsPaused = false; ///< Flaga okreœlaj¹ca, czy gra jest zapauzowana.

    public GameObject afterSuccessfulMissionUI; ///< Obiekt interfejsu u¿ytkownika, który pojawia siê po wygranej grze.


    /// <summary>
    /// Pauzuje grê i aktywuje panel z informacj¹ o zakoñczeniu poziomu.
    /// </summary>
    public void FinishLevel()
    {
        afterSuccessfulMissionUI.SetActive(true);

        Time.timeScale = 0f;
        GameIsPaused = true;

        // W³¹cz kursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Realizuje powrót do menu g³ównego gry.
    /// </summary>
    public void GoBackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}
