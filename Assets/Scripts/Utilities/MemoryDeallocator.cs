using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace yutokun.SphericalMediaPlayer
{
    public class MemoryDeallocator : MonoBehaviour
    {
        [SerializeField]
        FileOpener fileOpener;

        [SerializeField]
        MediaPlayer mediaPlayer;

        void Awake()
        {
            fileOpener.OnOpenedAsObservable
                      .Subscribe(_ => DeallocateAsync().Forget())
                      .AddTo(this);
        }

        async UniTaskVoid DeallocateAsync()
        {
            await UniTask.WaitWhile(() => mediaPlayer.IsLoading);
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }
    }
}
