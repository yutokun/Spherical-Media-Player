using R3;
using UnityEngine;
using UnityEngine.Video;

namespace yutokun.SphericalMediaPlayer
{
    public class ProjectionModeIODispatcher : MonoBehaviour
    {
        [SerializeField]
        ProjectionModeDatabase projectionModeDatabase;

        [SerializeField]
        ProjectionModeSwitcher switcher;

        [SerializeField]
        ProjectionModeSpecifier[] specifiers;

        [SerializeField]
        FileOpener fileOpener;

        [SerializeField]
        VideoPlayer videoPlayer;

        void Awake()
        {
            fileOpener.OnOpenedAsObservable
                      .Subscribe(OnOpenFile)
                      .AddTo(this);

            foreach (var specifier in specifiers)
            {
                specifier.OnProjectionModeSpecifiedAsObservable
                         .Subscribe(OnProjectionModeSpecified)
                         .AddTo(this);
            }
        }

        void OnOpenFile(string path)
        {
            var mode = projectionModeDatabase.Get(path);
            switcher.SwitchTo(mode);
        }

        void OnProjectionModeSpecified(ProjectionMode mode)
        {
            projectionModeDatabase.AddOrReplace(videoPlayer.url, mode);
        }
    }
}
