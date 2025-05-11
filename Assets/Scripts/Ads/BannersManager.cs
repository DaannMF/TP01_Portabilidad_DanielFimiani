using UnityEngine;
using UnityEngine.Advertisements;

public class BannersManager : MonoBehaviour {
#pragma warning disable 0414
    [SerializeField] private string _androidUnityId = "Banner_Android";
    [SerializeField] private string _iosUnityId = "Banner_iOS";
#pragma warning restore 0414

    private static BannersManager _instance;
    public static BannersManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<BannersManager>();
            }
            return _instance;
        }
    }

    private string _unityId = null;

    void Start() {
#if UNITY_ANDROID
        _unityId = _androidUnityId;
#elif UNITY_IOS
        _unityId = _iosUnityId;
#endif

        if (string.IsNullOrEmpty(_unityId)) {
            Debug.LogError("Unity ID is not set for this platform");
            return;
        }

        if (!Advertisement.isSupported) {
            Debug.LogError("Advertisement is not supported on this platform");
            return;
        }
    }

    internal void Initialize() {
        BannerLoadOptions bannerLoadOptions = new() {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load(_unityId, bannerLoadOptions);
    }

    private void OnBannerLoaded() {
        Debug.Log("Banner loaded successfully");

        ShowBanner();
    }


    public void ShowBanner() {
        Advertisement.Banner.Show(_unityId);
    }
    public void HideBanner() {
        Advertisement.Banner.Hide();
    }

    private void OnBannerError(string message) {
        Debug.Log($"Banner error: {message}");
    }
}
