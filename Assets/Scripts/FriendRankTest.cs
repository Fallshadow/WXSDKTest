using UnityEngine;
using UnityEngine.UI;
using WeChatWASM;

[System.Serializable]
public class OpenDataMessage {
    // ���ڱ����¼����ͣ���Ӧ�������е�Type
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

        // ע������һ������չʾ�񵥣��ٷ�������
        // ����������߿��������õ�layout��������չʾ�����õ�canvas��С��Ϣ

        var p = RankBody.rectTransform.position;
        Debug.Log($"[FriendRankTest] ��Ⱦ�����λ�� p");
        Debug.Log($"[FriendRankTest] ��Ⱦ����Ĵ�С {RankBody.rectTransform.rect.width}  {RankBody.rectTransform.rect.height}");
        Debug.Log($"[FriendRankTest] ��Ļ�Ĵ�С {Screen.width}  {Screen.height}");
        Debug.Log($"[FriendRankTest] �����ֱ��ʵĴ�С {referenceResolution.x}  {referenceResolution.y}");

        int x = (int)p.x;
        int y = Screen.height - (int)p.y;
        int width = (int)((Screen.width / referenceResolution.x) * RankBody.rectTransform.rect.width);
        int height = (int)((Screen.height / referenceResolution.y) * RankBody.rectTransform.rect.height);
        Debug.Log($"[FriendRankTest] չʾ�� {Screen.width} {Screen.height} {referenceResolution.x} {referenceResolution.y} {x} {y} {width} {height}");
        WX.ShowOpenData(
            RankBody.texture,
            x,
            y,
            width,
            height
        );

        Debug.Log("[FriendRankTest] ��������");
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