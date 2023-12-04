using UnityEngine;

/// <summary>
/// Odtwarza d�wi�ki uderzenia obiektu w elementy otoczenia.
/// </summary>
public class ImpactSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource; ///< �r�d�o d�wi�ku do odtwarzania efekt�w uderzenia.

    [SerializeField]
    private AudioClip[] clips; ///< Tablica klip�w d�wi�kowych, kt�re mog� by� odtwarzane przy uderzeniu.

    private AudioClip chosenClip; ///< Wybrany klip d�wi�kowy do odtworzenia.

    /// <summary>
    /// Wywo�ywana przy kolizji obiektu. Odtwarza d�wi�k uderzenia, je�li spe�nione s� okre�lone warunki.
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
