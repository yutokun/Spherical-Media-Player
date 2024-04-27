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

        void Update()
        {
            if (videoPlayer.isPlaying)
            {
                var normalizedTime = videoPlayer.time / videoPlayer.length;
                var anchoredPosition = indicator.anchoredPosition;
                anchoredPosition.x = (seekBar.rect.width * (float)normalizedTime) - (seekBar.rect.width / 2f);
                indicator.anchoredPosition = anchoredPosition;
            }
        }
    }
}
