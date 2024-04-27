using System;
using System.IO;
using SFB;
using UnityEngine;
using UnityEngine.UI;

namespace yutokun.SphericalMediaPlayer
{
    public class FileOpener : MonoBehaviour
    {
        [SerializeField]
        Button openButton;

        public event Action<string> OnOpen;

        readonly ExtensionFilter[] extensionsFilter =
        {
            new("Movie Files", "mp4", "mov", "m4v"),
            new("Photo Files", "png", "jpg", "jpeg"),
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

            OnOpen?.Invoke(path);
        }
    }
}
