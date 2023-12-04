using UnityEngine;

/// <summary>
/// Steruje otwieraniem i zamykaniem drzwi w zale¿noœci od stanu trzech bloków.
/// </summary>
/// <remarks>
/// Ta klasa wymaga trzech obiektów BlockTrigger oraz Animatora do animacji drzwi. 
/// Wykorzystuje równie¿ Ÿród³o dŸwiêku AudioSource do efektów dŸwiêkowych. Otwieranie i zamykanie drzwi 
/// zale¿¹ od stanu trzech bloków.
/// </remarks>
public class BlocksOpenDoor : MonoBehaviour
{
    [SerializeField]
    private BlockTrigger trigger1, trigger2, trigger3; ///< Obiekty BlockTrigger do kontroli drzwi.

    [SerializeField]
    private Animator door; ///< Animator do animacji otwierania i zamykania drzwi.

    [SerializeField]
    private AudioSource audioSource; ///< AudioSource do odtwarzania efektów dŸwiêkowych drzwi.

    private bool opened; ///< Œledzi czy drzwi s¹ otwarte.

    /// <summary>
    /// Sprawdza stan ka¿dego wyzwalacza i odpowiednio kontroluje stan drzwi.
    /// </summary>
    /// <remarks>
    /// Jeœli wszystkie wyzwalacze s¹ aktywne i drzwi nie s¹ jeszcze otwarte, inicjuje otwarcie drzwi.
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
