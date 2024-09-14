using UnityEngine;
using WeChatWASM;

enum WXSDKSetting {
    None = 0,
    UserInfo = 1 << 0,
    FuzzyLocation = 1 << 1,
}

public class TestShellWXSDKManager : MonoBehaviour {
    public static TestShellWXSDKManager instance { get; private set; }

    public string Wx_Code { private set; get; }
    public string NickName { private set; get; }
    public string AvatarUrl { private set; get; }
    public double FuzzyLocation_Latitude { private set; get; }
    public double FuzzyLocation_Longitude { private set; get; }

    private int wxsdkSetting;

    private void Awake() {
        instance = this;
    }

    public void InitSDK() {
        WX.InitSDK((code) => { Debug.Log($"[WX][InitSDK] Completed"); });
    }

    public void Login() {
        LoginOption info = new LoginOption();
        info.complete = (result) => { Debug.Log($"[WX][Login] Completed"); };
        info.fail = (result) => { Debug.Log($"[WX][Login] Fail : {result.errMsg}"); };
        info.success = (result) => {
            Debug.Log($"[WX][Login] Success : {result.code}");
            Wx_Code = result.code;
        };
        WX.Login(info);
    }

    public void GetSetting(int setting) {
        wxsdkSetting = setting;
        GetSettingOption info = new GetSettingOption();
        info.complete = (result) => { Debug.Log($"[WX][GetSetting] Completed"); };
        info.fail = (result) => { Debug.Log($"[WX][GetSetting] Fail : {result.errMsg}"); };
        info.success = (result) => { GetSettingSucess(result.authSetting); };
        WX.GetSetting(info);
    }

    private void GetSettingSucess(AuthSetting keyValuePairs) {
        foreach(var keyValue in keyValuePairs) {
            Debug.Log($"[WX][GetSetting] Sucess keyValue: {keyValue.Key} , {keyValue.Value}");
        }

        if((wxsdkSetting & (int)WXSDKSetting.UserInfo) > 0) {
            // �Ѿ���Ȩ������ֱ�ӵ��� GetUserInfo ��ȡͷ���ǳ�
            if(keyValuePairs.ContainsKey("scope.userInfo") && keyValuePairs["scope.userInfo"] == true) {
                Debug.Log($"[WX][GetSetting] ����Ȩ �û�������Ϣ ���Ի�ȡͷ���ǳ�");
                GetUserInfoOption info = new GetUserInfoOption();
                info.complete = (result) => { Debug.Log($"[WX][GetUserInfo] Completed"); };
                info.fail = (result) => { Debug.Log($"[WX][GetUserInfo] Fail : {result.errMsg}"); };
                info.success = (result) => { getUserInfoSucess(result.userInfo); };
                WX.GetUserInfo(info);
            }
            // δ��Ȩ����Ҫ�����û���Ϣ����Ȩ����
            else {
                Debug.Log($"[WX][GetSetting] δ��Ȩ �û�������Ϣ ��Ȩ��ť��ʾ");
                var button = WXBase.CreateUserInfoButton(0, 0, Screen.width, Screen.height, "zh_CN", false);
                button.Show();
                button.OnTap((result) => {
                    getWXUserInfoSucess(result.userInfo);
                    button.Hide();
                });
            }
        }

        if((wxsdkSetting & (int)WXSDKSetting.FuzzyLocation) > 0) {
            // �Ѿ���Ȩ������ֱ�ӵ��� GetFuzzyLocation ��ȡģ��λ����Ϣ
            if(keyValuePairs.ContainsKey("scope.userFuzzyLocation") && keyValuePairs["scope.userFuzzyLocation"] == true) {
                Debug.Log($"[WX][GetSetting] ����Ȩ λ����Ϣ ���Ի�ȡģ����γ��");
                getFuzzyLocation();
            }
            else {
                Debug.Log($"[WX][GetSetting] δ��Ȩ λ����Ϣ ��ȡAuthorize��Ȩ");
                AuthorizeOption authorizeOption = new AuthorizeOption();
                authorizeOption.scope = "scope.userFuzzyLocation";
                authorizeOption.complete = (result) => { Debug.Log($"[WX][Authorize] Completed"); };
                authorizeOption.fail = (result) => { Debug.Log($"[WX][Authorize] Fail : {result.errMsg}"); };
                authorizeOption.success = (result) => { getFuzzyLocation(); };
                WX.Authorize(authorizeOption);
            }
        }
    }

    private void getUserInfoSucess(UserInfo userInfo) {
        Debug.Log($"[WX][GetUserInfo] ����Ȩ���̵��û���Ϣ��ȡ GetUserInfoSucess ͷ��{userInfo.avatarUrl} �ǳƣ�{userInfo.nickName}");
        AvatarUrl = userInfo.avatarUrl;
        NickName = userInfo.nickName;
    }

    private void getWXUserInfoSucess(WXUserInfo wxUserInfo) {
        Debug.Log($"[WX][GetUserInfo] δ��Ȩ���̵��û���Ϣ��ȡ GetWXUserInfoSucess ͷ��{wxUserInfo.avatarUrl} �ǳƣ�{wxUserInfo.nickName}");
        AvatarUrl = wxUserInfo.avatarUrl;
        NickName = wxUserInfo.nickName;
    }

    private void getFuzzyLocation() {
        GetFuzzyLocationOption getFuzzyLocationOption = new GetFuzzyLocationOption();
        getFuzzyLocationOption.type = "wgs84";
        getFuzzyLocationOption.complete = (result) => { Debug.Log($"[WX][getFuzzyLocation] Completed"); };
        getFuzzyLocationOption.fail = (result) => { Debug.Log($"[WX][getFuzzyLocation] Fail : {result.errMsg}"); };
        getFuzzyLocationOption.success = (result) => {
            Debug.Log($"[WX][getFuzzyLocation] Success {result.latitude} {result.longitude}");
            FuzzyLocation_Latitude = result.latitude;
            FuzzyLocation_Longitude = result.longitude;
        };
        WX.GetFuzzyLocation(getFuzzyLocationOption);
    }
}