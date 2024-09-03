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
    public CanvasScaler canvasScaler;

    public Button ShowRankCanvasRootButton;
    public Button HideRankCanvasRootButton;
    public RawImage RankBodyCanvasRoot;

    public Button ShowRankPanelRootButton;
    public Button HideRankPanelRootButton;
    public RawImage RankBodyPanelRoot;


    private void OnValidate() {
        SetPlayerScoreButton = transform.DeepFind("SetPlayerScoreButton").GetComponent<Button>();
        canvasScaler = transform.DeepFind("Canvas").GetComponent<CanvasScaler>();

        ShowRankCanvasRootButton = transform.DeepFind("ShowRankCanvasRootButton").GetComponent<Button>();
        HideRankCanvasRootButton = transform.DeepFind("HideRankCanvasRootButton").GetComponent<Button>();
        RankBodyCanvasRoot = transform.DeepFind("RankBodyCanvasRoot").GetComponent<RawImage>();

        ShowRankPanelRootButton = transform.DeepFind("ShowRankPanelRootButton").GetComponent<Button>();
        HideRankPanelRootButton = transform.DeepFind("HideRankPanelRootButton").GetComponent<Button>();
        RankBodyPanelRoot = transform.DeepFind("RankBodyPanelRoot").GetComponent<RawImage>();
    }

    void Start() {
        SetPlayerScoreButton.onClick.AddListener(SetPlayerScore);

        ShowRankCanvasRootButton.onClick.AddListener(ShowRankCanvasRoot);
        HideRankCanvasRootButton.onClick.AddListener(HideRankCanvasRoot);

        ShowRankPanelRootButton.onClick.AddListener(ShowRankPanelRoot);
        HideRankPanelRootButton.onClick.AddListener(HideRankPanelRoot);

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

    public void ShowRankCanvasRoot() {
        ShowRank(RankBodyCanvasRoot);
    }

    public void HideRankCanvasRoot() {
        HideRank(RankBodyCanvasRoot);
    }

    public void ShowRankPanelRoot() {
        ShowRank(RankBodyPanelRoot);
    }

    public void HideRankPanelRoot() {
        HideRank(RankBodyPanelRoot);
    }

    public void ShowRank(RawImage RankBody) {
        RankBody.gameObject.SetActive(true);

        var referenceResolution = canvasScaler.referenceResolution;

        // ע������һ������չʾ�񵥣��ٷ�������
        // ����������߿��������õ�layout��������չʾ�����õ�canvas��С��Ϣ

        var p = RankBody.transform.position;
        var w = RankBody.rectTransform.rect.width;
        var h = RankBody.rectTransform.rect.height;
        Debug.Log($"[FriendRankTest] ��Ⱦ�����λ�� {p.x} {p.y}");
        Debug.Log($"[FriendRankTest] ��Ⱦ����Ĵ�С {RankBody.rectTransform.rect.width}  {RankBody.rectTransform.rect.height}");
        Debug.Log($"[FriendRankTest] ��Ļ�Ĵ�С {Screen.width}  {Screen.height}");
        Debug.Log($"[FriendRankTest] �����ֱ��ʵĴ�С {referenceResolution.x}  {referenceResolution.y}");

        int x = (int)p.x;
        int y = Screen.height - (int)p.y;
        int width = (int)((Screen.width / referenceResolution.x) * w);
        int height = (int)((Screen.height / referenceResolution.y) * h);
        Debug.Log($"[FriendRankTest] չʾ�� {x} {y} {width} {height}");
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

    public void HideRank(RawImage RankBody) {
        RankBody.gameObject.SetActive(false);
        WX.HideOpenData();
    }

}