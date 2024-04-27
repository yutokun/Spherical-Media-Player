using System;
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

        void Awake()
        {
            videoPlayer.prepareCompleted += OnPrepareCompleted;
        }

        void OnPrepareCompleted(VideoPlayer source)
        {
            showHour = source.length >= 3600;

            currentTime.text = GetTimeText(0d);
            duration.text = GetTimeText(source.length);
        }

        void Update()
        {
            if (videoPlayer.isPlaying)
            {
                currentTime.text = GetTimeText(videoPlayer.time);
            }
        }

        string GetTimeText(double time)
        {
            return TimeSpan.FromSeconds(time).ToString(showHour ? @"hh\:mm\:ss" : @"mm\:ss");
        }
    }
}
