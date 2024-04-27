using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

namespace yutokun.SphericalMediaPlayer
{
    public class CurrentTimeIndicator : MonoBehaviour
    {
        [SerializeField]
        VideoPlayer videoPlayer;

        [SerializeField]
        RectTransform indicator;

        [SerializeField]
        RectTransform seekBar;

        bool seeking;

        void Awake()
        {
            videoPlayer.seekCompleted += OnSeekCompleted;
        }

        void Update()
        {
            if (videoPlayer.isPlaying && !seeking)
            {
                var normalizedTime = videoPlayer.time / videoPlayer.length;
                SetPosition((float)normalizedTime);
            }
        }

        public void SetPosition(float normalizedTime)
        {
            var anchoredPosition = indicator.anchoredPosition;
            anchoredPosition.x = (seekBar.rect.width * normalizedTime) - (seekBar.rect.width / 2f);
            indicator.anchoredPosition = anchoredPosition;
        }

        public void StopUpdateUntilSeekingFinished()
        {
            seeking = true;
        }

        void OnSeekCompleted(VideoPlayer _)
        {
            UniTask.Void(async () =>
            {
                var frame = videoPlayer.frame;
                await UniTask.WaitUntil(() => videoPlayer.frame != frame);
                seeking = false;
            });
        }
    }
}
