using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneChanger : MonoBehaviour
{
    public VideoPlayer myVideoPlayer;
    public string nextSceneName;

    void Start()
    {
        myVideoPlayer.loopPointReached += LoadNextScene;
    }

    void LoadNextScene(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneName);
    }

    void OnDestroy()
    {
        myVideoPlayer.loopPointReached -= LoadNextScene;
    }
}