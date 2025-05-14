using UnityEngine;

public abstract class BasePanel : MonoBehaviour {
    [Header("Orientation Settings")]
    [SerializeField] public OrientationSettings landscapeSettings;
    [SerializeField] public OrientationSettings portraitSettings;

    private DeviceOrientation orientation;
    protected OrientationSettings currentSettings;

    void Awake() {
        ApplySettings();
    }

    void Update() {
        CheckOrientation();
    }

    private void CheckOrientation() {
        bool isLandscape = Screen.width > Screen.height;

        // Determine the new orientation based on the screen dimensions
        DeviceOrientation newOrientation = isLandscape
            ? DeviceOrientation.LandscapeLeft
            : DeviceOrientation.Portrait;

        // Verify if the orientation has changed
        if (newOrientation != orientation) {
            orientation = newOrientation;
            ApplySettings();
        }
    }

    public virtual void ApplySettings() {
        // Set the padding and size of the UI elements based on the current orientation
        currentSettings = Screen.width > Screen.height ? landscapeSettings : portraitSettings;
    }
}