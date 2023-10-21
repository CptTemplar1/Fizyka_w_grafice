using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    // metoda zamykania gry
    public void QuitGame()
    {
        // wyj�cie z gry nie dzia�a w Unity, wi�c sprawdzamy debugiem
        Debug.Log("Wychodzenie z gry dzia�a");
        Application.Quit();
    }

    // metoda wczytuj�ca pierwszy poziom
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }


    private void Update()
    {
        //TYMCZASOWE W��CZENIE KONSOLI (WYWO�ANIE B��DU)
        if (Input.GetKeyDown(KeyCode.C))
        {
            throw new Exception("W��CZENIE KONSOLI - NIE ZWRACA� UWAGI");
        }
    }
}
