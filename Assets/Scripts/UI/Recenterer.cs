using System.Collections;
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
            recenterButton.onClick.AddListener(() => StartCoroutine(Recenter()));
        }

        IEnumerator Recenter()
        {
            var startRotation = camera.rotation;
            var loopCount = 0;

            while (camera.rotation != Quaternion.identity)
            {
                ++loopCount;
                camera.rotation = Quaternion.Lerp(startRotation, Quaternion.identity, 1f / 60f * loopCount);
                yield return null;
            }
        }
    }
}
