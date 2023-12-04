using UnityEngine;

/// <summary>
/// Wykrywa, kiedy pi³ka wchodzi w specyficzny obszar i aktywuje animacjê oraz dŸwiêk.
/// </summary>
public class BasketTrigger : MonoBehaviour
{
    [SerializeField]
    private Animator slideDoor; ///< Animator zarz¹dzaj¹cy animacj¹ drzwi.

    [SerializeField]
    private AudioSource audioSource; ///< ród³o dŸwiêku do odtwarzania efektów dŸwiêkowych.

    /// <summary>
    /// Wywo³ywana, gdy obiekt wchodzi w pole Collidera.
    /// Aktywuje animacjê i dŸwiêk, gdy wykryta zostanie pi³ka.
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
