using UnityEngine;

public class TestShellGameManager : MonoBehaviour {
    public static TestShellGameManager instance { get; private set; }
    private void Awake() {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        TestShellWXSDKManager.instance.InitSDK();
        TestShellWXSDKManager.instance.Login();
        TestShellWXSDKManager.instance.GetSetting((int)(WXSDKSetting.UserInfo | WXSDKSetting.FuzzyLocation));

        // TestShellNetManager.instance.WebSocketConnect();
        // TestShellNetManager.instance.HttpGameLogin();
    }
}