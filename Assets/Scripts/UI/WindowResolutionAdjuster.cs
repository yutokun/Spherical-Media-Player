using R3;
using UnityEngine;

namespace yutokun.SphericalMediaPlayer
{
    public class WindowResolutionAdjuster : MonoBehaviour
    {
        static int MenuBarHeight => 43; // TODO 取得する方法があれば対応。43 は M2 あたりの MacBook で取りうる値。

        void Awake()
        {
            Observable.EveryValueChanged(this, _ => Screen.fullScreen)
                      .Subscribe(_ =>
                      {
                          if (Screen.fullScreen)
                          {
                              Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight - MenuBarHeight, FullScreenMode.MaximizedWindow);
                          }
                      });
        }
    }
}
