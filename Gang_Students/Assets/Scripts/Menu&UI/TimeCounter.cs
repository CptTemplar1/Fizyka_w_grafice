using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    private AfterMission afterMission; //skrypt AfterMission obs³uguj¹cy zachowanie okna po zakoñczeniu gry

    float time = 5; //czas do odliczania po np zakonczeniu lvla
    bool startPassedLevelCounter = false;
    private GameObject counter;
    private TMP_Text timeCounterText;

    private void Start()
    {
        afterMission = GameObject.Find("AfterMission").GetComponentInChildren<AfterMission>();
        counter = transform.GetChild(0).gameObject;
        timeCounterText = counter.GetComponent<TMP_Text>();
    }

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
