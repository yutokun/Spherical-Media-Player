using System;
using UnityEngine;
using UnityEngine.UI;

namespace yutokun.SphericalMediaPlayer
{
    public class ProjectionModeSwitcher : MonoBehaviour
    {
        [SerializeField]
        Material videoMaterial;

        [SerializeField]
        Text text;

        static readonly int ImageType = Shader.PropertyToID("_ImageType");
        static readonly int Layout = Shader.PropertyToID("_Layout");
        static readonly int Rotation = Shader.PropertyToID("_Rotation");

        void Awake()
        {
            SwitchTo(ProjectionMode.Mono360); // TODO 何か良い自動判定ができれば入れる
        }

        public void SwitchTo(ProjectionMode mode)
        {
            text.text = mode switch
            {
                ProjectionMode.Mono360 => "360\nMono",
                ProjectionMode.TB360 => "360\nTB",
                ProjectionMode.SBS360 => "360\nSBS",
                ProjectionMode.Mono180 => "180\nMono",
                ProjectionMode.TB180 => "180\nTB",
                ProjectionMode.SBS180 => "180\nSBS",
                _ => throw new ArgumentOutOfRangeException(),
            };

            SetDegree(mode);
            SetStereo(mode);
            SetRotation(mode);
        }

        void SetDegree(ProjectionMode mode)
        {
            var imageType = mode switch
            {
                ProjectionMode.Mono360 => 0,
                ProjectionMode.TB360 => 0,
                ProjectionMode.SBS360 => 0,
                ProjectionMode.Mono180 => 1,
                ProjectionMode.TB180 => 1,
                ProjectionMode.SBS180 => 1,
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null),
            };

            videoMaterial.SetFloat(ImageType, imageType);
        }

        void SetStereo(ProjectionMode mode)
        {
            var layout = mode switch
            {
                ProjectionMode.Mono360 => 0,
                ProjectionMode.TB360 => 2,
                ProjectionMode.SBS360 => 1,
                ProjectionMode.Mono180 => 0,
                ProjectionMode.TB180 => 2,
                ProjectionMode.SBS180 => 1,
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null),
            };

            videoMaterial.SetFloat(Layout, layout);
        }

        void SetRotation(ProjectionMode mode)
        {
            var rotation = mode switch
            {
                ProjectionMode.Mono360 => 90,
                ProjectionMode.TB360 => 90,
                ProjectionMode.SBS360 => 90,
                ProjectionMode.Mono180 => 0,
                ProjectionMode.TB180 => 0,
                ProjectionMode.SBS180 => 0,
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null),
            };

            videoMaterial.SetFloat(Rotation, rotation);
        }
    }
}
