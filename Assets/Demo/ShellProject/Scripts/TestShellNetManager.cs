using UnityEngine;
using UnityWebSocket;

public partial class TestShellNetManager : MonoBehaviour {
    public static TestShellNetManager instance { get; private set; }

    private const string mainAddress = "wss://XXXXX.com/XXXXX";
    private WebSocket gameMainSocket;
    private float heartbeatInterval = 10f; // 心跳间隔时间（秒）
    private float timer = 0f; // 计时器

    private void Awake() {
        instance = this;
    }

    private void Update() {
        timer += Time.deltaTime;

        if(timer > heartbeatInterval) {
            SendHeartRequest();
            timer = 0f;
        }
    }

    public void WebSocketConnect() {
        gameMainSocket = new WebSocket(mainAddress);
        gameMainSocket.OnOpen += MainSocket_OnOpen;
        gameMainSocket.OnClose += MainSocket_OnClose;
        gameMainSocket.OnMessage += MainSocket_OnMessage;
        gameMainSocket.OnError += MainSocket_OnError;
        gameMainSocket.ConnectAsync();
    }

    public void SendHeartRequest() {
        // WSHeartPack heartPack = new WSHeartPack();
           
        // WSEmptyInfo emptyInfo = new WSEmptyInfo();
        // heartPack.data = emptyInfo;
           
        // string json = JsonUtility.ToJson(heartPack);
        // Debug.Log("[Bobing][Socket SendHeartRequest 1]");
        // gameMainSocket.SendAsync(json);
    }

    private void MainSocket_OnOpen(object sender, OpenEventArgs e) {
        Debug.Log($"[WebSocket][OnOpen] {mainAddress}");
    }

    private void MainSocket_OnMessage(object sender, MessageEventArgs e) {
        if(e.IsBinary) {
            if(e.RawData.Length > 0) {
                Debug.Log($"[WebSocket][OnMessage] 接受到服务器二进制信息 {e.RawData[0]}");
            }
            else {
                Debug.Log($"[WebSocket][OnMessage] 接受到服务器二进制信息 但是为空");
            }
            return;
        }
        else if(e.IsText) {
            Debug.Log($"[WebSocket][OnMessage] 接受到服务器文本信息 {e.Data}");
        }
    }

    private void MainSocket_OnClose(object sender, CloseEventArgs e) {
        Debug.Log($"[WebSocket][OnClose] {e.StatusCode} {e.Reason}");
    }

    private void MainSocket_OnError(object sender, ErrorEventArgs e) {
        Debug.Log($"[WebSocket][OnError] {e.Message}");
    }

}