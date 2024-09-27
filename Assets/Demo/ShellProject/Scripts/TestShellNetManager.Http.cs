using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public partial class TestShellNetManager : MonoBehaviour {

    private const string wxLoginAddress = "https://XXXXX.com/XXXXX";

    public void HttpGameLogin() {
        // loginInfo 服务端所需登陆数据
        // WSWXLoginInfoRequest loginInfo = new WSWXLoginInfoRequest();
        // loginInfo.username = PlayerManager.instance.username;
        // loginInfo.wx_code = PlayerManager.instance.wx_code;
        // Debug.Log($"[Bobing][Http] name : {loginInfo.username} code : {loginInfo.wx_code}");
        StartCoroutine(PostLoginHttpRequest("json string"));
    }

    private IEnumerator PostLoginHttpRequest(string loginInfoString) {
        using(UnityWebRequest loginRequest = new UnityWebRequest(wxLoginAddress, UnityWebRequest.kHttpVerbPOST)) {
            UploadHandler uploader = new UploadHandlerRaw(Encoding.Default.GetBytes(loginInfoString));
            loginRequest.uploadHandler = uploader;
            loginRequest.SetRequestHeader("Content-Type", "application/json");
            DownloadHandler downloadHandler = new DownloadHandlerBuffer();
            loginRequest.downloadHandler = downloadHandler;

            yield return loginRequest.SendWebRequest();

            if(loginRequest.result == UnityWebRequest.Result.ConnectionError || loginRequest.result == UnityWebRequest.Result.ProtocolError) {
                Debug.LogError(loginRequest.error);
            }
            else {
                // 收到服务端Token后进行登陆
                Debug.Log("[Http] Form upload complete And receive data :" + loginRequest.downloadHandler.text);
                //WSWXLoginInfoReceive loginInfoReceive = JsonUtility.FromJson<WSWXLoginInfoReceive>(loginRequest.downloadHandler.text);
                //PlayerManager.instance.token = loginInfoReceive.token;
                //SendGameLogin(loginInfoReceive.token);
            }
        }
    }
}