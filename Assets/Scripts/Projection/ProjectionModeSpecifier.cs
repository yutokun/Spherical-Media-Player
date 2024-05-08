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
        RectTransform checkmark;

        [SerializeField]
        ProjectionModeSwitcher switcher;

        [SerializeField]
        ProjectionMode mode;

        public Observable<ProjectionMode> OnProjectionModeSpecifiedAsObservable => onProjectionModeSpecified;
        readonly Subject<ProjectionMode> onProjectionModeSpecified = new();

        void Awake()
        {
            button.OnPointerClickAsObservable()
                  .Subscribe(_ =>
                  {
                      switcher.SwitchTo(mode);
                      onProjectionModeSpecified.OnNext(mode);

                      var pos = checkmark.anchoredPosition;
                      pos.y = GetComponent<RectTransform>().anchoredPosition.y;
                      checkmark.anchoredPosition = pos;
                  })
                  .AddTo(this);
        }
    }
}
