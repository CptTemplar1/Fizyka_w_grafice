using UnityEngine;

/// <summary>
/// Steruje otwieraniem i zamykaniem drzwi w zale�no�ci od stanu trzech blok�w.
/// </summary>
/// <remarks>
/// Ta klasa wymaga trzech obiekt�w BlockTrigger oraz Animatora do animacji drzwi. 
/// Wykorzystuje r�wnie� �r�d�o d�wi�ku AudioSource do efekt�w d�wi�kowych. Otwieranie i zamykanie drzwi 
/// zale�� od stanu trzech blok�w.
/// </remarks>
public class BlocksOpenDoor : MonoBehaviour
{
    [SerializeField]
    private BlockTrigger trigger1, trigger2, trigger3; ///< Obiekty BlockTrigger do kontroli drzwi.

    [SerializeField]
    private Animator door; ///< Animator do animacji otwierania i zamykania drzwi.

    [SerializeField]
    private AudioSource audioSource; ///< AudioSource do odtwarzania efekt�w d�wi�kowych drzwi.

    private bool opened; ///< �ledzi czy drzwi s� otwarte.

    /// <summary>
    /// Sprawdza stan ka�dego wyzwalacza i odpowiednio kontroluje stan drzwi.
    /// </summary>
    /// <remarks>
    /// Je�li wszystkie wyzwalacze s� aktywne i drzwi nie s� jeszcze otwarte, inicjuje otwarcie drzwi.
    /// </remarks>
    void Update()
    {
        if(!opened && trigger1.block1 && trigger2.block2 && trigger3.block3)
        {
            opened = true;
            door.SetFloat("DoorSpeed", 1);
            door.Play("Open");
            
            audioSource.Stop();
            
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        
        else if(opened && (!trigger1.block1 || !trigger2.block2 || !trigger3.block3))
        {
            opened = false;
            door.SetFloat("DoorSpeed", -1);
            door.Play("Open");
            
            audioSource.Stop();
            
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
