using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour {

    [Header("Game")]
    [SerializeField] private float timeLimit = 10f;

    [Header("UI")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text clicksText;
    [SerializeField] private TMP_Text highScoreText;

    private float timer;
    private int clicks;
    private int highScore;
    private bool timerActive = false;

    void Start() {
        timer = timeLimit;
        clicks = 0;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Update() {
        ShowStas();
        if (timerActive) {
            CountDown();
        }
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
                highScore = clicks;
                PlayerPrefs.SetInt("HighScore", highScore);
            }
        }
    }

    private void OnTap(InputValue _) {
        if (timerActive) clicks++;
        else if (timer >= 0) timerActive = true;
    }
}
