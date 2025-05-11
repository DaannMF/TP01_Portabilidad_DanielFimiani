using UnityEngine;

public abstract class BasePanel : MonoBehaviour {
    [Header("Orientation Settings")]
    [SerializeField] public OrientationSettings landscapeSettings;
    [SerializeField] public OrientationSettings portraitSettings;

    private DeviceOrientation orientation;
    protected OrientationSettings currentSettings;

    void Awake() {
        orientation = Input.deviceOrientation;
        ApplySettings();
    }

    void Update() {
        CheckOrientation();
    }

    private void CheckOrientation() {
        bool isLandscape = Screen.width > Screen.height;

        bool orientationChanged = isLandscape
            ? orientation != DeviceOrientation.LandscapeLeft && orientation != DeviceOrientation.LandscapeRight
            : orientation != DeviceOrientation.Portrait && orientation != DeviceOrientation.PortraitUpsideDown;

        if (orientationChanged) {
            orientation = Input.deviceOrientation;
            ApplySettings();
        }
    }

    public virtual void ApplySettings() {
        // Set the padding and size of the UI elements based on the current orientation
        currentSettings = Screen.width > Screen.height ? landscapeSettings : portraitSettings;
    }
}