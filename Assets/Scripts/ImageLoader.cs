using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;

public class ImageLoader : MonoBehaviour
{
    public Image targetImage; // Reference to the UI Image component where the image will be displayed
    public string imageFileName = "Image.jpg"; // Name of the image file in StreamingAssets

    void Start()
    {
        StartCoroutine(LoadImage());
    }

    private IEnumerator LoadImage()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, imageFileName);

        // Use UnityWebRequest to load the image as a texture
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(filePath))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.Success)
            {
                // Get the texture and apply it to the UI Image component
                Texture2D texture = DownloadHandlerTexture.GetContent(uwr);
                targetImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
            else
            {
                Debug.LogError("Failed to load image: " + uwr.error);
            }
        }
    }
}
