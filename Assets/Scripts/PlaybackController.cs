using Cysharp.Threading.Tasks;
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
            UniTask.Void(async () =>
    {
        videoPlayer.Stop();
        videoPlayer.url = path;
                videoPlayer.Prepare();
                await UniTask.WaitUntil(() => videoPlayer.isPrepared);

                videoTexture.Release();
                videoTexture.width = (int)videoPlayer.width;
                videoTexture.height = (int)videoPlayer.height;
                videoTexture.Create();
        videoMaterial.SetTexture(MainTex, videoTexture);

        videoPlayer.Play();
            });
    }

    void OnApplicationQuit()
    {
        videoMaterial.SetTexture(MainTex, blackTexture);
    }
}
