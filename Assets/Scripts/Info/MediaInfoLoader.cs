using System;
using System.IO;
using System.Text;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace yutokun.SphericalMediaPlayer
{
    public class MediaInfoLoader : MonoBehaviour
    {
        [SerializeField]
        FileOpener fileOpener;

        [SerializeField]
        MediaPlayer mediaPlayer;

        [SerializeField]
        VideoPlayer videoPlayer;

        [SerializeField]
        Material mediaMaterial;

        [SerializeField]
        Text infoText;

        static readonly int MainTex = Shader.PropertyToID("_MainTex");

        void Awake()
        {
            fileOpener.OnOpenedAsObservable
                      .Subscribe(OnOpen)
                      .AddTo(this);
        }

        void OnOpen(string path)
        {
            UniTask.Void(async () =>
            {
                if (path.IsVideoPath() || path.IsPhotoPath())
                {
                    await UniTask.WaitWhile(() => mediaPlayer.IsLoading);

                    if (path.IsVideoPath())
                    {
                        ShowVideoInfo();
                    }
                    else if (path.IsPhotoPath())
                    {
                        ShowPhotoInfo();
                    }
                }
            });
        }

        void ShowVideoInfo()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"<b>{Path.GetFileName(mediaPlayer.Path)}</b>");
            sb.AppendLine();

            var time = TimeSpan.FromSeconds(videoPlayer.length);
            sb.AppendLine($"Duration: {time:mm\\:ss}");

            sb.AppendLine($"Resolution: {videoPlayer.width} × {videoPlayer.height}");
            sb.AppendLine($"FPS: {videoPlayer.frameRate}");
            sb.AppendLine();
            sb.AppendLine("Path:");
            sb.AppendLine($"{mediaPlayer.Path}");

            infoText.text = sb.ToString();
        }

        void ShowPhotoInfo()
        {
            var texture = mediaMaterial.GetTexture(MainTex);
            var sb = new StringBuilder();
            sb.AppendLine($"<b>{Path.GetFileName(mediaPlayer.Path)}</b>");
            sb.AppendLine();
            sb.AppendLine($"Resolution: {texture.width} × {texture.height}");
            sb.AppendLine();
            sb.AppendLine("Path:");
            sb.AppendLine($"{mediaPlayer.Path}");

            infoText.text = sb.ToString();
        }
    }
}
