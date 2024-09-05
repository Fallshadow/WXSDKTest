using UnityEngine;
using UnityEngine.UI;
using WeChatWASM;

public class ShareTest : MonoBehaviour {
    public Button ShareBtn;

    private void Start() {
        ShareBtn.onClick.AddListener(Share);
    }
    public void Share() {
        ShareAppMessageOption shareAppMessageOption = new ShareAppMessageOption();
        shareAppMessageOption.title = "中秋博饼真有趣！";
        shareAppMessageOption.imageUrl = "/images/background.jpg";
        WX.ShareAppMessage(shareAppMessageOption);
    }
}

