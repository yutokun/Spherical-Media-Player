using UnityEngine;
using UnityEngine.UI;

namespace yutokun.SphericalMediaPlayer
{
    public class ProjectionModeSwitcherActivator : MonoBehaviour
    {
        [SerializeField]
        Button button;

        [SerializeField]
        Animator animator;

        [SerializeField]
        CanvasGroup canvas;

        static readonly int Hidden = Animator.StringToHash("Hidden");

        void Awake()
        {
            button.onClick.AddListener(() => Toggle());
            Hide();
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
            canvas.interactable = true;
            canvas.blocksRaycasts = true;
        }

        void Hide()
        {
            animator.SetBool(Hidden, true);
            canvas.interactable = false;
            canvas.blocksRaycasts = false;
        }
    }
}
