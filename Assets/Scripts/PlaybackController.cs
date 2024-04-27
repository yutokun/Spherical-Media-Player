using UnityEngine;
using UnityEngine.Video;

public class PlaybackController : MonoBehaviour
{
    [SerializeField]
    FileOpener fileOpener;

    [SerializeField]
    VideoPlayer videoPlayer;

    [SerializeField]
    Material videoMaterial;

    [SerializeField]
    RenderTexture videoTexture;

    Texture blackTexture;
    static readonly int MainTex = Shader.PropertyToID("_MainTex");

    void Awake()
    {
        blackTexture = videoMaterial.GetTexture(MainTex);
        fileOpener.OnOpen += OnOpen;
    }

    void OnOpen(string path)
    {
        videoPlayer.Stop();
        videoPlayer.url = path;
        videoMaterial.SetTexture(MainTex, videoTexture);
        videoPlayer.Play();
    }

    void OnApplicationQuit()
    {
        videoMaterial.SetTexture(MainTex, blackTexture);
    }
}
