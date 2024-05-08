using UnityEngine;
using UnityEngine.UI;

namespace yutokun.SphericalMediaPlayer
{
    public class MediaInfoActivator : MonoBehaviour
    {
        [SerializeField]
        Button button;

        [SerializeField]
        Animator animator;

        static readonly int Hidden = Animator.StringToHash("Hidden");

        void Awake()
        {
            button.onClick.AddListener(() => Toggle());

            Hide();
            animator.Play("Hide", 0, 1f);
        }

        void Toggle()
        {
            var hidden = animator.GetBool(Hidden);
            if (hidden)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        void Show()
        {
            animator.SetBool(Hidden, false);
        }

        void Hide()
        {
            animator.SetBool(Hidden, true);
        }
    }
}
