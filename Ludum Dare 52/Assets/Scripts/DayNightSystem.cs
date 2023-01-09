using System;
using System.Collections;
using System.Collections.Generic;
using SupanthaPaul;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DayNightSystem : MonoBehaviour
{
    public static DayNightSystem instance;
    public enum GamePhaseEnum
    {
        Day,
        Night
    }
    [SerializeField] private float dayTimerMax = 300f;
    [SerializeField] private float startDelay = 5f;
    [SerializeField] private GameObject timerPanel;
    [SerializeField] private GameObject timeUpPanel;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI dayText;
    [Header("Events")] 
    public UnityEvent onDayStartedEvent;
    [SerializeField] private UnityEvent onNightStartedEvent;

    private GamePhaseEnum _currentGamePhase;
    private bool _timerStarted = false;
    private float _dayTimer;
    private int _currentDay = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this && instance != null)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        ResetTimer();
        WavesManager.instance.AllDeadEvent += InitiateDayTimer;

    }

    private void Update()
    {
        if (_timerStarted)
        {
            _dayTimer -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(_dayTimer / 60F);
            int seconds = Mathf.FloorToInt(_dayTimer - minutes * 60);
            string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
            timerText.text = niceTime;
            if (_dayTimer <= 10f)
            {
                timerText.color = Color.red;
            }

            if (_dayTimer <= 0f)
            {
                StopTimer();
            }
        }
    }

    public void InitiateDayTimer(float extraDelay=0f)
    {
        ResetTimer();
        _currentGamePhase = GamePhaseEnum.Day;
        _currentDay += 1;
        dayText.text = "Day " + _currentDay;
        onDayStartedEvent.Invoke();
        _dayTimer = dayTimerMax;
        Invoke(nameof(StartTimer), startDelay+extraDelay);
    }

    private void StartTimer()
    {
        _timerStarted = true;
        timerPanel.SetActive(true);
        timeUpPanel.SetActive(false);
    }

    public void StopTimer()
    {
        _currentGamePhase = GamePhaseEnum.Night;
        onNightStartedEvent.Invoke();
        _timerStarted = false;
        timerPanel.SetActive(false);
        timeUpPanel.SetActive(true);
        
    }

    public void ResetTimer()
    {
        _dayTimer = dayTimerMax;
        timerText.color = Color.white;
        timerPanel.SetActive(false);
        timeUpPanel.SetActive(false);
    }
}
