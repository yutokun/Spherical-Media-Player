using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace yutokun.SphericalMediaPlayer
{
    public class ProjectionModeSpecifier : MonoBehaviour
    {
        [SerializeField]
        Button button;

        [SerializeField]
        ProjectionModeSwitcher switcher;

        [SerializeField]
        ProjectionMode mode;

        void Awake()
        {
            button.OnPointerClickAsObservable()
                  .Subscribe(_ => switcher.SwitchTo(mode))
                  .AddTo(this);
        }
    }
}
