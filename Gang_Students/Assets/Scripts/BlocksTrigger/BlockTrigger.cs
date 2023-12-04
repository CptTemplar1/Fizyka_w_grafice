using UnityEngine;
/// <summary>
/// Wykrywa interakcje specyficznych bloków z wyzwalaczem i aktualizuje stany odpowiadaj¹ce blokom.
/// </summary>
/// <remarks>
/// Klasa s³u¿y do wykrywania, czy okreœlone bloki wesz³y w interakcjê z wyzwalaczem.
/// Ka¿dy wyzwalacz ma przypisany numer, który odpowiada numerowi bloku. Jeœli blok o odpowiednim
/// numerze wejdzie w kolizjê z wyzwalaczem, ustawia odpowiedni¹ flagê na true. Podobnie, kiedy blok
/// opuszcza wyzwalacz, flaga jest ustawiana na false.
/// </remarks>
public class BlockTrigger : MonoBehaviour
{
    [SerializeField]
    private int triggerNumber; ///< Numer wyzwalacza, odpowiadaj¹cy numerowi bloku.

    [HideInInspector]
    public bool block1, block2, block3; ///< Flagi stanów dla trzech ró¿nych bloków.

    /// <summary>
    /// Wywo³ywane, gdy obiekt wchodzi w kolizjê z wyzwalaczem.
    /// Sprawdza, czy kolizja dotyczy odpowiedniego bloku i ustawia odpowiedni¹ flagê.
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
    /// Wywo³ywane, gdy obiekt opuszcza kolizjê z wyzwalaczem.
    /// Sprawdza, czy kolizja dotyczy odpowiedniego bloku i resetuje odpowiedni¹ flagê.
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
