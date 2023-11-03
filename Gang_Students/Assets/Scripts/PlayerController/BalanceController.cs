using UnityEngine;

/// <summary>
/// Klasa odpowiedzialna za kontrol� r�wnowagi gracza.
/// </summary>
public class BalanceController : MonoBehaviour
{
    /// <summary>
    /// Referencja do obiektu PlayerController.
    /// </summary>
    [SerializeField]
    private PlayerController controller;

    /// <summary>
    /// Metoda wywo�ywana, gdy obiekt koliduje z innym obiektem.
    /// </summary>
    /// <param name="col">Obiekt Collision reprezentuj�cy kolizj�.</param>
    void OnCollisionEnter(Collision col)
    {
        // Wywo�aj funkcj� PlayerGetUp() w obiekcie PlayerController, gdy ten obiekt koliduje.
        controller.PlayerGetUp();
    }
}
