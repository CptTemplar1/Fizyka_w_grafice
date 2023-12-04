using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/// <summary>
/// Zarz¹dza g³ównym menu gry, umo¿liwiaj¹c rozpoczêcie gry i wyjœcie z niej.
/// </summary>
/// <remarks>
/// Klasa MainMenu zawiera funkcjonalnoœci zwi¹zane z g³ównym menu gry, 
/// takie jak rozpoczêcie nowej gry oraz wyjœcie z gry.
/// </remarks>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Wywo³uje zakoñczenie dzia³ania gry.
    /// </summary>
    public void QuitGame()
    {
        // wyjœcie z gry nie dzia³a w Unity, wiêc sprawdzamy debugiem
        Debug.Log("Wychodzenie z gry dzia³a");
        Application.Quit();
    }

    /// <summary>
    /// Rozpoczyna grê.
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Update jest wywo³ywany co klatkê gry. Zawiera tymczasow¹ funkcjê do testowania.
    /// </summary>
    private void Update()
    {
        //TYMCZASOWE W£¥CZENIE KONSOLI (WYWO£ANIE B£ÊDU)
        if (Input.GetKeyDown(KeyCode.C))
        {
            throw new Exception("W£¥CZENIE KONSOLI - NIE ZWRACAÆ UWAGI");
        }
    }
}
