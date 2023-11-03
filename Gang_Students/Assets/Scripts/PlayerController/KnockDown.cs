using UnityEngine;

/// <summary>
/// Klasa odpowiedzialna za obsługę upadku.
/// </summary>
public class KnockDown : MonoBehaviour
{
    /// <summary>
    /// Referencja do obiektu PlayerController.
    /// </summary>
    public PlayerController Player_Controller;

    /// <summary>
    /// Metoda wywoływana, gdy obiekt koliduje z innym obiektem.
    /// </summary>
    void OnCollisionEnter()
    {
        if (this.GetComponent<Rigidbody>().velocity.magnitude > 20)
        {
            // Jeśli prędkość obiektu przekracza 20, wywołaj funkcję KnockDown w PlayerController.
            Player_Controller.KnockDown();
        }
    }
}
