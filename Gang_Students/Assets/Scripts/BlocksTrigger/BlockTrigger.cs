using UnityEngine;
/// <summary>
/// Wykrywa interakcje specyficznych blok�w z wyzwalaczem i aktualizuje stany odpowiadaj�ce blokom.
/// </summary>
/// <remarks>
/// Klasa s�u�y do wykrywania, czy okre�lone bloki wesz�y w interakcj� z wyzwalaczem.
/// Ka�dy wyzwalacz ma przypisany numer, kt�ry odpowiada numerowi bloku. Je�li blok o odpowiednim
/// numerze wejdzie w kolizj� z wyzwalaczem, ustawia odpowiedni� flag� na true. Podobnie, kiedy blok
/// opuszcza wyzwalacz, flaga jest ustawiana na false.
/// </remarks>
public class BlockTrigger : MonoBehaviour
{
    [SerializeField]
    private int triggerNumber; ///< Numer wyzwalacza, odpowiadaj�cy numerowi bloku.

    [HideInInspector]
    public bool block1, block2, block3; ///< Flagi stan�w dla trzech r�nych blok�w.

    /// <summary>
    /// Wywo�ywane, gdy obiekt wchodzi w kolizj� z wyzwalaczem.
    /// Sprawdza, czy kolizja dotyczy odpowiedniego bloku i ustawia odpowiedni� flag�.
    /// </summary>
    /// <param name="other">Collider innego obiektu.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<blockType>())
        {
            if(other.GetComponent<blockType>().blockNumber == 1 && triggerNumber == 1)
            {
                block1 = true;
            }

            if(other.GetComponent<blockType>().blockNumber == 2 && triggerNumber == 2)
            {
                block2 = true;
            }

            if(other.GetComponent<blockType>().blockNumber == 3 && triggerNumber == 3)
            {
                block3 = true;
            }
        }
    }

    /// <summary>
    /// Wywo�ywane, gdy obiekt opuszcza kolizj� z wyzwalaczem.
    /// Sprawdza, czy kolizja dotyczy odpowiedniego bloku i resetuje odpowiedni� flag�.
    /// </summary>
    /// <param name="other">Collider innego obiektu.</param>
    void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<blockType>())
        {
            if(other.GetComponent<blockType>().blockNumber == 1 && triggerNumber == 1)
            {
                block1 = false;
            }

            if(other.GetComponent<blockType>().blockNumber == 2 && triggerNumber == 2)
            {
                block2 = false;
            }

            if(other.GetComponent<blockType>().blockNumber == 3 && triggerNumber == 3)
            {
                block3 = false;
            }
        }
    }
}
