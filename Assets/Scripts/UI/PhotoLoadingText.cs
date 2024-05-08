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
        Material mediaMaterial;

        static readonly int MainTex = Shader.PropertyToID("_MainTex");

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

            var prevTexture = mediaMaterial.GetTexture(MainTex);
            await UniTask.WaitUntil(() => mediaMaterial.GetTexture(MainTex) != prevTexture);

            gameObject.SetActive(false);
        }
    }
}
