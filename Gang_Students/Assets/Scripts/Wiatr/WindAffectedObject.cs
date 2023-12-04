using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Klasa odpowiedzialna za efekt wiatru wp�ywaj�cy na gracza.
/// </summary>
public class WindAffectedObject : MonoBehaviour
{
    ///Flaga okre�laj�ca, czy obiekt znajduje si� wewn�trz wietrznej strefy
    private bool inWindZone = false;
    ///Referencja do strefy wiatru                                 
    private GameObject windArea;
    ///lista komponent�w Rigidbody obiektu (wliczaj�c w to komponenty znajduj�ce si� w dzieciach)                     
    private List<Rigidbody> rbList = new List<Rigidbody>();

    /// <summary>
    /// Dodaje komponent RigidBody obiektu (je�li istnieje) do listy, a nastepnie wywo�uje metod� przeszukuj�c� dzieci
    /// </summary>
    private void Start()
    {
        // Najpierw sprawdzamy, czy obiekt posiada komponent Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rbList.Add(rb);
        }

        // Nast�pnie przeszukujemy dzieci
        SearchForRigidbodies(transform);

        Debug.Log("Ilo�� RigidBody w ciele playera = " + rbList.Count);
    }

    /// <summary>
    /// Funkcja rekurencyjna do znajdowania wszystkich komponent�w RigidBody obiektu (przeszukuje rekurencyjnie dzieci dzieci.
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

            // Rekurencyjnie wywo�ujemy funkcj� dla dzieci obecnie przetwarzanego dziecka
            SearchForRigidbodies(child);
        }
    }

    /// <summary>
    /// Nadanie obiektowi si�y dzia�aj�cej w danym kierunku na podstawie danych konkretnego pola wiatru.
    /// Iterujemy po wszystkich elementach posiadaj�cych komponent RigidBody i ka�demu nadajemy dzia�aj�c� na niego si��.
    /// </summary>
    private void FixedUpdate()
    {
        if (inWindZone)
        {
            //Iteracja po ka�dym komponencie Rigidbody
            foreach (Rigidbody rb in rbList)
            {
                rb.AddForce(windArea.GetComponent<WindArea>().direction * windArea.GetComponent<WindArea>().strength);
            }
        }
    }

    /// <summary>
    /// Okre�lenie zachowania po wej�ciu w stref� wiatru
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WindArea"))
        {
            windArea = other.gameObject;
            inWindZone = true;
        }
    }

    /// <summary>
    /// Okre�lenie zachowania po wyj�ciu ze strefy wiatru
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