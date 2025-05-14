using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Game")]
    [SerializeField] private float timeLimit = 10f;
    [SerializeField] private float reward = 2f;

    [Header("UI")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text clicksText;
    [SerializeField] private TMP_Text highScoreText;
    [SerializeField] private GameObject buttonsPanel;
    [SerializeField] private GameObject highScorePanel;

    private static GameManager _instance;
    public static GameManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }

    private float timer;
    private int clicks;
    private int highScore;
    private bool timerActive;

    void Start() {
        SetDefaultValues();
    }

    void Update() {
        ShowStas();
        if (timerActive) CountDown();
    }

    private void ShowStas() {
        int seconds = Mathf.FloorToInt(timer);
        int milliseconds = Mathf.FloorToInt((timer - seconds) * 100);

        timerText.text = $"Tiempo: {seconds:00}:{milliseconds:00}";
        clicksText.text = $"{clicks:00} clicks";
        highScoreText.text = $"High Score: {highScore:00}";
    }

    private void CountDown() {
        timer -= Time.deltaTime;
        timer = Math.Clamp(timer, 0, timeLimit);
        if (timer <= 0) {
            timerActive = false;
            if (clicks > highScore) {
                HandleHighScore();
            }
            else {
                InterstitialManager.Instance.ShowInterstitialAd();
            }

            SetDefaultValues();
        }
    }

    public void OnTap() {
        if (timerActive) clicks++;
        else if (timer >= 0) {
            timerActive = true;
            buttonsPanel.SetActive(false);
        }
    }

    public void AddReward() {
        timer += reward;
    }

    private void HandleHighScore() {
        if (clicks > highScore) {
            highScore = clicks;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
            highScorePanel.SetActive(true);
        }
    }

    public void SetDefaultValues() {
        timer = timeLimit;
        clicks = 0;
        timerActive = false;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        buttonsPanel.SetActive(true);
    }
}
