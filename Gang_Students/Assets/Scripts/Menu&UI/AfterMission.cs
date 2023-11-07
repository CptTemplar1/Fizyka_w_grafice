using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AfterMission : MonoBehaviour
{
    public String mainMenuScene; // pole okreœlaj¹ce nazwê sceny z menu g³ównym

    public static bool GameIsPaused = false; // flaga okreœlaj¹ca czy gra jest zapauzowana

    public GameObject afterSuccessfulMissionUI; // schowany obiekt z interfejsem Menu, który jest pokazywany po wygranym poziomie

    // metoda pauzuj¹ca grê i w³¹czaj¹ca panel wygranej
    public void FinishLevel()
    {
        afterSuccessfulMissionUI.SetActive(true);

        Time.timeScale = 0f;
        GameIsPaused = true;

        // W³¹cz kursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // metoda wychodz¹ca z gry do Menu g³ównego
    public void GoBackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}
