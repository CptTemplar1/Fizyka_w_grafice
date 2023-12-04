using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Wykrywa przekroczenie linii mety przez gracza i inicjuje zachowania zwi¹zane z zakoñczeniem gry.
/// </summary>
/// <remarks>
/// Klasa FinishLine jest odpowiedzialna za wykrywanie momentu, gdy gracz przekracza liniê mety.
/// Wykorzystuje do tego Collider i sprawdza, czy obiekt, który go przekroczy³, ma tag "Player".
/// Po wykryciu gracza, inicjuje zachowanie zwi¹zane z zakoñczeniem gry, takie jak uruchomienie 
/// licznika czasu do koñca gry.
/// </remarks>
public class FinishLine : MonoBehaviour
{
    public TimeCounter timeCounter; ///< Skrypt TimeCounter obs³uguj¹cy zachowanie timera po zakoñczeniu gry.

    /// <summary>
    /// Wywo³ywana, gdy obiekt wchodzi w pole Collidera.
    /// Rozpoczyna odliczanie koñca gry, gdy gracz przekracza liniê mety.
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
