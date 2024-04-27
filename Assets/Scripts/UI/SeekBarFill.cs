using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace yutokun.SphericalMediaPlayer
{
    public class SeekBarFill : MonoBehaviour
    {
        [SerializeField]
        VideoPlayer videoPlayer;

        [SerializeField]
        Image fill;

        bool seeking;

        void Awake()
        {
            videoPlayer.seekCompleted += OnSeekCompleted;
        }

        void Update()
        {
            if (videoPlayer.isPlaying && !seeking)
            {
                fill.fillAmount = (float)(videoPlayer.time / videoPlayer.length);
            }
        }

        public void SetNormalizedTime(float normalizedTime)
        {
            fill.fillAmount = normalizedTime;
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
