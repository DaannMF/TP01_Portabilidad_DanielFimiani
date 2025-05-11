using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel {
    [Header("UI Elements")]
    [SerializeField] private Button buttonImage;
    [SerializeField] private RectTransform creditsButton;
    [SerializeField] private Button adsButton;

    private VerticalLayoutGroup verticalLayoutGroup;

    void Awake() {
        verticalLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();
        adsButton.onClick.AddListener(OnAdsButtonClicked);
        buttonImage.onClick.AddListener(OnClickButtonClicked);
    }

    void OnDestroy() {
        adsButton.onClick.RemoveListener(OnAdsButtonClicked);
        buttonImage.onClick.RemoveListener(OnClickButtonClicked);
    }

    public override void ApplySettings() {
        base.ApplySettings();

#if UNITY_ANDROID
        adsButton.gameObject.SetActive(true);
#else
        adsButton.gameObject.SetActive(false);
#endif

        // Set the padding and size of the UI elements based on the current orientation
        verticalLayoutGroup.padding = new RectOffset(currentSettings.padding, currentSettings.padding, currentSettings.padding, currentSettings.padding);

        // Set the size of the button image and buttons
        buttonImage.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(currentSettings.imageSize, currentSettings.imageSize);

        // Set the size of the buttons
        creditsButton.sizeDelta = new Vector2(currentSettings.buttonSize, currentSettings.buttonSize);
        adsButton.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(currentSettings.buttonSize, currentSettings.buttonSize);
    }

    private void OnAdsButtonClicked() {
        RewardAdsManager.Instance.ShowRewardedAd();
    }

    private void OnClickButtonClicked() {
        GameManager.Instance.OnTap();
    }
}
