using UnityEngine;
using UnityEngine.Video;

namespace yutokun.SphericalMediaPlayer
{
    public class KeyboardManipulation : MonoBehaviour
    {
        [SerializeField]
        VideoPlayer videoPlayer;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (videoPlayer.isPlaying)
                {
                    videoPlayer.Pause();
                }
                else
                {
                    videoPlayer.Play();
                }
            }
        }
    }
}
