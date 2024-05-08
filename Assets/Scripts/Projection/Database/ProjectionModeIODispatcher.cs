using System.Linq;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

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
        MediaPlayer mediaPlayer;

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
            UniTask.Void(async () =>
            {
                if (path.IsPhotoPath())
                {
                    await UniTask.WaitWhile(() => mediaPlayer.IsLoading);
                }

                var mode = projectionModeDatabase.Get(path);
                switcher.SwitchTo(mode);

                var specifier = specifiers.First(s => s.Mode == mode);
                specifier.MoveCheckmarkHere();
            });
        }

        void OnProjectionModeSpecified(ProjectionMode mode)
        {
            projectionModeDatabase.AddOrReplace(mediaPlayer.Path, mode);
        }
    }
}
