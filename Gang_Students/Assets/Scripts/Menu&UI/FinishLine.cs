using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public TimeCounter timeCounter; //skrypt TimeCounter obs³uguj¹cy zachowanie timera po zakoñczeniu gry

    //Metoda rozpoczynaj¹ca odliczanie zakoñczenia gry po wejœciu obiektu z tagiem "Player" w pole wykrywania Collidera
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timeCounter.StartCounter();
        }
    }
}
