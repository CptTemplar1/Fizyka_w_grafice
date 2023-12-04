using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// W³¹cza kursor myszy i odblokowuje go w momencie inicjalizacji sceny.
/// </summary>
/// <remarks>
/// Klasa EnableCursor jest u¿ywana do zarz¹dzania widocznoœci¹ i stanem blokady kursora myszy
/// w grze.
/// </remarks>
public class EnableCursor : MonoBehaviour
{
    void Awake()
    {
        // W³¹cz kursor
        Cursor.visible = true;

        // Odblokuj kursor
        Cursor.lockState = CursorLockMode.None;
    } 
}
