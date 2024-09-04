using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TestAvatarDownload : MonoBehaviour
{
    public string imageUrl = "https://thirdwx.qlogo.cn/mmopen/vi_32/L6SG55UJpKT1xOXDgjiaxjAymZAXIYScAAmZcHl976pgvLODHicVC6BHwBqwYjia6sib1QxAmorGctwoIjXic3pYhvA/132"; // 替换为你的图片URL  
    public RawImage targetImage; // 用于UI显示的RawImage  

    public Button downloadBtn;

    private void Start() {
        downloadBtn.onClick.AddListener(DownloadImage);
    }

    public void DownloadImage() {
        StartCoroutine(DownloadImageWeb());
    }

    IEnumerator DownloadImageWeb() {
        using(UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(imageUrl)) {
            yield return uwr.SendWebRequest();

            if(uwr.result != UnityWebRequest.Result.Success) {
                Debug.LogError("[bobing] Error downloading image: " + uwr.error);
            }
            else {
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);

                if(targetImage != null) {
                    targetImage.texture = texture;
                }
            }
        }
    }
}