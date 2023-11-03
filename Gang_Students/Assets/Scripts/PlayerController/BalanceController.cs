using UnityEngine;

/// <summary>
/// Klasa odpowiedzialna za kontrolê równowagi gracza.
/// </summary>
public class BalanceController : MonoBehaviour
{
    /// <summary>
    /// Referencja do obiektu PlayerController.
    /// </summary>
    [SerializeField]
    private PlayerController controller;

    /// <summary>
    /// Metoda wywo³ywana, gdy obiekt koliduje z innym obiektem.
    /// </summary>
    /// <param name="col">Obiekt Collision reprezentuj¹cy kolizjê.</param>
    void OnCollisionEnter(Collision col)
    {
        // Wywo³aj funkcjê PlayerGetUp() w obiekcie PlayerController, gdy ten obiekt koliduje.
        controller.PlayerGetUp();
    }
}
