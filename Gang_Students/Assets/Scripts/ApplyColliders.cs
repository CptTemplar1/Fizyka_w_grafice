using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyColliders : MonoBehaviour
{
    /// Obiekt ragdolla gracza, z kt�rego pobieramy collidery
    public GameObject playerRagdoll;
    /// Lista dodatkowych element�w, kt�rych CapsuleCollidery maj� ingerowa� w zachowanie materia�u
    public List<CapsuleCollider> additionalCapsuleColliders = new List<CapsuleCollider>();
    /// Lista dodatkowych element�w, kt�rych CapsuleCollidery maj� ingerowa� w zachowanie materia�u
    public List<SphereCollider> additionalSphereColliders = new List<SphereCollider>();

    ///lista komponent�w CapsuleCollider obiektu (wliczaj�c w to komponenty znajduj�ce si� w dzieciach)                     
    private List<CapsuleCollider> capsuleColliderList = new List<CapsuleCollider>();
    ///lista komponent�w CapsuleCollider obiektu (wliczaj�c w to komponenty znajduj�ce si� w dzieciach)                     
    private List<SphereCollider> sphereColliderList = new List<SphereCollider>();

    /// <summary>
    /// Dodaje komponenty CapsuleCollider oraz SphereCollider obiektu (je�li istnieje) do listy, a nastepnie wywo�uje metod� przeszukuj�c� dzieci
    /// </summary>
    private void Start()
    {
        // Sprawdzamy, czy g��wny obiekt (playerRagdoll) posiada komponent CapsuleCollider 
        CapsuleCollider playerCapsuleCollider = playerRagdoll.GetComponent<CapsuleCollider>();
        if (playerCapsuleCollider != null)
        {
            capsuleColliderList.Add(playerCapsuleCollider);
        }
        // Sprawdzamy, czy g��wny obiekt (playerRagdoll) posiada komponent SphereCollider 
        SphereCollider playerSphereCollider = playerRagdoll.GetComponent<SphereCollider>();
        if (playerSphereCollider != null)
        {
            sphereColliderList.Add(playerSphereCollider);
        }

        // Nast�pnie przeszukujemy jego dzieci
        SearchForColliders(playerRagdoll.transform);

        // Dodajemy dodatkowe CapsuleCollidery (z listy additionalCapsuleColliders)
        capsuleColliderList.AddRange(additionalCapsuleColliders);
        // Dodajemy dodatkowe SphereCollidery (z listy additionalSphereColliders)
        sphereColliderList.AddRange(additionalSphereColliders);

        // Pobierz komponent Cloth z obiektu
        Cloth clothComponent = GetComponent<Cloth>();

        if (clothComponent != null)
        {
            //Dodanie listy CapsuleCollider�w w komponencie Cloth
            clothComponent.capsuleColliders = capsuleColliderList.ToArray();

            // Tworzenie tablicy ClothSphereColliderPair
            ClothSphereColliderPair[] sphereCollidersPairs = new ClothSphereColliderPair[sphereColliderList.Count];

            // Przypisanie SphereCollider�w do ClothSphereColliderPair
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
    /// Funkcja rekurencyjna do znajdowania wszystkich komponent�w CapsuleCollider oraz SphereCollider obiektu (przeszukuje rekurencyjnie dzieci dzieci).
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


            // Rekurencyjnie wywo�ujemy funkcj� dla dzieci obecnie przetwarzanego dziecka
            SearchForColliders(child);
        }
    }
}
