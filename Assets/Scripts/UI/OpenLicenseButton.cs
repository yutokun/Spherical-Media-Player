using UnityEngine;
using UnityEngine.UI;

namespace yutokun.SphericalMediaPlayer
{
    public class OpenLicenseButton : MonoBehaviour
    {
        [SerializeField]
        Button button;

        void Awake()
        {
            button.onClick.AddListener(OnClick);
        }

        void OnClick()
        {
            Application.OpenURL("https://github.com/yutokun/Spherical-Media-Player#license");
        }
    }
}
