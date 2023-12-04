using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Zarz�dza odliczaniem czasu, na przyk�ad po zako�czeniu poziomu w grze.
/// </summary>
/// <remarks>
/// Klasa TimeCounter odpowiada za odliczanie czasu, kt�re mo�e by� u�yte do r�nych cel�w w grze,
/// takich jak czas oczekiwania po zako�czeniu poziomu.
/// </remarks>
public class TimeCounter : MonoBehaviour
{
    private AfterMission afterMission; ///< Skrypt AfterMission obs�uguj�cy zachowanie okna po zako�czeniu gry.

    float time = 5; ///< Czas do odliczania, np. po zako�czeniu poziomu.
    bool startPassedLevelCounter = false; ///< Flaga okre�laj�ca, czy odliczanie zosta�o rozpocz�te.
    private GameObject counter; ///< Obiekt interfejsu u�ytkownika dla wy�wietlania czasu.
    private TMP_Text timeCounterText; ///< Komponent tekstowy wy�wietlaj�cy odliczanie czasu.

    private void Start()
    {
        afterMission = GameObject.Find("AfterMission").GetComponentInChildren<AfterMission>();
        counter = transform.GetChild(0).gameObject;
        timeCounterText = counter.GetComponent<TMP_Text>();
    }

    /// <summary>
    /// Update jest wywo�ywany co klatk� gry, zarz�dza odliczaniem czasu.
    /// </summary>
    void Update()
    {
        //odliczanie po wygranym poziomie
        if (startPassedLevelCounter == true)
        {
            counter.SetActive(true);
            time -= Time.deltaTime;
            timeCounterText.text = Mathf.Clamp(Mathf.CeilToInt(time), 0, int.MaxValue).ToString();

            if (time <= 0)
            {
                counter.SetActive(false);
                startPassedLevelCounter = false;
                afterMission.FinishLevel();
            }
        }
    }

    /// <summary>
    /// Metoda rozpoczyna odliczanie czasu
    /// </summary>
    public void StartCounter()
    {
        startPassedLevelCounter = true;
    }

    /// <summary>
    /// Metoda zatrzymuje odliczanie czasu
    /// </summary>
    public void StopCounter()
    {
        startPassedLevelCounter = false;
    }
}
