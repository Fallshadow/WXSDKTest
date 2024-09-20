using UnityEngine;


public class TestShellLoginManager : MonoBehaviour {

    public static TestShellLoginManager instance { get; private set; }

    private void Awake() {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
#if WECHAT
        TestShellWXSDKManager.instance.InitSDK(LoginAuthorize);
#endif
    }

    private void LoginAuthorize() {
        TestShellWXSDKManager.instance.Login();
        TestShellWXSDKManager.instance.GetSetting((int)(WXSDKSetting.UserInfo));
    }
}