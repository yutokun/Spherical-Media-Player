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

        [SerializeField]
        float recenterDuration = 1f;

        void Awake()
        {
            recenterButton.onClick.AddListener(() => StartCoroutine(Recenter()));
        }

        IEnumerator Recenter()
        {
            var startRotation = camera.rotation;
            var elapsedTime = 0f;

            while (camera.rotation != Quaternion.identity)
            {
                elapsedTime += Time.deltaTime;
                camera.rotation = Quaternion.Lerp(startRotation, Quaternion.identity, Mathf.Clamp01(elapsedTime / recenterDuration));
                yield return null;
            }
        }
    }
}
