using UnityEngine;

/// <summary>
/// Odtwarza dŸwiêki uderzenia obiektu w elementy otoczenia.
/// </summary>
public class ImpactSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource; ///< ród³o dŸwiêku do odtwarzania efektów uderzenia.

    [SerializeField]
    private AudioClip[] clips; ///< Tablica klipów dŸwiêkowych, które mog¹ byæ odtwarzane przy uderzeniu.

    private AudioClip chosenClip; ///< Wybrany klip dŸwiêkowy do odtworzenia.

    /// <summary>
    /// Wywo³ywana przy kolizji obiektu. Odtwarza dŸwiêk uderzenia, jeœli spe³nione s¹ okreœlone warunki.
    /// </summary>
    /// <param name="col">Informacje o kolizji.</param>
    void OnCollisionEnter(Collision col)
    {
        if(!audioSource.isPlaying && col.gameObject.layer == LayerMask.NameToLayer("World"))
        {
            chosenClip = clips[Random.Range(0, clips.Length)];
            audioSource.clip = chosenClip;
            audioSource.Play();
        }
    }
}
