using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Zarz¹dza odliczaniem czasu, na przyk³ad po zakoñczeniu poziomu w grze.
/// </summary>
/// <remarks>
/// Klasa TimeCounter odpowiada za odliczanie czasu, które mo¿e byæ u¿yte do ró¿nych celów w grze,
/// takich jak czas oczekiwania po zakoñczeniu poziomu.
/// </remarks>
public class TimeCounter : MonoBehaviour
{
    private AfterMission afterMission; ///< Skrypt AfterMission obs³uguj¹cy zachowanie okna po zakoñczeniu gry.

    float time = 5; ///< Czas do odliczania, np. po zakoñczeniu poziomu.
    bool startPassedLevelCounter = false; ///< Flaga okreœlaj¹ca, czy odliczanie zosta³o rozpoczête.
    private GameObject counter; ///< Obiekt interfejsu u¿ytkownika dla wyœwietlania czasu.
    private TMP_Text timeCounterText; ///< Komponent tekstowy wyœwietlaj¹cy odliczanie czasu.

    private void Start()
    {
        afterMission = GameObject.Find("AfterMission").GetComponentInChildren<AfterMission>();
        counter = transform.GetChild(0).gameObject;
        timeCounterText = counter.GetComponent<TMP_Text>();
    }

    /// <summary>
    /// Update jest wywo³ywany co klatkê gry, zarz¹dza odliczaniem czasu.
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
