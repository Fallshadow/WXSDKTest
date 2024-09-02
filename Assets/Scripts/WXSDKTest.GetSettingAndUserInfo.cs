using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeChatWASM;

public partial class WXSDKTest
{
    public void GetSetting() {
        GetSettingOption info = new GetSettingOption();
        info.complete = (result) => {
            GetSettingComplete();
        };
        info.fail = (result) => {
            GetSomethingFail("GetSetting", result.errMsg);
        };
        info.success = (result) => {
            GetSettingSucess(result.authSetting);
        };

        WX.GetSetting(info);
    }

    private void GetSettingSucess(AuthSetting keyValuePairs) {
        foreach(var keyValue in keyValuePairs) {
            Debug.Log($"WX: GetSettingSucess keyValue: {keyValue.Key} , {keyValue.Value}");
        }

        // 已经授权，可以直接调用 getUserInfo 获取头像昵称
        if(keyValuePairs.ContainsKey("scope.userInfo")) {
            GetUserInfoOption info = new GetUserInfoOption();
            info.complete = (result) => {

            };
            info.fail = (result) => {
                GetSomethingFail("GetUserInfoOption", result.errMsg);
            };
            info.success = (result) => {
                GetUserInfoSucess(result.userInfo);
            };
            WX.GetUserInfo(info);
        }
        else {
            Debug.Log($"WX: 未授权用户信息，调用授权窗口按钮!");
            var button = WXBase.CreateUserInfoButton(0, 0, Screen.width, Screen.height, "zh_CN", false);
            button.OnTap((result) => {
                GetButtonUserInfoSucess(result.userInfo);
            });
        }
    }

    private void GetUserInfoSucess(UserInfo userInfo) {
        Debug.Log($"WX: GetUserInfoSucess");
        Debug.Log(userInfo);
    }

    private void GetButtonUserInfoSucess(WXUserInfo wxUserInfo) {
        Debug.Log($"WX: GetButtonUserInfoSucess");
        Debug.Log(wxUserInfo);
    }

    private void GetSettingComplete() {
        Debug.Log($"WX: GetSettingComplete");
    }
}
