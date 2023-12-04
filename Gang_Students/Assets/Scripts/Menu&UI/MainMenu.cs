using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

/// <summary>
/// Zarz�dza g��wnym menu gry, umo�liwiaj�c rozpocz�cie gry i wyj�cie z niej.
/// </summary>
/// <remarks>
/// Klasa MainMenu zawiera funkcjonalno�ci zwi�zane z g��wnym menu gry, 
/// takie jak rozpocz�cie nowej gry oraz wyj�cie z gry.
/// </remarks>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Wywo�uje zako�czenie dzia�ania gry.
    /// </summary>
    public void QuitGame()
    {
        // wyj�cie z gry nie dzia�a w Unity, wi�c sprawdzamy debugiem
        Debug.Log("Wychodzenie z gry dzia�a");
        Application.Quit();
    }

    /// <summary>
    /// Rozpoczyna gr�.
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Update jest wywo�ywany co klatk� gry. Zawiera tymczasow� funkcj� do testowania.
    /// </summary>
    private void Update()
    {
        //TYMCZASOWE W��CZENIE KONSOLI (WYWO�ANIE B��DU)
        if (Input.GetKeyDown(KeyCode.C))
        {
            throw new Exception("W��CZENIE KONSOLI - NIE ZWRACA� UWAGI");
        }
    }
}
