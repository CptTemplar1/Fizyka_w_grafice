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
        // wyjœcie z gry nie dzia³a w Unity, wiêc sprawdzamy debugiem
        Debug.Log("Wychodzenie z gry dzia³a");
        Application.Quit();
    }

    // metoda wczytuj¹ca pierwszy poziom
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }


    private void Update()
    {
        //TYMCZASOWE W£¥CZENIE KONSOLI (WYWO£ANIE B£ÊDU)
        if (Input.GetKeyDown(KeyCode.C))
        {
            throw new Exception("W£¥CZENIE KONSOLI - NIE ZWRACAÆ UWAGI");
        }
    }
}
