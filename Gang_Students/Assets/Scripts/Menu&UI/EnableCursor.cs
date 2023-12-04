using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// W��cza kursor myszy i odblokowuje go w momencie inicjalizacji sceny.
/// </summary>
/// <remarks>
/// Klasa EnableCursor jest u�ywana do zarz�dzania widoczno�ci� i stanem blokady kursora myszy
/// w grze.
/// </remarks>
public class EnableCursor : MonoBehaviour
{
    void Awake()
    {
        // W��cz kursor
        Cursor.visible = true;

        // Odblokuj kursor
        Cursor.lockState = CursorLockMode.None;
    } 
}
