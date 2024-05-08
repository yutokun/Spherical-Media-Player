using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace yutokun.SphericalMediaPlayer
{
    public class PhotoLoadingText : MonoBehaviour
    {
        [SerializeField]
        FileOpener fileOpener;

        [SerializeField]
        MediaPlayer mediaPlayer;

        void Awake()
        {
            fileOpener.OnOpenedAsObservable
                      .Subscribe(OnOpen)
                      .AddTo(this);

            gameObject.SetActive(false);
        }

        void OnOpen(string path)
        {
            if (path.IsPhotoPath())
            {
                ShowLoadingTextAsync().Forget();
            }
        }

        async UniTaskVoid ShowLoadingTextAsync()
        {
            gameObject.SetActive(true);

            await UniTask.WaitWhile(() => mediaPlayer.IsLoading);

            gameObject.SetActive(false);
        }
    }
}
