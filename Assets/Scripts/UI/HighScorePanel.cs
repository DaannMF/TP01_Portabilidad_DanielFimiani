using TMPro;
using UnityEngine;
using UnityEngine.UI;

class HighScorePanel : BasePanel {
    [Header("UI Elements")]
    [SerializeField] private VerticalLayoutGroup[] layoutGroups;
    [SerializeField] private RectTransform closeButton;
    [SerializeField] private TMP_Text[] tMP_Texts;
    [SerializeField] private RectTransform medalImage;
    [SerializeField] private TMP_Text highScoreText;

    private void OnEnable() {
        SetHighScoreText();
    }

    public override void ApplySettings() {
        base.ApplySettings();

        // Set the padding and size of the UI elements based on the current orientation
        closeButton.sizeDelta = new Vector2(currentSettings.buttonSize, currentSettings.buttonSize);

        // Set the padding and size of the UI elements based on the current orientation
        foreach (VerticalLayoutGroup layoutGroup in layoutGroups)
            layoutGroup.padding = new RectOffset(currentSettings.padding, currentSettings.padding, currentSettings.padding, currentSettings.padding);

        // Set the size of the text elements
        foreach (TMP_Text text in tMP_Texts)
            text.fontSize = currentSettings.fontSize;

        // Set the size of the medal image
        medalImage.sizeDelta = new Vector2(currentSettings.imageSize, currentSettings.imageSize);
    }

    private void SetHighScoreText() {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = $"{highScore:00} POINTS";
    }
}