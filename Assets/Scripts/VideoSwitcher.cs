using UnityEngine;
using UnityEngine.Video;
using System.IO;

public class VideoSwitcher : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component
    public string[] videoFileNames; // Names of video files in StreamingAssets
    private int currentVideoIndex = 0; // Track the current video

    void Start()
    {
        if (videoFileNames.Length > 0)
        {
            // Set the initial video from StreamingAssets
            string filePath = Path.Combine(Application.streamingAssetsPath, videoFileNames[currentVideoIndex]);
            videoPlayer.url = filePath;
            videoPlayer.Play();
        }
    }

    public void PlayNextVideo()
    {
        // Cycle to the next video
        currentVideoIndex = (currentVideoIndex + 1) % videoFileNames.Length;

        // Get the file path from StreamingAssets and play the video
        string filePath = Path.Combine(Application.streamingAssetsPath, videoFileNames[currentVideoIndex]);
        videoPlayer.url = filePath;
        videoPlayer.Play();
    }
}
