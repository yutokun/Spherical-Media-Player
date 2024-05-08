using System.IO;
using Cysharp.Threading.Tasks;
using Mochineko.StbImageSharpForUnity;
using R3;
using UnityEngine;
using UnityEngine.Video;

namespace yutokun.SphericalMediaPlayer
{
    public class MediaPlayer : MonoBehaviour
    {
        [SerializeField]
        FileOpener fileOpener;

        [SerializeField]
        VideoPlayer videoPlayer;

        [SerializeField]
        Material mediaMaterial;

        [SerializeField]
        RenderTexture videoTexture;

        Texture blackTexture;
        static readonly int MainTex = Shader.PropertyToID("_MainTex");

        public string Path { get; private set; }

        void Awake()
        {
            blackTexture = mediaMaterial.GetTexture(MainTex);
            fileOpener.OnOpenedAsObservable
                      .Subscribe(OnOpen)
                      .AddTo(this);
        }

        void OnOpen(string path)
        {
            if (path.IsVideoPath())
            {
                Path = path;
                StopVideo();
                PlayVideoAsync(path).Forget();
            }
            else if (path.IsPhotoPath())
            {
                Path = path;
                StopVideo();
                ShowPhotoAsync(path).Forget();
            }
        }

        async UniTaskVoid PlayVideoAsync(string path)
        {
            videoPlayer.url = path;
            videoPlayer.Prepare();
            await UniTask.WaitUntil(() => videoPlayer.isPrepared);

            videoTexture.Release();
            videoTexture.width = (int)videoPlayer.width;
            videoTexture.height = (int)videoPlayer.height;
            videoTexture.Create();
            mediaMaterial.SetTexture(MainTex, videoTexture);

            SetBetterFramerate(videoPlayer.frameRate);

            videoPlayer.Play();

            return;

            void SetBetterFramerate(float framerate)
            {
                Application.targetFrameRate = framerate switch
                {
                    25 => 50,
                    30 => 60,
                    _ => (int)framerate,
                };
            }
        }

        void StopVideo()
        {
            videoPlayer.Stop();
            videoPlayer.url = null;
        }

        async UniTaskVoid ShowPhotoAsync(string path)
        {
            var data = await File.ReadAllBytesAsync(path);

            await UniTask.SwitchToThreadPool();
            var result = ImageDecoder.DecodeImage(data);
            await UniTask.SwitchToMainThread();

            var tex2d = result.ToTexture2D();
            mediaMaterial.SetTexture(MainTex, tex2d);
        }


        void OnApplicationQuit()
        {
            mediaMaterial.SetTexture(MainTex, blackTexture);
        }
    }
}
