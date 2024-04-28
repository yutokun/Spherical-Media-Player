using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.Video;

namespace yutokun.SphericalMediaPlayer
{
    public class VideoStarter : MonoBehaviour
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
            fileOpener.OnOpenedAsObservable
                      .Subscribe(OnOpen)
                      .AddTo(this);
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

                SetBetterFramerate(videoPlayer.frameRate);

                videoPlayer.Play();
            });
        }

        void SetBetterFramerate(float framerate)
        {
            Application.targetFrameRate = framerate switch
            {
                25 => 50,
                30 => 60,
                _ => (int)framerate,
            };
        }

        void OnApplicationQuit()
        {
            videoMaterial.SetTexture(MainTex, blackTexture);
        }
    }
}
