using UnityEngine;
using UnityEngine.UI;
using WeChatWASM;

[System.Serializable]
public class OpenDataMessage {
    // 用于表明事件类型，对应开放域中的Type
    public string type;

    public string shareTicket;

    public int score;
}

public class FriendRankTest : MonoBehaviour {
    public Button SetPlayerScoreButton;
    public Button ShowRankButton;
    public Button HideRankButton;
    public RawImage RankBody;
    public CanvasScaler canvasScaler;

    void Start() {
        ShowRankButton.onClick.AddListener(ShowRank);
        HideRankButton.onClick.AddListener(HideRank);
        SetPlayerScoreButton.onClick.AddListener(SetPlayerScore);

        WX.InitSDK((int code) => {

        });
    }

    public void SetPlayerScore() {
        OpenDataMessage msgData = new OpenDataMessage();
        msgData.type = "setUserRecord";
        msgData.score = Random.Range(1, 1000);

        string msg = JsonUtility.ToJson(msgData);

        Debug.Log(msg);
        WX.GetOpenDataContext().PostMessage(msg);
    }

    public void ShowRank() {
        RankBody.gameObject.SetActive(true);

        var referenceResolution = canvasScaler.referenceResolution;

        // 注意这里一定是先展示榜单，再发送申请
        // 发送申请这边开放域有用到layout，依赖于展示榜单设置的canvas大小信息

        var p = RankBody.rectTransform.position;
        Debug.Log($"[FriendRankTest] 渲染主体的位置 p");
        Debug.Log($"[FriendRankTest] 渲染主体的大小 {RankBody.rectTransform.rect.width}  {RankBody.rectTransform.rect.height}");
        Debug.Log($"[FriendRankTest] 屏幕的大小 {Screen.width}  {Screen.height}");
        Debug.Log($"[FriendRankTest] 画布分辨率的大小 {referenceResolution.x}  {referenceResolution.y}");

        int x = (int)p.x;
        int y = Screen.height - (int)p.y;
        int width = (int)((Screen.width / referenceResolution.x) * RankBody.rectTransform.rect.width);
        int height = (int)((Screen.height / referenceResolution.y) * RankBody.rectTransform.rect.height);
        Debug.Log($"[FriendRankTest] 展示榜单 {Screen.width} {Screen.height} {referenceResolution.x} {referenceResolution.y} {x} {y} {width} {height}");
        WX.ShowOpenData(
            RankBody.texture,
            x,
            y,
            width,
            height
        );

        Debug.Log("[FriendRankTest] 发送申请");
        OpenDataMessage msgData = new OpenDataMessage();
        msgData.type = "showFriendsRank";
        string msg = JsonUtility.ToJson(msgData);
        WX.GetOpenDataContext().PostMessage(msg);
    }

    public void HideRank() {
        RankBody.gameObject.SetActive(false);
        WX.HideOpenData();
    }

}