using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindAffectedObject : MonoBehaviour
{
    ///Flaga okreœlaj¹ca, czy obiekt znajduje siê wewn¹trz wietrznej strefy
    private bool inWindZone = false;
    ///Referencja do strefy wiatru                                 
    private GameObject windArea;
    ///lista komponentów Rigidbody obiektu (wliczaj¹c w to komponenty znajduj¹ce siê w dzieciach)                     
    private List<Rigidbody> rbList = new List<Rigidbody>();

    /// <summary>
    /// Metoda przeszukuje wszystkie dzieci g³ównego rodzica i pobieramy ich komponenty Rigidbody do listy
    /// </summary>
    private void Start()
    {
        // Najpierw sprawdzamy, czy rodzic posiada komponent Rigidbody
        Rigidbody parentRb = GetComponentInParent<Rigidbody>();
        if (parentRb != null)
        {
            rbList.Add(parentRb);
        }

        // Nastêpnie przeszukujemy dzieci
        SearchForRigidbodies(transform);
    }

    /// <summary>
    /// Funkcja rekurencyjna do znajdowania wszystkich komponentów RigidBody obiektu (przeszukuje rekurencyjnie dzieci dzieci.
    /// </summary>
    /// <param name="parent"></param>
    private void SearchForRigidbodies(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Rigidbody childRb = child.GetComponent<Rigidbody>();
            if (childRb != null)
            {
                rbList.Add(childRb);
            }

            // Rekurencyjnie wywo³ujemy funkcjê dla dzieci obecnie przetwarzanego dziecka
            SearchForRigidbodies(child);
        }
    }

    /// <summary>
    /// Nadanie obiektowi si³y dzia³aj¹cej w danym kierunku na podstawie danych konkretnego pola wiatru.
    /// Iterujemy po wszystkich elementach posiadaj¹cych komponent RigidBody i ka¿demu nadajemy dzia³aj¹c¹ na niego si³ê.
    /// </summary>
    private void FixedUpdate()
    {
        if (inWindZone)
        {
            //Iteracja po ka¿dym komponencie Rigidbody
            foreach (Rigidbody rb in rbList)
            {
                rb.AddForce(windArea.GetComponent<WindArea>().direction * windArea.GetComponent<WindArea>().strength);
            }
        }
    }

    /// <summary>
    /// Okreœlenie zachowania po wejœciu w strefê wiatru
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WindArea"))
        {
            windArea = other.gameObject;
            inWindZone = true;
        }
        print(rbList.Count);
    }

    /// <summary>
    /// Okreœlenie zachowania po wyjœciu ze strefy wiatru
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("WindArea"))
        {
            inWindZone = false;
        }
    }
}