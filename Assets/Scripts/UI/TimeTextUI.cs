using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace yutokun.SphericalMediaPlayer
{
    public class TimeTextUI : MonoBehaviour
    {
        [SerializeField]
        VideoPlayer videoPlayer;

        [SerializeField]
        Text currentTime;

        [SerializeField]
        Text duration;

        bool showHour;

        bool seeking;

        void Awake()
        {
            videoPlayer.prepareCompleted += OnPrepareCompleted;
            videoPlayer.seekCompleted += OnSeekCompleted;
        }

        void OnPrepareCompleted(VideoPlayer source)
        {
            showHour = source.length >= 3600;

            currentTime.text = GetTimeText(0d);
            duration.text = GetTimeText(source.length);
        }

        void Update()
        {
            if (videoPlayer.isPlaying && !seeking)
            {
                currentTime.text = GetTimeText(videoPlayer.time);
            }
        }

        string GetTimeText(double time)
        {
            return TimeSpan.FromSeconds(time).ToString(showHour ? @"hh\:mm\:ss" : @"mm\:ss");
        }

        public void SetNormalizedTime(float normalizedTime)
        {
            currentTime.text = GetTimeText(videoPlayer.length * normalizedTime);
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
