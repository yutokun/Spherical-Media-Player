using System.Collections;
using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace yutokun.SphericalMediaPlayer
{
    public class SeekBarController : MonoBehaviour
    {
        [SerializeField]
        Image seekBar;

        [SerializeField]
        CurrentTimeIndicator indicator;

        [SerializeField]
        VideoPlayer videoPlayer;

        bool Seeking { get; set; }

        void Awake()
        {
            seekBar.OnPointerDownAsObservable()
                   .Subscribe(_ => StartCoroutine(Seek()))
                   .AddTo(this);

            seekBar.OnPointerUpAsObservable()
                   .Subscribe(_ => Seeking = false)
                   .AddTo(this);
        }

        IEnumerator Seek()
        {
            indicator.StopUpdateUntilSeekingFinished();

            var center = transform.position.x;
            var width = seekBar.rectTransform.rect.width * (Screen.dpi / 72f); // dpi の違いを補正
            var leftMargin = center - width / 2f;

            var normalizedTime = 0f;
            Seeking = true;
            while (Seeking)
            {
                normalizedTime = Mathf.Clamp01((Input.mousePosition.x - leftMargin) / width);
                indicator.SetPosition(normalizedTime);
                yield return null;
            }

            videoPlayer.time = videoPlayer.length * normalizedTime;
        }
    }
}
