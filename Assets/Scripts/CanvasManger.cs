using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrientationManager : MonoBehaviour {
    [Header("UI Elements")]
    [SerializeField] private int paddingLandScape = 50;
    [SerializeField] private int paddingPortrait = 200;
    [SerializeField] private float fontSizeLandScape = 72;
    [SerializeField] private float fontSizePortrait = 100;
    [SerializeField] private float clickButtonImageSizeLandScape = 200;
    [SerializeField] private float clickButtonImageSizePortrait = 350;
    [SerializeField] private float buttonSizeLandScpae = 70;
    [SerializeField] private float buttonSizePortrait = 150;
    [SerializeField] private GameObject clickButton;
    [SerializeField] private GameObject creditsButton;
    [SerializeField] private GameObject adsButton;

    private VerticalLayoutGroup verticalLayoutGroup;
    private TMP_Text[] texts;
    private DeviceOrientation orientation;

    void Awake() {
        verticalLayoutGroup = GetComponentInChildren<VerticalLayoutGroup>();
        texts = GetComponentsInChildren<TMP_Text>();
        orientation = Input.deviceOrientation;
    }

    void Update() {
        CheckOrientation();
    }

    private void CheckOrientation() {
        if (Screen.width > Screen.height) {
            if (orientation != DeviceOrientation.LandscapeLeft && orientation != DeviceOrientation.LandscapeRight) {
                orientation = Input.deviceOrientation;
                SetDefaultValues();
            }
        }
        else {
            if (orientation != DeviceOrientation.Portrait && orientation != DeviceOrientation.PortraitUpsideDown) {
                orientation = Input.deviceOrientation;
                SetDefaultValues();
            }
        }
    }

    private void SetDefaultValues() {
#if UNITY_ANDROID
        adsButton.SetActive(true);
#else
        adsButton.SetActive(false);
#endif

        if (Screen.width > Screen.height) {
            verticalLayoutGroup.padding = new RectOffset(paddingLandScape, paddingLandScape, paddingLandScape, paddingLandScape);
            clickButton.GetComponent<RectTransform>().sizeDelta = new Vector2(clickButtonImageSizeLandScape, clickButtonImageSizeLandScape);
            creditsButton.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonSizeLandScpae, buttonSizeLandScpae);
            adsButton.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonSizeLandScpae, buttonSizeLandScpae);
        }
        else {
            verticalLayoutGroup.padding = new RectOffset(paddingPortrait, paddingPortrait, paddingPortrait, paddingPortrait);
            clickButton.GetComponent<RectTransform>().sizeDelta = new Vector2(clickButtonImageSizePortrait, clickButtonImageSizePortrait);
            creditsButton.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonSizePortrait, buttonSizePortrait);
            adsButton.GetComponent<RectTransform>().sizeDelta = new Vector2(buttonSizePortrait, buttonSizePortrait);
        }

        foreach (TMP_Text text in texts) {
            if (Screen.width > Screen.height)
                text.fontSize = fontSizeLandScape;
            else
                text.fontSize = fontSizePortrait;
        }
    }
}
