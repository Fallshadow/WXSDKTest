using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeChatWASM;

public partial class WXSDKTest
{
    public void PrivacyAuthorize() {
        RequirePrivacyAuthorizeOption info = new RequirePrivacyAuthorizeOption();
        info.complete = (result) => {
            Debug.Log($"WX: PrivacyAuthorizeComplete");
        };
        info.fail = (result) => {
            GetSomethingFail("PrivacyAuthorize", result.errMsg);
        };
        info.success = (result) => {
            Debug.Log($"WX: PrivacyAuthorizeSuccess");
            GetSetting();
        };

        WX.RequirePrivacyAuthorize(info);
    }
}
