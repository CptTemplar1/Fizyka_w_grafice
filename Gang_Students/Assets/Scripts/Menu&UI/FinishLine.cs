using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Wykrywa przekroczenie linii mety przez gracza i inicjuje zachowania zwi�zane z zako�czeniem gry.
/// </summary>
/// <remarks>
/// Klasa FinishLine jest odpowiedzialna za wykrywanie momentu, gdy gracz przekracza lini� mety.
/// Wykorzystuje do tego Collider i sprawdza, czy obiekt, kt�ry go przekroczy�, ma tag "Player".
/// Po wykryciu gracza, inicjuje zachowanie zwi�zane z zako�czeniem gry, takie jak uruchomienie 
/// licznika czasu do ko�ca gry.
/// </remarks>
public class FinishLine : MonoBehaviour
{
    public TimeCounter timeCounter; ///< Skrypt TimeCounter obs�uguj�cy zachowanie timera po zako�czeniu gry.

    /// <summary>
    /// Wywo�ywana, gdy obiekt wchodzi w pole Collidera.
    /// Rozpoczyna odliczanie ko�ca gry, gdy gracz przekracza lini� mety.
    /// </summary>
    /// <param name="other">Collider innego obiektu.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timeCounter.StartCounter();
        }
    }
}
