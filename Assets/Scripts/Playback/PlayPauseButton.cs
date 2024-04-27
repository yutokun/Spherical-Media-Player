using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace yutokun.SphericalMediaPlayer
{
    public class PlayPauseButton : MonoBehaviour
    {
        [SerializeField]
        Button playPauseButton;

        [SerializeField]
        VideoPlayer videoPlayer;

        void Awake()
        {
            playPauseButton.onClick.AddListener(OnClick);
        }

        void OnClick()
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
            }
            else
            {
                videoPlayer.Play();
            }
        }
    }
}
