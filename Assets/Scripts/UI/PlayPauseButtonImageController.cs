using R3;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace yutokun.SphericalMediaPlayer
{
    public class PlayPauseButtonImageController : MonoBehaviour
    {
        [SerializeField]
        VideoPlayer videoPlayer;

        [SerializeField]
        Button button;

        [SerializeField]
        Image image;

        [SerializeField]
        Sprite play, pause;

        void Awake()
        {
            Observable.EveryValueChanged(videoPlayer, player => player.isPlaying)
                      .Subscribe(isPlaying => image.sprite = isPlaying ? pause : play)
                      .AddTo(this);
        }
    }
}
