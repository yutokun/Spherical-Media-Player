using System.Collections;
using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.UI;
using Screen = UnityEngine.Screen;

namespace yutokun.SphericalMediaPlayer
{
    public class ControlAreaActivator : MonoBehaviour
    {
        [SerializeField]
        CanvasGroup canvas;

        [SerializeField]
        Image background;

        [SerializeField]
        Animator animator;

        bool dragging;

        Vector3 prevMousePosition;
        float mouseStopTime;

        bool mouseWasClicked;

        static readonly int Hidden = Animator.StringToHash("Hidden");

        static bool MouseIsInWindow => Input.mousePosition.x >= 0 && Input.mousePosition.x <= Screen.width &&
                                       Input.mousePosition.y >= 0 && Input.mousePosition.y <= Screen.height;

        void Awake()
        {
            background.OnPointerDownAsObservable()
                      .Subscribe(_ => StartCoroutine(DetectClick()))
                      .AddTo(this);
        }

        IEnumerator DetectClick()
        {
            dragging = true;

            var mousePositionOnButtonDown = Input.mousePosition;
            yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
            mouseWasClicked = mousePositionOnButtonDown == Input.mousePosition;
            yield return null;
            mouseWasClicked = false;

            dragging = false;
        }

        void Update()
        {
            BuildConditions();
            ApplyActiveness();
        }

        void BuildConditions()
        {
            if (MouseIsInWindow)
            {
                if (prevMousePosition != Input.mousePosition)
                {
                    mouseStopTime = 0f;
                }
                else
                {
                    mouseStopTime += Time.deltaTime;
                }
            }

            prevMousePosition = Input.mousePosition;
        }

        void ApplyActiveness()
        {
            var mouseIsMoving = mouseStopTime == 0f;
            if (mouseIsMoving && !dragging)
            {
                Show();
            }
            else if (mouseWasClicked || mouseStopTime > 3f)
            {
                Hide();
            }

            if (!MouseIsInWindow)
            {
                Hide();
            }
        }

        void Show()
        {
            animator.SetBool(Hidden, false);
            canvas.interactable = true;
            canvas.blocksRaycasts = true;

            Cursor.visible = true;
        }

        void Hide()
        {
            animator.SetBool(Hidden, true);
            canvas.interactable = false;
            canvas.blocksRaycasts = false;

            Cursor.visible = false;
        }
    }
}
