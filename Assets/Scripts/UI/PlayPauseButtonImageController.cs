using R3;
using R3.Triggers;
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

        [SerializeField]
        Animator animator;

        static readonly int Pressed = Animator.StringToHash("Pressed");

        void Awake()
        {
            Observable.EveryValueChanged(videoPlayer, player => player.isPlaying)
                      .Subscribe(isPlaying => image.sprite = isPlaying ? pause : play)
                      .AddTo(this);

            button.OnPointerDownAsObservable()
                  .Subscribe(_ => animator.SetBool(Pressed, true))
                  .AddTo(this);

            button.OnPointerUpAsObservable()
                  .Subscribe(_ => animator.SetBool(Pressed, false))
                  .AddTo(this);
        }
    }
}
