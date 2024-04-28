using System.IO;
using R3;
using SFB;
using UnityEngine;
using UnityEngine.UI;

namespace yutokun.SphericalMediaPlayer
{
    public class FileOpener : MonoBehaviour
    {
        [SerializeField]
        Button openButton;

        public Observable<string> OnOpenedAsObservable => onOpened.AsObservable();
        readonly Subject<string> onOpened = new();

        readonly ExtensionFilter[] extensionsFilter =
        {
            new("Movie Files", "mp4", "mov", "m4v"),
            // new("Photo Files", "png", "jpg", "jpeg"), // TODO
        };

        void Start()
        {
            openButton.onClick.AddListener(ShowOpenDialog);
            ShowOpenDialog();
        }

        void ShowOpenDialog()
        {
            var paths = StandaloneFileBrowser.OpenFilePanel("Open Movie...", Settings.LastPath, extensionsFilter, false);
            var path = paths[0];
            if (string.IsNullOrEmpty(path)) return;

            Debug.Log($"開いたファイル: {path}");
            Settings.LastPath.Value = Path.GetDirectoryName(path);

            onOpened.OnNext(path);
        }
    }
}
