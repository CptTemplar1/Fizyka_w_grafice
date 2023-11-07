using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public TimeCounter timeCounter; //skrypt TimeCounter obs�uguj�cy zachowanie timera po zako�czeniu gry

    //Metoda rozpoczynaj�ca odliczanie zako�czenia gry po wej�ciu obiektu z tagiem "Player" w pole wykrywania Collidera
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timeCounter.StartCounter();
        }
    }
}
