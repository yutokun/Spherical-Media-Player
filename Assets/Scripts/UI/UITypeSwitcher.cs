using R3;
using UnityEngine;

namespace yutokun.SphericalMediaPlayer
{
    public class UITypeSwitcher : MonoBehaviour
    {
        [SerializeField]
        FileOpener fileOpener;

        [SerializeField]
        GameObject[] videoUI;

        [SerializeField]
        GameObject[] photoUI;

        void Awake()
        {
            fileOpener.OnOpenedAsObservable
                      .Subscribe(path => SwitchUI(path.IsMoviePath()))
                      .AddTo(this);
        }


        void SwitchUI(bool isVideo)
        {
            foreach (var ui in videoUI)
            {
                ui.SetActive(isVideo);
            }

            foreach (var ui in photoUI)
            {
                ui.SetActive(!isVideo);
            }
        }
    }
}
