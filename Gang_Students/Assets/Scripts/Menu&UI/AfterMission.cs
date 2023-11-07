using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AfterMission : MonoBehaviour
{
    public String mainMenuScene; // pole okre�laj�ce nazw� sceny z menu g��wnym

    public static bool GameIsPaused = false; // flaga okre�laj�ca czy gra jest zapauzowana

    public GameObject afterSuccessfulMissionUI; // schowany obiekt z interfejsem Menu, kt�ry jest pokazywany po wygranym poziomie

    // metoda pauzuj�ca gr� i w��czaj�ca panel wygranej
    public void FinishLevel()
    {
        afterSuccessfulMissionUI.SetActive(true);

        Time.timeScale = 0f;
        GameIsPaused = true;

        // W��cz kursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // metoda wychodz�ca z gry do Menu g��wnego
    public void GoBackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }
}
