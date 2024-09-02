using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeChatWASM;

public partial class WXSDKTest : MonoBehaviour
{
    void Start()
    {
        WX.InitSDK((int code) => {
            PrivacyAuthorize();
        });
    }

    private void GetSomethingFail(string something, string errMsg) {
        Debug.Log($"WX: {something} Fail Error : {errMsg}");
    }
}
