using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public TimeCounter timeCounter; //skrypt TimeCounter obsługujący zachowanie timera po zakończeniu gry

    //Metoda rozpoczynająca odliczanie zakończenia gry po wejściu obiektu z tagiem "Player" w pole wykrywania Collidera
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timeCounter.StartCounter();
        }
    }
}
