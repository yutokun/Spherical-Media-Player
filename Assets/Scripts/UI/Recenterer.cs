using UnityEngine;
using UnityEngine.UI;

namespace yutokun.SphericalMediaPlayer
{
    public class Recenterer : MonoBehaviour
    {
        [SerializeField]
        Button recenterButton;

        [SerializeField]
        Transform camera;

        void Awake()
        {
            recenterButton.onClick.AddListener(() => camera.rotation = Quaternion.identity);
        }
    }
}
