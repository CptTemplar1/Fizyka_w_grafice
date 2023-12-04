using UnityEngine;

/// <summary>
/// Wykrywa, kiedy pi�ka wchodzi w specyficzny obszar i aktywuje animacj� oraz d�wi�k.
/// </summary>
public class BasketTrigger : MonoBehaviour
{
    [SerializeField]
    private Animator slideDoor; ///< Animator zarz�dzaj�cy animacj� drzwi.

    [SerializeField]
    private AudioSource audioSource; ///< �r�d�o d�wi�ku do odtwarzania efekt�w d�wi�kowych.

    /// <summary>
    /// Wywo�ywana, gdy obiekt wchodzi w pole Collidera.
    /// Aktywuje animacj� i d�wi�k, gdy wykryta zostanie pi�ka.
    /// </summary>
    /// <param name="other">Collider innego obiektu.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            slideDoor.Play("slideOpen");
            audioSource.Play();
        }
    }
}
