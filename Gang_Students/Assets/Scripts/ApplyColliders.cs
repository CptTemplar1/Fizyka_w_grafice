using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyColliders : MonoBehaviour
{
    /// Obiekt ragdolla gracza, z którego pobieramy collidery
    public GameObject playerRagdoll;
    /// Lista dodatkowych elementów, których CapsuleCollidery maj¹ ingerowaæ w zachowanie materia³u
    public List<CapsuleCollider> additionalCapsuleColliders = new List<CapsuleCollider>();
    /// Lista dodatkowych elementów, których CapsuleCollidery maj¹ ingerowaæ w zachowanie materia³u
    public List<SphereCollider> additionalSphereColliders = new List<SphereCollider>();

    ///lista komponentów CapsuleCollider obiektu (wliczaj¹c w to komponenty znajduj¹ce siê w dzieciach)                     
    private List<CapsuleCollider> capsuleColliderList = new List<CapsuleCollider>();
    ///lista komponentów CapsuleCollider obiektu (wliczaj¹c w to komponenty znajduj¹ce siê w dzieciach)                     
    private List<SphereCollider> sphereColliderList = new List<SphereCollider>();

    /// <summary>
    /// Dodaje komponenty CapsuleCollider oraz SphereCollider obiektu (jeœli istnieje) do listy, a nastepnie wywo³uje metodê przeszukuj¹c¹ dzieci
    /// </summary>
    private void Start()
    {
        // Sprawdzamy, czy g³ówny obiekt (playerRagdoll) posiada komponent CapsuleCollider 
        CapsuleCollider playerCapsuleCollider = playerRagdoll.GetComponent<CapsuleCollider>();
        if (playerCapsuleCollider != null)
        {
            capsuleColliderList.Add(playerCapsuleCollider);
        }
        // Sprawdzamy, czy g³ówny obiekt (playerRagdoll) posiada komponent SphereCollider 
        SphereCollider playerSphereCollider = playerRagdoll.GetComponent<SphereCollider>();
        if (playerSphereCollider != null)
        {
            sphereColliderList.Add(playerSphereCollider);
        }

        // Nastêpnie przeszukujemy jego dzieci
        SearchForColliders(playerRagdoll.transform);

        // Dodajemy dodatkowe CapsuleCollidery (z listy additionalCapsuleColliders)
        capsuleColliderList.AddRange(additionalCapsuleColliders);
        // Dodajemy dodatkowe SphereCollidery (z listy additionalSphereColliders)
        sphereColliderList.AddRange(additionalSphereColliders);

        // Pobierz komponent Cloth z obiektu
        Cloth clothComponent = GetComponent<Cloth>();

        if (clothComponent != null)
        {
            //Dodanie listy CapsuleColliderów w komponencie Cloth
            clothComponent.capsuleColliders = capsuleColliderList.ToArray();

            // Tworzenie tablicy ClothSphereColliderPair
            ClothSphereColliderPair[] sphereCollidersPairs = new ClothSphereColliderPair[sphereColliderList.Count];

            // Przypisanie SphereColliderów do ClothSphereColliderPair
            for (int i = 0; i < sphereColliderList.Count; i++)
            {
                sphereCollidersPairs[i] = new ClothSphereColliderPair(sphereColliderList[i]);
            }

            // Przypisanie tablicy ClothSphereColliderPair do komponentu Cloth
            clothComponent.sphereColliders = sphereCollidersPairs;


            Debug.Log("CapsuleCollidersCount = " + capsuleColliderList.Count);
            Debug.Log("SphereCollidersCount = " + sphereColliderList.Count);
        }
    }

    /// <summary>
    /// Funkcja rekurencyjna do znajdowania wszystkich komponentów CapsuleCollider oraz SphereCollider obiektu (przeszukuje rekurencyjnie dzieci dzieci).
    /// </summary>
    /// <param name="parent"></param>
    private void SearchForColliders(Transform parent)
    {
        foreach (Transform child in parent)
        {
            CapsuleCollider childCapsuleCollider = child.GetComponent<CapsuleCollider>();
            if (childCapsuleCollider != null)
            {
                capsuleColliderList.Add(childCapsuleCollider);
            }

            SphereCollider childSphereCollider = child.GetComponent<SphereCollider>();
            if (childSphereCollider != null)
            {
                sphereColliderList.Add(childSphereCollider);
            }


            // Rekurencyjnie wywo³ujemy funkcjê dla dzieci obecnie przetwarzanego dziecka
            SearchForColliders(child);
        }
    }
}
